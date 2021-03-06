﻿using innovation4austria.data;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace innovation4austria.logic
{
    public enum LogonResult
    {
        LogonDataValid,
        LogonDataInvalid,
        UserInactive,
        UnkownUser,
    }

    public enum PasswordChangeResult
    {
        Success,
        UserInactive,
        UsernameInvalid,
        PasswortInvalid
    }

    public enum ProfileChangeResult
    {
        Success,
        UserInactive,
        UsernameInvalid
    }

    public class UserAdministration
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static PasswordChangeResult ChangePassword(string username, string oldPassword, string newPassword)
        {
            PasswordChangeResult result = PasswordChangeResult.UsernameInvalid;

            log.Info("ChangePassword(username, oldPassword, newPassword)");

            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException(nameof(username));
            else if (string.IsNullOrEmpty(newPassword))
                throw new ArgumentNullException(nameof(newPassword));
            else if (string.IsNullOrEmpty(oldPassword))
                throw new ArgumentNullException(nameof(oldPassword));
            else
            {
                using (var context = new innovation4austriaEntities())
                {
                    try
                    {
                        User curUser = context.AllUsers.Where(x => x.Username == username).FirstOrDefault();

                        if (curUser == null)
                        {
                            result = PasswordChangeResult.UsernameInvalid;
                        }
                        else if (!curUser.Active)
                        {
                            result = PasswordChangeResult.UserInactive;
                        }
                        else if (!curUser.Password.SequenceEqual(Helper.GetSHA2(oldPassword)))
                        {
                            result = PasswordChangeResult.PasswortInvalid;
                        }
                        else
                        {
                            log4net.LogicalThreadContext.Properties["idUser"] = curUser.ID;

                            curUser.Password = Helper.GetSHA2(newPassword);
                            context.SaveChanges();

                            result = PasswordChangeResult.Success;
                            log.Info("Changed password successfully!");
                        }
                    }
                    catch (Exception ex)
                    {
                        log.Error("Exception in ChangePassword", ex);
                        if (ex.InnerException != null)
                            log.Error("Exception in ChangePassword (inner)", ex.InnerException);
                        throw;
                    }
                }
            }
            return result;
        }

        public static PasswordChangeResult ChangePassword(int id, string oldPassword, string newPassword)
        {
            PasswordChangeResult result = PasswordChangeResult.UsernameInvalid;

            log.Info("ChangePassword(id, oldPassword, newPassword)");

            if (id<=0)
                throw new ArgumentException($"Invalid {nameof(id)}");
            else if (string.IsNullOrEmpty(newPassword))
                throw new ArgumentNullException(nameof(newPassword));
            else if (string.IsNullOrEmpty(oldPassword))
                throw new ArgumentNullException(nameof(oldPassword));
            else
            {
                using (var context = new innovation4austriaEntities())
                {
                    try
                    {
                        User curUser = context.AllUsers.Where(x => x.ID == id).FirstOrDefault();

                        if (curUser == null)
                        {
                            result = PasswordChangeResult.UsernameInvalid;
                        }
                        else if (!curUser.Active)
                        {
                            result = PasswordChangeResult.UserInactive;
                        }
                        else if (!curUser.Password.SequenceEqual(Helper.GetSHA2(oldPassword)))
                        {
                            result = PasswordChangeResult.PasswortInvalid;
                        }
                        else
                        {
                            log4net.LogicalThreadContext.Properties["idUser"] = curUser.ID;

                            curUser.Password = Helper.GetSHA2(newPassword);
                            context.SaveChanges();

                            result = PasswordChangeResult.Success;
                            log.Info("Changed password successfully!");
                        }
                    }
                    catch (Exception ex)
                    {
                        log.Error("Exception in ChangePassword", ex);
                        if (ex.InnerException != null)
                            log.Error("Exception in ChangePassword (inner)", ex.InnerException);
                        throw;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Saves new first- and lastname for the given userName
        /// </summary>
        /// <param name="username">the user to change data for</param>
        /// <param name="firstname">new firstname</param>
        /// <param name="lastname">new lastname</param>
        /// <returns><see cref="ProfileChangeResult"/> SUCCESS if information could be saved, otherelse corresponding error member</returns>
        /// <exception cref="Exception">in case saving information fails or unknown user</exception>
        /// <exception cref="ArgumentNullException">if user-, first- or lastname is null or empty</exception>
        public static ProfileChangeResult SaveProfileData(string username, string firstname, string lastname)
        {
            log.Info("SaveProfileData(username, firstname, lastname)");
            ProfileChangeResult result = ProfileChangeResult.UsernameInvalid;

            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException($"{nameof(username)} is null or empty");
            if (string.IsNullOrEmpty(firstname))
                throw new ArgumentNullException($"{nameof(firstname)} is null or empty");
            if (string.IsNullOrEmpty(lastname))
                throw new ArgumentNullException($"{nameof(lastname)} is null or empty");

            using (var context = new innovation4austriaEntities())
            {
                try
                {
                    User currentUser = context.AllUsers.FirstOrDefault(x => x.Username == username);
                    if (currentUser != null)
                    {
                        if (currentUser.Active)
                        {
                            currentUser.FirstName = firstname;
                            currentUser.LastName = lastname;
                            context.SaveChanges();
                            log.Info("Profile Data changed successfully!");
                            result = ProfileChangeResult.Success;
                        }
                        else
                        {
                            log.Info("SaveProfileData - UserInactive");
                            result = ProfileChangeResult.UserInactive;
                        }
                    }
                    else
                    {
                        log.Info("SaveProfileData - UsernameInvalid");
                        result = ProfileChangeResult.UsernameInvalid;
                    }
                }
                catch (Exception ex)
                {
                    log.Error("Exception in SaveProfileData", ex);
                    if (ex.InnerException != null)
                        log.Error("Exception in SaveProfileData (inner)", ex.InnerException);
                    throw;
                }
            }

            return result;
        }

        /// <summary>
        /// Saves new first- and lastname for the given userName
        /// </summary>
        /// <param name="username">the user to change data for</param>
        /// <param name="firstname">new firstname</param>
        /// <param name="lastname">new lastname</param>
        /// <returns><see cref="ProfileChangeResult"/> SUCCESS if information could be saved, otherelse corresponding error member</returns>
        /// <exception cref="Exception">in case saving information fails or unknown user</exception>
        /// <exception cref="ArgumentNullException">if user-, first- or lastname is null or empty</exception>
        public static ProfileChangeResult SaveEmployeeData(int idEmployee, string firstname, string lastname)
        {
            log.Info("SaveProfileData(username, firstname, lastname)");
            ProfileChangeResult result = ProfileChangeResult.UsernameInvalid;

            if (idEmployee<=0)
                throw new ArgumentException($"Invalid {nameof(idEmployee)} ");
            if (string.IsNullOrEmpty(firstname))
                throw new ArgumentNullException($"{nameof(firstname)} is null or empty");
            if (string.IsNullOrEmpty(lastname))
                throw new ArgumentNullException($"{nameof(lastname)} is null or empty");

            using (var context = new innovation4austriaEntities())
            {
                try
                {
                    User currentUser = context.AllUsers.FirstOrDefault(x => x.ID == idEmployee);
                    if (currentUser != null)
                    {
                        if (currentUser.Active)
                        {
                            currentUser.FirstName = firstname;
                            currentUser.LastName = lastname;
                            context.SaveChanges();
                            log.Info("Employee Data changed successfully!");
                            result = ProfileChangeResult.Success;
                        }
                        else
                        {
                            log.Info("SaveEmployeeData - UserInactive");
                            result = ProfileChangeResult.UserInactive;
                        }
                    }
                    else
                    {
                        log.Info("SaveEmployeeData - UsernameInvalid");
                        result = ProfileChangeResult.UsernameInvalid;
                    }
                }
                catch (Exception ex)
                {
                    log.Error("Exception in SaveEmployeeData", ex);
                    if (ex.InnerException != null)
                        log.Error("Exception in SaveEmployeeData (inner)", ex.InnerException);
                    throw;
                }
            }

            return result;
        }

        /// <summary>
        /// if given id belongs to a company user, it will be deactivated
        /// otherelse an exception will be thrown
        /// </summary>
        /// <param name="idUser">id for a given company user</param>
        /// <exception cref="Exception">in case data access failed</exception>
        /// <returns>true if deactivation was successfull, otherelse false</returns>
        public static bool DeleteCompanyUser(int idUser)
        {
            bool deleteSuccessful = false;

            try
            {
                User user = null;
                using (var context = new innovation4austriaEntities())
                {
                    user = context.AllUsers.FirstOrDefault(x => x.ID == idUser && x.ID_Role == 2);
                }
                if (user!=null)
                {
                    deleteSuccessful = DeactivateUser(user.Username);
                } else
                {
                    log.Info($"idUser {idUser} is no startup User");
                }
            }
            catch (Exception ex) 
            {
                log.Error("Exception in DeleteCompanyUser", ex);
                if (ex.InnerException != null)
                    log.Error("Exception in DeleteCompanyUser (inner)", ex.InnerException);
                throw;
            }

            return deleteSuccessful;
        }

        public static bool CreateCompanyUser(string username, string password, string firstname, string lastname, int idCompany)
        {
            bool created = false;

            try
            {
                using (var context = new innovation4austriaEntities())
                {
                    User user = new User()
                    {
                        Username = username,
                        Password = Helper.GetSHA2(password),
                        FirstName = firstname,
                        LastName = lastname,
                        ID_Company = idCompany,
                        ID_Role = 2, // startup, company
                        Active = true
                    };
                    context.AllUsers.Add(user);
                    context.SaveChanges();
                    created = true;
                }
            }
            catch (Exception ex)
            {
                log.Error("Exception in CreateCompanyUser", ex);
                if (ex.InnerException != null)
                    log.Error("Exception in CreateCompanyUser (inner)", ex.InnerException);
                throw;
            }

            return created;
        }

        public static User GetUser(string username)
        {
            log.Info("GetUser(username)");

            User user = null;

            using (var context = new innovation4austriaEntities())
            {
                try
                {
                    user = context.AllUsers.Where(x => x.Username == username).FirstOrDefault();

                    if (user == null)
                    {
                        log.Info("Unknown username!");
                    }
                }
                catch (Exception ex)
                {
                    log.Error("Exception in GetUser", ex);
                    if (ex.InnerException != null)
                        log.Error("Exception in GetUser (inner)", ex.InnerException);
                    throw;
                }
            }

            return user;
        }

        public static User GetUser(int id)
        {
            log.Info("GetUser(id)");

            User user = null;

            using (var context = new innovation4austriaEntities())
            {
                try
                {
                    user = context.AllUsers.Where(x => x.ID == id).FirstOrDefault();

                    if (user == null)
                    {
                        log.Info("Unknown ID!");
                    }
                }
                catch (Exception ex)
                {
                    log.Error("Exception in GetUser", ex);
                    if (ex.InnerException != null)
                        log.Error("Exception in GetUser (inner)", ex.InnerException);
                    throw;
                }
            }

            return user;
        }

        public static bool DeactivateUser(string username)
        {
            log.Info("DeactivateUser(username)");
            bool success = false;

            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentNullException(nameof(username));
            }
            else
            {
                using (var context = new innovation4austriaEntities())
                {
                    try
                    {
                        User curUser = context.AllUsers.Where(x => x.Username == username).FirstOrDefault();

                        if (curUser != null)
                        {
                            curUser.Active = false;
                            context.SaveChanges();
                            success = true;
                            log.Info("User has been deactivated!");
                        }
                        else
                        {
                            log.Info("Unknown username");
                        }
                    }
                    catch (Exception ex)
                    {
                        log.Error("Exception in DeactivateUser", ex);
                        if (ex.InnerException != null)
                            log.Error("Exception in DeactivateUser (inner)", ex.InnerException);
                        throw;
                    }
                }
            }
            return success;
        }

        public static bool ActivateUser(string username)
        {
            log.Info("ActivateUser(username)");
            bool success = false;

            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentNullException(nameof(username));
            }
            else
            {
                using (var context = new innovation4austriaEntities())
                {
                    try
                    {
                        User curUser = context.AllUsers.Where(x => x.Username == username).FirstOrDefault();

                        if (curUser != null)
                        {
                            curUser.Active = true;
                            context.SaveChanges();
                            success = true;
                            log.Info("User has been deactivated!");
                        }
                        else
                        {
                            log.Info("Unknown username");
                        }
                    }
                    catch (Exception ex)
                    {
                        log.Error("Exception in DeactivateUser", ex);
                        if (ex.InnerException != null)
                            log.Error("Exception in DeactivateUser (inner)", ex.InnerException);
                        throw;
                    }
                }
            }
            return success;
        }

        public static LogonResult Logon(string username, string password)
        {
            log.Info("Logon(username, password)");
            LogonResult result = LogonResult.LogonDataInvalid;

            if (string.IsNullOrEmpty(username))
            {
                log.Error("Username is empty!");
                throw new ArgumentNullException(nameof(username));
            }
            else if (string.IsNullOrEmpty(password))
            {
                log.Error("Password is empty!");
                throw new ArgumentNullException(nameof(password));
            }
            else
            {
                using (var context = new innovation4austriaEntities())
                {
                    try
                    {
                        User user = context.AllUsers.Where(x => x.Username == username).FirstOrDefault();

                        if (user != null)
                        {
                            if (user.Password.SequenceEqual(Helper.GetSHA2(password)))
                            {
                                if (!user.Active)
                                {
                                    log.Info("User inactive");
                                    result = LogonResult.UserInactive;
                                }
                                else
                                {
                                    log.Info("Logon data valid");
                                    result = LogonResult.LogonDataValid;
                                }
                            }
                            else
                            {
                                log.Info("Logon data invalid");
                                result = LogonResult.LogonDataInvalid;
                            }

                            int anzahlZeilen = context.SaveChanges();
                        }
                        else
                        {
                            result = LogonResult.UnkownUser;
                            log.Info("Unknown username");
                        }
                    }
                    catch (Exception ex)
                    {
                        log.Error("Exception in Logon", ex);
                        if (ex.InnerException != null)
                            log.Error("Exception in Logon (inner)", ex.InnerException);
                        throw;
                    }
                }
            }
            return result;
        }

        public static List<User> GetCompanyUsers(int companyId)
        {
            log.Info("GetCompanyUsers(companyId)");

            List<User> companyUsers = null;

            using (var context = new innovation4austriaEntities())
            {
                try
                {
                    var company = context.AllCompanies.FirstOrDefault(x => x.ID == companyId);

                    if (company != null)
                        companyUsers = company.AllUsers.ToList();
                    else
                        throw new ArgumentException("Invalid companyId", nameof(companyId));

                }
                catch (Exception ex)
                {
                    log.Error("Exception in GetCompanyUsers", ex);
                    if (ex.InnerException != null)
                        log.Error("Exception in GetCompanyUsers (inner)", ex.InnerException);
                    throw;
                }
            }

            return companyUsers;
        }
    }

}