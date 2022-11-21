using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyProject.Models;

namespace CompanyProject.Controllers
{
    static class CompanyController
    {
        public async static Task<List<Reseller>> GetAllResellers(string BusinessName, string VAT, string City, int page, int pageSize)
        {
            using (CompanyContext context = new CompanyContext())
            {
                return await context.Resellers
                    .Select(s => s)
                    .Where(s => BusinessName != null ? s.BusinessName.Contains(BusinessName) : true)
                    .Where(s => VAT != null ? s.VAT.Contains(VAT) : true)
                    .Where(s=> City != null ? s.City == City : true)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            }
        }

        public async static Task AddReseller(string BusinessName, string VAT, string Address, string City, string PostalCode, string Mail, string TelephoneNumber)
        {
            
        }

        public async static Task<int> checkReseller()
        {
            using (CompanyContext context = new CompanyContext())
            {
                return await context.Resellers
                    .Select(s => s)
                    .Where(s => BusinessName != null ? s.BusinessName.Contains(BusinessName) : true)
                    .Where(s => VAT != null ? s.VAT.Contains(VAT) : true)
                    .Where(s => City != null ? s.City == City : true)
                    .Count();
                    
            }
        }
    }
}
