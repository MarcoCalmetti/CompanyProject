using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CompanyProject.Models;

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
                    var QueryEF = context.OrderHeaders
                        //.Join(context.Resellers,
                        //s => s.ResellerId,
                        //p => p.ResellerID,
                        //(s, p) => new OrderHeaderView
                        //{
                        //    OrderHeaderId = s.OrderHeaderId,
                        //    BusinessName = p.BusinessName,
                        //    ResellerId = s.ResellerId,
                        //    OrderDate = s.OrderDate,
                        //    OrderReceipt = s.OrderReceipt,
                        //    ProductionStartDate = s.ProductionStartDate,
                        //    ProductionEndDate = s.ProductionEndDate,
                        //    OrderStatus = s.OrderStatus,
                        //    SalesOrderReference = s.SalesOrderReference,
                        //    OrderStatusString = context.OrderStates.Where(t => t.Id == s.OrderStatus).FirstOrDefault().Name,
                        //    Note = s.Note
                        //})

                        .Select(s =>new OrderHeaderView
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

                    List<OrderHeaderView> Lista = (List<OrderHeaderView>)await QueryEF.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

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
                                    OrderHeaderId = await context.OrderHeaders.Select(s=>s.OrderHeaderId).MaxAsync() + 1,
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
                    return await context.OrderStates.Select(s => s.Name).ToListAsync();
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


    }
}
