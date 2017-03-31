using innovation4austria.data;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace innovation4austria.logic
{
    public class TypeAdministration
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        public static List<data.Type> GetRoomTypes()
        {
            log.Info("GetRoomTypes()");
            List<data.Type> types = null;

            try
            {
                using (var context = new innovation4austriaEntities())
                {
                    types = context.AllTypes.Where(x => x.Active).OrderBy(x => x.Name).ToList();
                }
            }
            catch (Exception ex)
            {
                log.Error("Exception in GetRoomTypes()", ex);
                if (ex.InnerException != null)
                    log.Error("Exception in GetRoomTypes (inner)", ex.InnerException);
                throw;
            }

            return types;
        }
    }
}
