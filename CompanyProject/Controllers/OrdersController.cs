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
                    return await context.OrderHeaders
                        .Select(s => s)
                        .Where(s => Name != null ? s.Name.Contains(Name) : true)
                        .Where(s => Code != null ? s.Code.Contains(Code) : true)
                        .OrderBy(s => s.)
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();

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
