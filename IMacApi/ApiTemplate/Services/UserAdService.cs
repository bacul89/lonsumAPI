using ApiTemplate.Cores.Models;
using ApiTemplate.Identity;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ApiTemplate.Services
{
    public interface IUserAdService : ILoginProvider
    {
        //UserAd GetUser(string userName, string password);
        UserAd GetUser(string userName);
        Task<List<string>> GetAllUserMail(string mailSuffix);
    }
    public class UserAdService : IUserAdService
    {
        //private string _domainName = "192.168.129.58";
        //private string _containersUsers = "DC=mii,DC=corp";
        //private string _userName = "testDevelop";
        //private string _password = "P@ssw0rd";

        private string _domainName = "londonsumatra.com";
        private string _containersUsers = "OU=Lonsum Users,DC=londonsumatra,DC=com";
        private string _containers = "DC=londonsumatra,DC=com";
        private string _userName = "RoomAdmin";
        private string _password = "54321";

        public UserAd GetUser(string userName)
        {
            try
            {
                //TODO : uncomment di lonsum
                var userPrincipalName = $"{userName}@londonsumatra.com";
                //var userPrincipalName = userName;
                using (var context = new PrincipalContext(ContextType.Domain, this._domainName, this._containersUsers,
                    this._userName, this._password))
                {
                    var result = FindUser(context, userPrincipalName);
                    return result;

                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //public UserAd GetUser(string userName, string password)
        //{
        //    try
        //    {
        //        var userPrincipalName = $"{userName}@londonsumatra.com";
        //        using (var context = new PrincipalContext(ContextType.Domain, this._domainName, this._containersUsers,
        //            this._userName, this._password))
        //        {
        //            if (context.ValidateCredentials(userPrincipalName, password))
        //            {
        //                var result = FindUser(context, userPrincipalName);
        //                return result;
        //            }

        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }

        //    return null;
        //}
        private UserAd FindUser(PrincipalContext principalContext, string upn)
        {
            //if (upn.ToLower().Contains("guest")) //testforguestmii
            //{
            //    return new UserAd
            //    {
            //        Username = "Guest.MII",
            //        DisplayName = "Guest.MII",
            //        FirstName = "Guest",
            //        LastName = "MII",
            //        Mail = "Guest.MII@londonsumatra.com"
            //    };
            //}

            //TODO : uncomment di lonsum
            
            //return result;

            List<string> namagrup = new List<string>();
            var userContext = new UserPrincipalExtended(principalContext) { UserPrincipalName = upn };
            using (var user = UserPrincipal.FindByIdentity(principalContext, upn))
            {
                var usergrup = user.GetGroups();
               
                using (usergrup)
                {
                    foreach (Principal group in usergrup) // cycle through all the groups for this user
                    {
                        namagrup.Add(group.Name);
                    }
                }
            }

            using (var search = new PrincipalSearcher(userContext))
            {
                if (search.FindOne() is UserPrincipalExtended result)
                {
                    UserAd userModel = new UserAd();
                    userModel.Username = result.UserPrincipalName;
                    userModel.JobTitle = result.Title;
                    userModel.DisplayName = result.DisplayName;
                    userModel.FirstName = result.GivenName;
                    userModel.LastName = result.Surname;
                    userModel.Mail = result.EmailAddress;

                    bool a = namagrup.Contains("SG LSI ICT Administration & QA");
                    if (result.Title == "DMS3 Engineer")
                        userModel.Role = "Engineer";
                    else if (result.Title == "DMS3 Leader" || result.Title == "DMS3 Project Manager")
                        userModel.Role = "Leader";
                    //TODO : check grup SGLSIICTQA
                    else if (namagrup.Contains("SG LSI ICT Administration & QA"))
                        userModel.Role = "IT";
                    else if (result.EmailAddress.Contains("SGLSIICTQA"))
                        userModel.Role = "User";
                    else
                        userModel.Role = "";

                    return userModel;
                }
            }

            return null;
        }

        public Task<List<string>> GetAllUserMail(string mailSuffix = "")
        {
            var email = new List<string>();
            return Task.Run(() =>
            {
                using (var context = new PrincipalContext(ContextType.Domain, this._domainName, this._containersUsers,
                    this._userName, this._password))
                {
                    var userContext = new UserPrincipal(context) { EmailAddress = $"{mailSuffix.ToLower()}*" };
                    //var userContext = new UserPrincipal(context) { Description = "*Room*" };
                    using (var search = new PrincipalSearcher(userContext))
                    {
                        var res = search.FindAll();
                        foreach (var u in res)
                        {
                            var user = (UserPrincipal)u;
                            var d = user.Description;
                            if (!string.IsNullOrEmpty(user.EmailAddress))
                                email.Add(user.EmailAddress);

                        }

                        return email;
                    }
                }
            });


        }

        public bool ValidateCredentials(string userName, string password, out ClaimsIdentity identity, out string role)
        {
            role = "";

            using (var pc = new PrincipalContext(ContextType.Domain, this._domainName, userName, password))
            {
                var isValid = pc.ValidateCredentials(userName, password);
                if (isValid)
                {
                    var result = FindUser(pc, userName);
                    identity = new ClaimsIdentity(Startup.OAuthOptions.AuthenticationType);
                    identity.AddClaim(new Claim(ClaimTypes.Name, userName));

                    if (result.JobTitle.ToLower() == "dms3 engineer")
                    {
                        identity.AddClaim(new Claim(ClaimTypes.Role, "engineer"));
                        role = "engineer";
                    }
                    else if (result.JobTitle.ToLower().Contains("dms3 leader") ||
                             result.JobTitle.ToLower().Contains("dms3 project manager"))
                    {
                        identity.AddClaim(new Claim(ClaimTypes.Role, "leader"));
                        role = "leader";
                    }
                    else
                    {
                        identity.AddClaim(new Claim(ClaimTypes.Role, "user"));
                        role = "user";
                    }

                }
                else
                {
                    identity = null;
                }

                return isValid;
            }
        }
    }
}
