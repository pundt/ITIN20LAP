using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using innovation4austria.data;
using log4net;

namespace innovation4austria.logic
{

    public class CompanyAdministration
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// returns a list of all companies
        /// </summary>
        /// <exception cref="Exception">if database access fails</exception>
        /// <returns>list of all active companies</returns>
        public static List<Company> GetCompanies()
        {
            log.Info("GetCompanies()");
            List<Company> allCompanies = null;

            try
            {
                using (var context = new innovation4austriaEntities())
                {
                    allCompanies = context.AllCompanies.ToList();
                }
            }
            catch (Exception ex)
            {
                log.Error("Exception in GetCompanies", ex);
                if (ex.InnerException != null)
                    log.Error("Exception in GetCompanies (inner)", ex.InnerException);
                throw;
            }

            return allCompanies;
        }

        /// <summary>
        /// returns company with the given id
        /// </summary>
        /// <param name="id">the id to look up for</param>
        /// <returns>company with given id or null in case of an erro</returns>
        /// <exception cref="ArgumentException">in case of an invalid id</exception>
        public static Company GetCompany(int id)
        {
            log.Info("GetCompany(id)");

            if (id <= 0)
                throw new ArgumentException("Invalid value for id", nameof(id));
            else
            {
                Company company = null;

                try
                {
                    using (var context = new innovation4austriaEntities())
                    {
                        company = context.AllCompanies.FirstOrDefault(x => x.ID == id);
                    }
                }
                catch (Exception ex)
                {
                    log.Error("Exception in GetCompany", ex);
                    if (ex.InnerException != null)
                        log.Error("Exception in GetCompany (inner)", ex.InnerException);
                    throw;
                }
                
                return company;
            }
        }

        public static bool SaveCompanyData(int companyId, string companyName, string street, string number, string zip, string city)
        {
            log.Info("SaveCompanyData(companyId, companyName, street, number, zip, city)");
            bool result = false;

            if (string.IsNullOrEmpty(companyName))
                throw new ArgumentNullException($"{nameof(companyName)} is null or empty");
            if (string.IsNullOrEmpty(street))
                throw new ArgumentNullException($"{nameof(street)} is null or empty");
            if (string.IsNullOrEmpty(number))
                throw new ArgumentNullException($"{nameof(number)} is null or empty");
            if (companyId<=0)
                throw new ArgumentNullException($"{nameof(companyId)} invalid");

            using (var context = new innovation4austriaEntities())
            {
                try
                {
                    Company curCompany = context.AllCompanies.FirstOrDefault(x => x.ID == companyId);
                    if (curCompany!=null)
                    {
                        curCompany.Name = companyName;
                        curCompany.Street = street;
                        curCompany.Number = number;
                        curCompany.Zip = zip;
                        curCompany.City = city;
                        context.SaveChanges();
                        result = true;
                    } 
                }
                catch (Exception ex)
                {
                    log.Error("Exception in SaveCompanyData", ex);
                    if (ex.InnerException != null)
                        log.Error("Exception in SaveCompanyData (inner)", ex.InnerException);
                    throw;
                }
            }

            return result;
        }
    }
}
