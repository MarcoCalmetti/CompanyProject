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

        public async static Task<string> checkReseller(string BusinessName, string VAT, string Address, string City, string PostalCode, string Mail, string TelephoneNumber)
        {
            using (CompanyContext context = new CompanyContext())
            {
                try
                {
                    if (await context.Resellers.Select(s => s).Where(s => BusinessName != null ? s.BusinessName.Contains(BusinessName) : true).CountAsync() > 0)
                        return "There is already another company name with the same name";
                
                    if(await context.Resellers.Select(s => s).Where(s => BusinessName != null ? s.BusinessName.Contains(BusinessName) : true).CountAsync() > 0)
                        return "There is already another company with the same VAT";

                    if (await context.Resellers.Select(s => s).Where(s => BusinessName != null ? s.BusinessName.Contains(BusinessName) : true).CountAsync() > 0)
                        return "There is already another company with the same VAT";

                    return null;
                }
                catch(Exception e)
                {
                    throw;
                }
                
            }
        }

        public static bool EmailFormatChecker(string email)
        {
            if(email.Contains("@") && email.Contains(".") && email.Substring(0,email.IndexOf("@")).Length > 0 && )
        }
    }
}
