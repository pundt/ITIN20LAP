using innovation4austria.data;
using innovation4austria.logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace innovation4austria.authentication
{
    class i4aRoleProvider : RoleProvider
    {
        string applicationName = "innovation4austria database";
        public override string ApplicationName
        {
            get
            {
                return applicationName;
            }

            set
            {
                if (!string.IsNullOrEmpty(value))
                    applicationName = value;
            }
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            List<User> roleUsers = RoleAdministration.GetRoleUsers(roleName);
            if (roleUsers != null)
            {
                return roleUsers.Where(x => x.Username.Contains(usernameToMatch)).Select(x => x.Username).ToArray();
            }
            else
            {
                return null;
            }
        }

        public override string[] GetAllRoles()
        {
            List<Role> allRoles = RoleAdministration.GetRoles();

            if (allRoles != null)
            {
                return allRoles.Select(x => x.Name).ToArray();
            }
            else
            {
                return null;
            }
        }

        public override string[] GetRolesForUser(string username)
        {
            Role userRoles = RoleAdministration.GetUserRole(username);

            if (userRoles != null)
            {
                return new string[] { userRoles.Name };
            }
            else
            {
                return null;
            }
        }

        public override string[] GetUsersInRole(string roleName)
        {

            List<User> roleUsers = RoleAdministration.GetRoleUsers(roleName);
            if (roleUsers != null)
            {
                return roleUsers.Select(x => x.Username).ToArray();
            }
            else
            {
                return null;
            }
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            string[] userRoles = GetRolesForUser(username);
            if (userRoles != null)
            {
                return userRoles.Contains(roleName);
            }
            else
            {
                return false;
            }
        }

        public override bool RoleExists(string roleName)
        {
            string[] allRoles = GetAllRoles();
            if (allRoles != null)
            {
                return allRoles.Contains(roleName);
            }
            else
            {
                return false;
            }
        }

        #region NotImplementedMember
        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }
        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
