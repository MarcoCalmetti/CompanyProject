using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CompanyProject.Models;
using RestSharp;
using RestSharp.Authenticators;

namespace CompanyProject.Controllers
{
    class OrdersController
    {
        public async static Task<List<OrderHeaderView>> GetAll(string BusinessName, string SelectedStatus, int? SelectedResellerID, string SelectedOrderBy, int page, int pageSize)
        {
            try
            {
                using (CompanyContext context = new CompanyContext())
                {
                    await GetOrdersFromAPI();
                    var QueryEF = context.OrderHeaders

                        .Select(s =>new OrderHeaderView
                        {
                            OrderHeaderId = s.OrderHeaderId,
                            OrderIdAPI = s.OrderIdAPI,
                            BusinessName = s.ResellerId != null ? context.Resellers.Where(t => t.ResellerID == s.ResellerId).FirstOrDefault().BusinessName : null,
                            ResellerId = s.ResellerId,
                            OrderDate = s.OrderDate,
                            OrderReceipt = s.OrderReceipt,
                            ProductionStartDate = s.ProductionStartDate,
                            ProductionEndDate = s.ProductionEndDate,
                            OrderStatus = s.OrderStatus,
                            SalesOrderReference = s.SalesOrderReference,
                            OrderStatusString = context.OrderStates.Where(t => t.Id == s.OrderStatus).FirstOrDefault().Name,
                            Note = s.Note
                        })
                        .Where(s => !String.IsNullOrEmpty(BusinessName) ? context.Resellers.Where(t => t.ResellerID== s.ResellerId).FirstOrDefault().BusinessName.Contains(BusinessName) : true)
                        .Where(s => SelectedStatus != null ? context.OrderStates.Where(t => t.Id == s.OrderStatus).FirstOrDefault().Name == SelectedStatus : true)
                        .Where(s => SelectedResellerID != null ? s.ResellerId == SelectedResellerID : true);

                    switch (SelectedOrderBy)
                    {
                        case "OrderDate":
                            QueryEF = QueryEF
                            .OrderByDescending(s => s.OrderDate);
                            break;
                        case "Reseller Name":
                            QueryEF = QueryEF
                            .OrderBy(s => s.BusinessName);
                            break;
                        default:
                            QueryEF = QueryEF
                            .OrderBy(s => s.OrderHeaderId);
                            break;
                    }

                    List<OrderHeaderView> Lista = await QueryEF.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

                    foreach (OrderHeaderView Oh in Lista)
                    {
                        if((await context.OrderRows.Where(s => s.OrderHeaderId == Oh.OrderHeaderId).CountAsync()) > 0)
                        {
                            Oh.TotalPrice = await context.OrderRows
                                    .Where(s => s.OrderHeaderId == Oh.OrderHeaderId)
                                    .SumAsync(s => s.Quantity * s.UnitPrice);
                            Oh.Leadtime = await context.OrderRows
                                .Where(s => s.OrderHeaderId == Oh.OrderHeaderId)
                                .Include("Item")
                                .MaxAsync(s => s.Item.LeadTime);
                        }
                    }

                    return Lista;
                    
                }
            }
            catch (ArgumentException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async static Task<int> GetItemsNumber(string BusinessName, string SelectedStatus, int? SelectedResellerID)
        {
            try
            {
                using (CompanyContext context = new CompanyContext())
                {
                    return await context.OrderHeaders
                        .Select(s => new OrderHeaderView
                        {
                            OrderHeaderId = s.OrderHeaderId,
                            BusinessName = s.ResellerId != null ? context.Resellers.Where(t => t.ResellerID == s.ResellerId).FirstOrDefault().BusinessName : null,
                            ResellerId = s.ResellerId,
                            OrderDate = s.OrderDate,
                            OrderReceipt = s.OrderReceipt,
                            ProductionStartDate = s.ProductionStartDate,
                            ProductionEndDate = s.ProductionEndDate,
                            OrderStatus = s.OrderStatus,
                            SalesOrderReference = s.SalesOrderReference,
                            OrderStatusString = context.OrderStates.Where(t => t.Id == s.OrderStatus).FirstOrDefault().Name,
                            Note = s.Note
                        })
                        .Where(s => !String.IsNullOrEmpty(BusinessName) ? context.Resellers.Where(t => t.ResellerID == s.ResellerId).FirstOrDefault().BusinessName.Contains(BusinessName) : true)
                        .Where(s => SelectedStatus != null ? context.OrderStates.Where(t => t.Id == s.OrderStatus).FirstOrDefault().Name == SelectedStatus : true)
                        .Where(s => SelectedResellerID != null ? s.ResellerId == SelectedResellerID : true)
                        .CountAsync();
                }
            }
            catch (ArgumentException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public async static Task AddNewOrder(List<Item> Items, string note)
        {
            try
            {
                using (CompanyContext context = new CompanyContext())
                {
                    OrderHeader NewOr = new OrderHeader
                    {
                        ResellerId = null,
                        OrderDate = DateTime.Now,
                        OrderStatus = (await context.OrderStates.Where(s => s.Name == "Confermato").FirstOrDefaultAsync()).Id, 
                        OrderReceipt = DateTime.Now,
                        ProductionStartDate = null,
                        ProductionEndDate = null,
                        SalesOrderReference = null,
                        Note = note
                    };

                    context.OrderHeaders.Add(NewOr);
                    await context.SaveChangesAsync();
                    List<OrderRow> OrderRowsList = new List<OrderRow>();
                    
                    foreach(Item i in Items)
                    {
                        if (i.Quantity == null || i.Quantity == 0)
                            continue;

                        context.OrderRows.Add(
                                new OrderRow
                                {
                                    OrderHeaderId = await context.OrderHeaders.Select(s=>s.OrderHeaderId).MaxAsync(),
                                    ItemId = i.Id,
                                    UnitPrice = i.Price,
                                    Quantity = (int)i.Quantity
                                }
                            );
                    }

                    await context.SaveChangesAsync();
                }
            }
            catch (ArgumentException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async static Task UpdateOrder(List<Item> Items, OrderHeaderView Oh, string note)
        {
            try
            {
                using (CompanyContext context = new CompanyContext())
                {
                    var mod = await context.OrderHeaders.SingleOrDefaultAsync(s=>s.OrderHeaderId == Oh.OrderHeaderId);
                    mod.Note = note;
                    context.Entry(mod).State = EntityState.Modified;
                    
                    List<OrderRow> OrderRowsList = await context.OrderRows.Where(s=>s.OrderHeaderId == Oh.OrderHeaderId).ToListAsync();

                    context.OrderRows.RemoveRange(OrderRowsList);

                    foreach(Item i in Items)
                        if(i.Quantity != 0)
                        context.OrderRows.Add(
                            new OrderRow
                            {
                                OrderHeaderId = Oh.OrderHeaderId,
                                ItemId = i.Id,
                                UnitPrice = i.Price,
                                Quantity = (int)i.Quantity
                            });

                    await context.SaveChangesAsync();
                }
            }
            catch (ArgumentException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async static Task<List<Item>> GetAllOrdersRows(OrderHeaderView Or)
        {
            try
            {
                using (CompanyContext context = new CompanyContext())
                {

                    List < Item > Items = await context.Items.ToListAsync();

                    foreach (Item i in Items)
                    {
                        if (await context.OrderRows.Where(s => s.ItemId == i.Id && s.OrderHeaderId == Or.OrderHeaderId).CountAsync() > 0)
                            i.Quantity = await context.OrderRows.Where(s => s.ItemId == i.Id && s.OrderHeaderId == Or.OrderHeaderId).SumAsync(s => s.Quantity);
                        else
                            i.Quantity = 0;
                    }
                        

                    return Items;
                }
            }
            catch (ArgumentException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async static Task<List<Item>> GetAllOrdersRows()
        {
            try
            {
                using (CompanyContext context = new CompanyContext())
                {
                    List<Item> Items = await context.Items.ToListAsync();

                    foreach (Item i in Items)
                        i.Quantity = 0;

                    return Items;
                }
            }
            catch (ArgumentException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async static Task DeleteOrder(OrderHeaderView Oh)
        {
            try
            {
                using (CompanyContext context = new CompanyContext())
                {
                    var x = await context.OrderRows.Where(s => s.OrderHeaderId == Oh.OrderHeaderId).ToListAsync();
                    context.OrderRows.RemoveRange(x);
                    await context.SaveChangesAsync();
                    context.OrderHeaders.Remove(await context.OrderHeaders.Where(s=>s.OrderHeaderId == Oh.OrderHeaderId).FirstOrDefaultAsync());
                    await context.SaveChangesAsync();
                }
            }
            catch (ArgumentException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async static Task<List<string>> OrderStatusList()
        {
            try
            {
                using (CompanyContext context = new CompanyContext())
                {
                    return await context.OrderStates.Select(s => s.Name).Where(s=>!s.Contains("Inserito")).ToListAsync();
                }
            }
            catch (ArgumentException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async static Task<List<int?>> ResellersIDList()
        {
            try
            {
                using (CompanyContext context = new CompanyContext())
                {
                    return await context.OrderHeaders.Select(s => s.ResellerId).Distinct().ToListAsync();
                }
            }
            catch (ArgumentException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async static Task StartProduction(OrderHeaderView Oh)
        {
            try
            {
                using (CompanyContext context = new CompanyContext())
                {
                    if (Oh.OrderIdAPI != null)
                        ModifyStatus(Oh, 30);

                    var x = await context.OrderHeaders.FirstOrDefaultAsync(s=>s.OrderHeaderId == Oh.OrderHeaderId);
                    x.OrderStatus = (await context.OrderStates.FirstOrDefaultAsync(s => s.Name == "InProduzione")).Id;
                    x.ProductionStartDate = DateTime.Now;
                    context.Entry(x).State = EntityState.Modified;
                    await context.SaveChangesAsync();
                }
            }
            catch (ArgumentException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async static Task EndProduction(OrderHeaderView Oh)
        {
            try
            {
                using (CompanyContext context = new CompanyContext())
                {
                    if(Oh.OrderIdAPI != null)
                        ModifyStatus(Oh, 40);

                    var x = await context.OrderHeaders.FirstOrDefaultAsync(s => s.OrderHeaderId == Oh.OrderHeaderId);
                    x.OrderStatus = (await context.OrderStates.FirstOrDefaultAsync(s => s.Name == "Prodotto")).Id;
                    x.ProductionEndDate = DateTime.Now;
                    context.Entry(x).State = EntityState.Modified;
                    await context.SaveChangesAsync();
                }
            }
            catch (ArgumentException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private static void ModifyStatus(OrderHeaderView Oh, int status)
        {
            using (var context = new CompanyContext())
            {
                var client = ConnectionToAPI();

                var request = new RestRequest();

                var x = new RestRequest(Oh.OrderIdAPI + @"/state");

                x.AddBody(new
                {
                    id = status
                });

                client.Put(x);
            }
        }

        private static RestClient ConnectionToAPI()
        {
            string APIAddress = ConfigurationManager.ConnectionStrings["APIAddress"].ConnectionString;
            string Token = ConfigurationManager.ConnectionStrings["TokenAPI"].ConnectionString;
            var option = new RestClientOptions(APIAddress)
            {
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true //verifica il certifcato (ssl/tls) e mettendolo a true, viene certificato
            };

            var client = new RestClient(option)
            {
                Authenticator = new JwtAuthenticator(Token)//andrà poi messo nell'app config
            };

            return client;
        }

        private async static Task GetOrdersFromAPI()
        {
            using (var context = new CompanyContext())
            {

                var client = ConnectionToAPI();

                var request = new RestRequest();

                var response = await client.GetAsync<List<OrderAPI>>(request);

                foreach(OrderAPI OA in response)
                {
                    if((await context.OrderHeaders.CountAsync(s=>s.OrderIdAPI == OA.Id)) == 0 && (await context.Resellers.CountAsync(s => s.ResellerIdAPI == OA.ResellerId)) > 0)
                    {
                        OrderHeader NewOh = new OrderHeader
                        {
                            OrderIdAPI = OA.Id,
                            ResellerId = (await context.Resellers.Where(s => s.ResellerIdAPI == OA.ResellerId).FirstOrDefaultAsync()).ResellerID,
                            OrderDate = OA.OrderDate,
                            OrderStatus = OA.OrderStateId,
                            OrderReceipt = (DateTime)OA.SendDate,
                            ProductionStartDate = OA.StartProductionDate,
                            ProductionEndDate = OA.StopProductionDate,
                            SalesOrderReference = OA.CustomerId,
                            Note = OA.Notes,
                        };

                        context.OrderHeaders.Add(NewOh);
                        await context.SaveChangesAsync();

                        List<OrderRow> NewOrderRows = new List<OrderRow>();
                        foreach(OrderItem OI in OA.OrderItems)
                        {
                            NewOrderRows.Add(new OrderRow
                            {
                                OrderHeaderId = await context.OrderHeaders.MaxAsync(s => s.OrderHeaderId),
                                ItemId = OI.ItemId,
                                Quantity = OI.Quantity,
                                UnitPrice = OI.UnitaryPrice
                            });
                        }
                        context.OrderRows.AddRange(NewOrderRows);
                        await context.SaveChangesAsync();
                    } 
                }
            }
        }

        public static bool ValidationChecker(List<Item> ItemList)
        {
            if(ItemList != null)
                foreach (Item i in ItemList)
                    if (i.ErrorChecker == true)
                        return false;
            return true;
        }
    }
}
