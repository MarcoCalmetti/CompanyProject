using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyProject.Models;

namespace CompanyProject.Controllers
{
    class ItemsController 
    {
        public async static Task<List<Item>> GetAll(string Name, string Code, string OrderBy, int page, int pageSize)
        {
            try
            {
                using (CompanyContext context = new CompanyContext())
                {
                    var x = context.Items
                        .Where(s => !String.IsNullOrEmpty(Name) ? s.Name.Contains(Name) : true)
                        .Where(s => !String.IsNullOrEmpty(Code) ? s.Code.Contains(Code) : true)
                        .OrderBy(s => s.Id);
                        
                    if(OrderBy != null)
                    {
                        if (OrderBy == "Alphabetical Order")
                            x = x.OrderBy(s => s.Name);
                        if (OrderBy == "Descending Price")
                            x = x.OrderByDescending(s => s.Price);
                        if (OrderBy == "Ascending Price")
                            x = x.OrderBy(s => s.Price);
                    }

                    return await x.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();


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

        public async static Task<int> GetItemsNumber(string Name, string Code)
        {
            try
            {
                using (CompanyContext context = new CompanyContext())
                {
                    return await context.Items
                        .Where(s => !String.IsNullOrEmpty(Name) ? s.Name.Contains(Name) : true)
                        .Where(s => !String.IsNullOrEmpty(Code) ? s.Code.Contains(Code) : true)
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
    }
}
