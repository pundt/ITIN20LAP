using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using innovation4austria.data;
using log4net;

namespace innovation4austria.logic
{
    public class RoleAdministration
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static List<User> GetRoleUsers(string roleName)
        {
            log.Info("GetRoleUsers(rolenName)");

            if (string.IsNullOrEmpty(roleName))
            {
                throw new ArgumentNullException(nameof(roleName));
            }
            else
            {
                List<User> roleUsers = null;

                using (var context = new innovation4austriaEntities())
                {
                    try
                    {
                        Role aktRolle = context.AllRoles.Where(x => x.Name == roleName).FirstOrDefault();
                        if (aktRolle != null)
                        {
                            roleUsers = aktRolle.AllUsers.Where(x => x.Active).ToList();
                        }
                    }
                    catch (Exception ex)
                    {
                        log.Error("Exception in GetRoleUsers", ex);
                        if (ex.InnerException != null)
                            log.Error("Exception in GetRoleUsers (inner)", ex.InnerException);
                        throw;
                    }
                }

                return roleUsers;
            }
        }

        public static List<Role> GetRoles()
        {
            log.Info("GetRoles()");
            List<Role> rollen = null;

            using (var context = new innovation4austriaEntities())
            {
                try
                {
                    rollen = context.AllRoles.Where(x => x.Active).ToList();
                }
                catch (Exception ex)
                {
                    log.Error("Exception in GetRoles", ex);
                    if (ex.InnerException != null)
                        log.Error("Exception in GetRoles (inner)", ex.InnerException);
                    throw;
                }
            }

            return rollen;
        }

        public static Role GetUserRole(string username)
        {
            log.Info("GetUserRoles(username)");

            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentNullException(nameof(username));
            }
            else
            {
                Role userRole = null;

                using (var context = new innovation4austriaEntities())
                {
                    try
                    {
                        User aktBenutzer = context.AllUsers.Where(x => x.Username == username).FirstOrDefault();
                        if (aktBenutzer != null)
                        {
                            userRole = aktBenutzer.Role;
                        }
                    }
                    catch (Exception ex)
                    {
                        log.Error("Exception in GetUserRole", ex);
                        if (ex.InnerException != null)
                            log.Error("Exception in GetUserRole (inner)", ex.InnerException);
                        throw;
                    }
                }

                return userRole;
            }
        }
    }
}
