using innovation4austria.data;
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
                            log.Info("Passwort aufgrund altem Passwort erfolgreich geändert!");
                        }
                    }
                    catch (Exception ex)
                    {
                        log.Error("Fehler bei BenutzerPasswortÄndern", ex);
                        if (ex.InnerException != null)
                            log.Error("Fehler bei BenutzerPasswortÄndern (inner)", ex.InnerException);
                        throw;
                    }
                }
            }
            return result;
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

                    if (user== null)
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
    }

}