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
    static class ResellersController
    {
        public async static Task<int> GetResellerNumber(string BusinessName, string VAT, string City)
        {
            try
            {
                using (CompanyContext context = new CompanyContext())
                {
                    return await context.Resellers
                        .Where(s => !String.IsNullOrEmpty(BusinessName) ? s.BusinessName.Contains(BusinessName) : true)
                        .Where(s => !String.IsNullOrEmpty(VAT) ? s.VAT.Contains(VAT) : true)
                        .Where(s => !String.IsNullOrEmpty(City) ? s.City == City : true)
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
        public async static Task<List<string>> GetAllCity()
        {
            try
            {
                using (CompanyContext context = new CompanyContext())
                {
                    var x = await context.Resellers
                        .Select(s => s.City)
                        .Distinct()
                        .ToListAsync();

                    return x;
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
        public async static Task<List<Reseller>> GetAll(string BusinessName, string VAT, string City, int page, int pageSize)
        {
            try
            {
                using (CompanyContext context = new CompanyContext())
                {
                    var x = await context.Resellers
                        .Where(s => !String.IsNullOrEmpty(BusinessName) ? s.BusinessName.Contains(BusinessName) : true)
                        .Where(s => !String.IsNullOrEmpty(VAT) ? s.VAT.Contains(VAT) : true)
                        .Where(s => !String.IsNullOrEmpty(City) ? s.City == City : true)
                        .OrderBy(s=>s.ResellerID)
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();

                    return x;
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

        public async static Task Add(Reseller r)
        {
            try
            {
                using (CompanyContext context = new CompanyContext())
                {
                    context.Resellers.Add(r);
                    await context.SaveChangesAsync();
                }
            }catch(ArgumentException e)
            {
                throw e;        
            }catch(Exception e)
            {
                throw e;
            }
        }

        private async static Task checkReseller(string BusinessName, string VAT, string Mail, string TelephoneNumber)
        {
            try
            {
                using (CompanyContext context = new CompanyContext())
                {
                    if (await context.Resellers.Select(s => s).Where(s => BusinessName != null ? s.BusinessName.Contains(BusinessName) : true).CountAsync() > 0)
                        throw new ArgumentException("There is already another company  with the same Business name");

                    if (await context.Resellers.Select(s => s).Where(s => VAT != null ? s.BusinessName.Contains(VAT) : true).CountAsync() > 0)
                        throw new ArgumentException("There is already another company with the same VAT");

                    if (await context.Resellers.Select(s => s).Where(s => Mail != null ? s.BusinessName.Contains(Mail) : true).CountAsync() > 0)
                        throw new ArgumentException("There is already another company with the same Mail");

                    if (await context.Resellers.Select(s => s).Where(s => TelephoneNumber != null ? s.BusinessName.Contains(TelephoneNumber) : true).CountAsync() > 0)
                        throw new ArgumentException("There is already another company with the Telephone Number ");
                }
            } catch (ArgumentException e)
            {
                throw e;
            } catch(Exception e)
            {
                throw e;
            }
        }

        public async static Task Update(Reseller ResellerToUpdate)
        {
            using (CompanyContext context = new CompanyContext())
            {
                var temp = context.Resellers.FirstOrDefaultAsync(r => r.ResellerID == ResellerToUpdate.ResellerID);
                temp.Result.Address = ResellerToUpdate.Address;
                temp.Result.BusinessName = ResellerToUpdate.BusinessName;
                temp.Result.City = ResellerToUpdate.BusinessName;
                temp.Result.Mail = ResellerToUpdate.Mail;
                temp.Result.OrderHeaders = ResellerToUpdate.OrderHeaders;
                temp.Result.PostalCode = ResellerToUpdate.PostalCode;
                temp.Result.TelephoneNumber = ResellerToUpdate.TelephoneNumber;
                temp.Result.VAT = ResellerToUpdate.VAT;
                await context.SaveChangesAsync();
            }
        }

        public async static Task<bool> Delete(Reseller ResellerToDelete)
        {
            try
            {
                if(await checkForDelete(ResellerToDelete))
                {
                    using (CompanyContext context = new CompanyContext())
                    {
                        context.Resellers.Remove(ResellerToDelete);
                        await context.SaveChangesAsync();
                        return true;
                    }
                }
                else
                {
                    throw new ArgumentException("It is not possible to delete a reseller with linked orders");
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

        private async static Task<bool> checkForDelete(Reseller ResellerToDelete)
        {
            try
            {
                using (CompanyContext context = new CompanyContext())
                {
                    if (await context.Resellers.CountAsync(r=>r.ResellerID == ResellerToDelete.ResellerID) > 0)
                        return true;
                    return false;
                }
            }catch(ArgumentException e)
            {
                throw e;
            }catch(Exception e)
            {
                throw e;
            }   
        }
    }
}
