using innovation4austria.data;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace innovation4austria.logic
{
    public class FacilityAdministration
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static byte[] GetFacilityImage(int id, bool active)
        {
            log.Info("GetFacilityImage(int id)");
            byte[] image = null;

            try
            {
                using (var context = new innovation4austriaEntities())
                {
                    Facility facility = context.AllFacilities.Where(x => x.ID == id).FirstOrDefault();

                    if (facility != null)
                    {
                        if (active)
                        {
                            image = facility.ImageActive;
                        }
                        else
                        {
                            image = facility.Image;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Exception in GetFacilityImage(int id)", ex);
                if (ex.InnerException != null)
                    log.Error("Exception in GetFacilityImage(int id) - (inner)", ex.InnerException);
                throw;
            }

            return image;
        }

        public static List<Facility> GetFacilities()
        {
            log.Info("GetFacilities()");
            List<Facility> facilities = null;

            try
            {
                using (var context = new innovation4austriaEntities())
                {
                    facilities = context.AllFacilities.Where(x => x.Active).OrderBy(x => x.Order).ToList();
                }
            }
            catch (Exception ex)
            {
                log.Error("Exception in GetFacilities()", ex);
                if (ex.InnerException != null)
                    log.Error("Exception in GetFacilities (inner)", ex.InnerException);
                throw;
            }

            return facilities;
        }
    }
}
