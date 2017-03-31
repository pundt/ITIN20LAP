using innovation4austria.data;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace innovation4austria.logic
{
    public class BuildingAdministration
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static List<Building> GetBuildings()
        {
            log.Info("GetBuildings()");
            List<Building> result = null;

            using (var context = new innovation4austriaEntities())
            {
                try
                {
                    result = context.AllBuildings.Where(x => x.Active).OrderBy(x => x.Order).ToList();
                }
                catch (Exception ex)
                {
                    log.Error("Exception in GetBuildings", ex);
                    if (ex.InnerException != null)
                        log.Error("Exception in GetBuildings (inner)", ex.InnerException);
                    throw;
                }
            }

            return result;
        }

    }
}
