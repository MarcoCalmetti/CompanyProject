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
            using (CompanyContext context = new CompanyContext())
            {
                try
                {
                    checkReseller(BusinessName, VAT, Mail, TelephoneNumber);
                    context.Resellers.Add(
                        new Reseller
                        {
                            BusinessName = BusinessName,
                            VAT = VAT,
                            Address = Address,
                            City = City,
                            PostalCode = PostalCode,
                            Mail = Mail,
                            TelephoneNumber = TelephoneNumber
                        });
                }catch(ArgumentException e)
                {
                    throw e;        
                }
            }
        }

        public async static Task checkReseller(string BusinessName, string VAT, string Mail, string TelephoneNumber)
        {
            using (CompanyContext context = new CompanyContext())
            {
                try
                {
                    if (await context.Resellers.Select(s => s).Where(s => BusinessName != null ? s.BusinessName.Contains(BusinessName) : true).CountAsync() > 0)
                        throw new ArgumentException "There is already another company  with the same Business name";
                
                    if(await context.Resellers.Select(s => s).Where(s => VAT != null ? s.BusinessName.Contains(VAT) : true).CountAsync() > 0)
                        throw new ArgumentException "There is already another company with the same VAT";

                    if(!TelephoneNumber.All(char.IsDigit))
                        throw new ArgumentException "The telephone format is not correct";

                    if(!EmailFormatChecker(email))
                        throw new ArgumentException "The email format is not correct";
                    
                    if (await context.Resellers.Select(s => s).Where(s => Mail != null ? s.BusinessName.Contains(Mail) : true).CountAsync() > 0)
                        throw new ArgumentException "There is already another company with the same Mail";

                    if (await context.Resellers.Select(s => s).Where(s => TelephoneNumber != null ? s.BusinessName.Contains(TelephoneNumber) : true).CountAsync() > 0)
                        throw new ArgumentException "There is already another company with the Telephone Number ";
                }
                catch(ArgumentException e)
                {
                    throw e;
                }
                
            }
        }

        public static bool EmailFormatChecker(string email)
        {
            if (email.Contains("@") && email.Substring(0, email.IndexOf("@")).Length > 0 && email.Substring(email.IndexOf("@"), email.Length - 1).Contains("."))
                return true;
            return false
        }


    }
}
