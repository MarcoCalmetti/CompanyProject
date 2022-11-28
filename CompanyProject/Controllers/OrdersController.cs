using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyProject.Models;

namespace CompanyProject.Controllers
{
    class OrdersController
    {
        public async static Task<List<OrderHeader>> GetAll(string Name, string Code, int page, int pageSize)
        {
            try
            {
                using (CompanyContext context = new CompanyContext())
                {
                    List<OrderHeader> Lista = await context.OrderHeaders
                        .Select(s => s)
                        .OrderBy(s => s.OrderHeaderId)
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();

                    foreach (OrderHeader Oh in Lista)
                    {
                        Oh.TotalPrice = await context.OrderRows
                            .Where(s => s.OrderHeaderId == Oh.OrderHeaderId)
                            .SumAsync(s => s.Quantity * s.UnitPrice);
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

                    List<OrderRow> OrderRowsList = new List<OrderRow>();
                    
                    foreach(Item i in Items)
                    {
                        if (i.Quantity == null || i.Quantity == 0)
                            continue;

                        OrderRowsList.Add(
                                new OrderRow
                                {
                                    OrderHeaderId = await context.OrderHeaders.Select(s=>s.OrderHeaderId).MaxAsync() + 1,
                                    ItemId = i.Id,
                                    UnitPrice = i.Price,
                                    Quantity = (int)i.Quantity
                                }
                            );
                    }

                    context.OrderRows.AddRange(OrderRowsList);

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

        public async static Task UpdateOrder(List<Item> Items, OrderHeader Oh)
        {
            try
            {
                using (CompanyContext context = new CompanyContext())
                {

                    List<OrderRow> OrderRowsList = await context.OrderRows.Where(s=>s.OrderHeaderId == Oh.OrderHeaderId).ToListAsync();

                    Item toAdd = null;

                    foreach(OrderRow or in OrderRowsList)
                    {
                        foreach (Item i in Items)
                            if (or.ItemId == i.Id)
                            {
                                if(i.Quantity == null)
                                {
                                    toAdd = i;
                                    break;
                                }
                                or.Quantity = (int)i.Quantity;
                                break;
                            }

                         if(toAdd != null)
                            context.OrderRows.Add(
                                new OrderRow
                                {
                                    OrderHeaderId = Oh.OrderHeaderId,
                                    ItemId = toAdd.Id,
                                    UnitPrice = toAdd.Price,
                                    Quantity = (int)toAdd.Quantity
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

        public async static Task<List<Item>> GetAllOrdersRows(OrderHeader Or)
        {
            try
            {
                using (CompanyContext context = new CompanyContext())
                {
                    List < Item > Items = await context.Items.Select(s => s).ToListAsync();

                    foreach (Item i in Items)
                        i.Quantity = (await context.OrderRows.Select(s => s).Where(s => s.ItemId == i.Id && s.OrderHeaderId == Or.OrderHeaderId).FirstOrDefaultAsync()).Quantity;
     

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

        public async static Task DeleteOrder(OrderHeader Oh)
        {
            try
            {
                using (CompanyContext context = new CompanyContext())
                {
                    if(Oh.OrderStatus == (await context.OrderStates.Where(s=>s.Name == "Confermato").FirstOrDefaultAsync()).Id)
                    {
                        context.OrderRows.RemoveRange(await context.OrderRows.Where(s => s.OrderHeaderId == Oh.OrderHeaderId).ToListAsync());
                        await context.SaveChangesAsync();
                    }
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
