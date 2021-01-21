using ApiTemplate.Cores.Models;
using ApiTemplate.Extensions;
using ApiTemplate.Requests;
using ApiTemplate.Responses;
using ApiTemplate.Services;
using Serilog;
using System;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;

namespace ApiTemplate.Controllers
{
    [RoutePrefix("api/v1/userad")]
    public class UserADController : ApiController
    {
        private readonly ILogger _logger;
        private readonly IUserAdService _userADService;
        public UserADController(ILogger Logger, IUserAdService UserADService)
        {
            _logger = Logger;
            _userADService = UserADService;
        }
        //TODO Uncoment setelah WFH
        [Authorize]
        [HttpPost, Route("getuserad")]
        public HttpResponseMessage Get([FromBody]LoginRequest request)
        {
            var response = new SingleResponse<UserAd>();

            try
            {
                UserIdentity result = null;
                UserAd userPost = new UserAd();
                var identity = (ClaimsIdentity)User.Identity;
                var claims = identity.Claims.ToList();
                if (claims.Any())
                {
                    result = new UserIdentity
                    {
                        LoginId = identity?.Name
                    };


                    userPost.Username = identity?.Name;
                    userPost.Mail = claims.Where(d => d.Type.Equals(ClaimTypes.Email,
                        StringComparison.OrdinalIgnoreCase)).Select(x => x.Value).FirstOrDefault();
                    userPost.JobTitle = claims.Where(d => d.Type.Equals("jobdescription",
                        StringComparison.OrdinalIgnoreCase)).Select(x => x.Value).FirstOrDefault();
                    userPost.DisplayName = claims.Where(d => d.Type.Equals(ClaimTypes.GivenName,
                        StringComparison.OrdinalIgnoreCase)).Select(x => x.Value).FirstOrDefault();
                    if (userPost.JobTitle == ConfigurationManager.AppSettings["RoleEngineer"])
                        userPost.Role = "Engineer";
                    else if (userPost.JobTitle.ToLower() == ConfigurationManager.AppSettings["RoleEngineerLeader"] ||
                        userPost.JobTitle.ToLower() == ConfigurationManager.AppSettings["RoleEngineerLeader2"])
                        userPost.Role = "Leader";
                    //TODO : check grup SGLSIICTQA
                    else if (claims.Where(d => d.Type.Equals("memberofgroup",
                        StringComparison.OrdinalIgnoreCase)).Any(x => x.Value.Contains(ConfigurationManager.AppSettings["RoleIT"])))
                        userPost.Role = "IT";
                    //TODO : rubah menjadi tidak memiliki contain DMS3
                    else if (!userPost.Mail.Contains("DMS3"))
                        userPost.Role = "User";
                    else
                        userPost.Role = "";
                    //    //var userid = claims.Where(d => d.Type.Equals("employeeid",
                    //    //    StringComparison.OrdinalIgnoreCase)).Select(x => x.Value).FirstOrDefault();
                    //    //foreach (var item in claims)
                    //    //{
                    //    //    if (item.Type.Equals("department", StringComparison.OrdinalIgnoreCase))
                    //    //        result.Department = item.Value;

                    //    //    if (item.Type.Equals("employeeid", StringComparison.OrdinalIgnoreCase))
                    //    //        result.EmployeeId = item.Value;

                    //    //    if (item.Type.Equals("jobdescription", StringComparison.OrdinalIgnoreCase))
                    //    //        result.JobDescription = item.Value;

                    //    //    if (item.Type.Equals("company", StringComparison.OrdinalIgnoreCase))
                    //    //        result.Company = item.Value;

                    //    //    if (item.Type.Equals("office", StringComparison.OrdinalIgnoreCase))
                    //    //        result.Office = item.Value;

                    //    //    if (item.Type.Equals(ClaimTypes.Email, StringComparison.OrdinalIgnoreCase))
                    //    //        result.EmailAddress = item.Value;

                    //    //    if (item.Type.Equals(ClaimTypes.HomePhone, StringComparison.OrdinalIgnoreCase))
                    //    //        result.TelpNo = item.Value;

                    //    //    if (item.Type.Equals(ClaimTypes.DateOfBirth, StringComparison.OrdinalIgnoreCase))
                    //    //        result.DoB = Convert.ToDateTime(item.Value);

                    //    //    if (item.Type.Equals("memberofgroup", StringComparison.OrdinalIgnoreCase))
                    //    //        result.MemberOfGroup = item.Value.Split(',').ToList();

                    //    //}
                }

                ////TODO: user dev FWH
                //    if (request.Username.ToLower() == "guest.mii@londonsumatra.com")
                //{
                //    userPost.Username = "guest.mii@londonsumatra.com";
                //    userPost.Mail = "guest.mii@londonsumatra.com";
                //    userPost.DisplayName = "Guest MII";
                //    userPost.Role = "User";
                //}
                //else if (request.Username.ToLower() == "guest.mii2@londonsumatra.com")
                //{
                //    userPost.Username = "guest.mii2@londonsumatra.com";
                //    userPost.Mail = "guest.mii2@londonsumatra.com";
                //    userPost.DisplayName = "Guest MII 2";
                //    userPost.Role = "IT";
                //}
                //else if (request.Username.ToLower() == "dms3.andreas@londonsumatra.com")
                //{
                //    userPost.Username = "dms3.andreas@londonsumatra.com";
                //    userPost.Mail = "dms3.andreas@londonsumatra.com";
                //    userPost.DisplayName = "Andreas Bayyina";
                //    userPost.Role = "Engineer";
                //}
                //else if (request.Username.ToLower().Replace("@londonsumatra.com", "") == "alexp")
                //{
                //    userPost.Username = "alexp@londonsumatra.com";
                //    userPost.Mail = "alexp@londonsumatra.com";
                //    userPost.DisplayName = "Alex Binsar Panjaitan";
                //    userPost.Role = "IT";
                //}
                //else if (request.Username.ToLower().Replace("@londonsumatra.com", "") == "ardiansyahl")
                //{
                //    userPost.Username = "ardiansyahl@londonsumatra.com";
                //    userPost.Mail = "user1@londonsumatra.com";
                //    userPost.DisplayName = "User 1";
                //    userPost.Role = "User";
                //}
                //else if (request.Username.ToLower().Replace("@londonsumatra.com", "") == "agni.bhargo")
                //{
                //    userPost.Username = "Agni.Bhargo@londonsumatra.com";
                //    userPost.Mail = "user1@londonsumatra.com";
                //    userPost.DisplayName = "User 2";
                //    userPost.Role = "User";
                //}
                //else if (request.Username.ToLower().Replace("@londonsumatra.com", "") == "pungky")
                //{
                //    userPost.Username = "Pungky@londonsumatra.com";
                //    userPost.Mail = "user1@londonsumatra.com";
                //    userPost.DisplayName = "User 3";
                //    userPost.Role = "User";
                //}
                //else if (request.Username.ToLower().Replace("@londonsumatra.com", "") == "robbania")
                //{
                //    userPost.Username = "robbania@londonsumatra.com";
                //    userPost.Mail = "user1@londonsumatra.com";
                //    userPost.DisplayName = "User 4";
                //    userPost.Role = "User";

                //}
                //else if (request.Username.ToLower().Replace("@londonsumatra.com", "") == "doni.hidayat")
                //{
                //    userPost.Username = "doni.hidayat@londonsumatra.com";
                //    userPost.Mail = "user1@londonsumatra.com";
                //    userPost.DisplayName = "User 5";
                //    userPost.Role = "User";
                //}
                //else if (request.Username.ToLower().Replace("@londonsumatra.com", "") == "rajohan")
                //{
                //    userPost.Username = "rajohan@londonsumatra.com";
                //    userPost.Mail = "user1@londonsumatra.com";
                //    userPost.DisplayName = "User 6";
                //    userPost.Role = "User";
                //}
                //else if (request.Username.ToLower().Replace("@londonsumatra.com", "") == "ujang")
                //{
                //    userPost.Username = "Ujang@londonsumatra.com";
                //    userPost.Mail = "user1@londonsumatra.com";
                //    userPost.DisplayName = "User 7";
                //    userPost.Role = "User";
                //}
                //else if (request.Username.ToLower().Replace("@londonsumatra.com", "") == "widyap")
                //{
                //    userPost.Username = "WidyaP@londonsumatra.com";
                //    userPost.Mail = "user1@londonsumatra.com";
                //    userPost.DisplayName = "User 8";
                //    userPost.Role = "User";
                //}
                //else if (request.Username.ToLower().Replace("@londonsumatra.com", "") == "icha")
                //{
                //    userPost.Username = "Icha@londonsumatra.com";
                //    userPost.Mail = "user1@londonsumatra.com";
                //    userPost.DisplayName = "User 9";
                //    userPost.Role = "User";
                //}
                //else if (request.Username.ToLower().Replace("@londonsumatra.com", "") == "ukur")
                //{
                //    userPost.Username = "ukur@londonsumatra.com";
                //    userPost.Mail = "user1@londonsumatra.com";
                //    userPost.DisplayName = "User 10";
                //    userPost.Role = "User";
                //}
                //else if (request.Username.ToLower().Replace("@londonsumatra.com", "") == "bahrum")
                //{
                //    userPost.Username = "bahrum@londonsumatra.com";
                //    userPost.Mail = "bahrum23113@londonsumatra.com";
                //    userPost.DisplayName = "Bahrum bukan teroris";
                //    userPost.Role = "User";
                //}
                //else if (request.Username.ToLower().Replace("@londonsumatra.com", "") == "afrianto")
                //{
                //    userPost.Username = "afrianto@londonsumatra.com";
                //    userPost.Mail = "afrianto@londonsumatra.com";
                //    userPost.DisplayName = "Afrianto disebut jg afro";
                //    userPost.Role = "User";
                //}
                //else if (request.Username.ToLower().Replace("@londonsumatra.com", "") == "tjukhasto")
                //{
                //    userPost.Username = "tjukhasto@londonsumatra.com";
                //    userPost.Mail = "Tjukhasto@londonsumatra.com";
                //    userPost.DisplayName = "Tjukhasto Maro Chan";
                //    userPost.Role = "User";
                //}
                //else if (request.Username.ToLower().Replace("@londonsumatra.com", "") == "pramujo")
                //{
                //    userPost.Username = "pramujo@londonsumatra.com";
                //    userPost.Mail = "Pramujo@londonsumatra.com";
                //    userPost.DisplayName = "Pramujo Pramuka";
                //    userPost.Role = "User";
                //}
                //else if (request.Username.ToLower().Replace("@londonsumatra.com", "") == "heruh")
                //{
                //    userPost.Username = "heruh@londonsumatra.com";
                //    userPost.Mail = "HeruH@londonsumatra.com";
                //    userPost.DisplayName = "Heru Hashirama";
                //    userPost.Role = "User";
                //}
                //else if (request.Username.ToLower().Replace("@londonsumatra.com", "") == "andrians")
                //{
                //    userPost.Username = "andrians@londonsumatra.com";
                //    userPost.Mail = "andrians@londonsumatra.com";
                //    userPost.DisplayName = "Andrian Senju";
                //    userPost.Role = "User";
                //}
                //else if (request.Username.ToLower().Replace("@londonsumatra.com", "") == "kusuma")
                //{
                //    userPost.Username = "kusuma@londonsumatra.com";
                //    userPost.Mail = "kusuma@londonsumatra.com";
                //    userPost.DisplayName = "Kusumahhh";
                //    userPost.Role = "User";
                //}
                //else if (request.Username.ToLower().Replace("@londonsumatra.com", "") == "akbar")
                //{
                //    userPost.Username = "akbar@londonsumatra.com";
                //    userPost.Mail = "akbar@londonsumatra.com";
                //    userPost.DisplayName = "akbar tanjung";
                //    userPost.Role = "User";
                //}
                //else if (request.Username.ToLower().Replace("@londonsumatra.com", "") == "ortega")
                //{
                //    userPost.Username = "ortega@londonsumatra.com";
                //    userPost.Mail = "ortega@londonsumatra.com";
                //    userPost.DisplayName = "Ariel Ortega";
                //    userPost.Role = "User";
                //}
                //else if (request.Username.ToLower().Replace("@londonsumatra.com", "") == "dms3.hendri")
                //{
                //    userPost.Username = "dms3.hendri@londonsumatra.com";
                //    userPost.Mail = "dms3.hendri@londonsumatra.com";
                //    userPost.DisplayName = "hendri disebut jg Hendro";
                //    userPost.Role = "Engineer";
                //}
                //else if (request.Username.ToLower().Replace("@londonsumatra.com", "") == "dms3.fahmi")
                //{
                //    userPost.Username = "dms3.fahmi@londonsumatra.com";
                //    userPost.Mail = "dms3.fahmi@londonsumatra.com";
                //    userPost.DisplayName = "Fahmi DMS3";
                //    userPost.Role = "Engineer";
                //}
                //else if (request.Username.ToLower().Replace("@londonsumatra.com", "") == "hengky.sebastianus")
                //{
                //    userPost.Username = "hengky.sebastianus@londonsumatra.com";
                //    userPost.Mail = "Hengky.Sebastianus@londonsumatra.com";
                //    userPost.DisplayName = "Hengky Sebastianus";
                //    userPost.Role = "User";
                //}
                //else
                //{
                //    userPost.Username = "dms3.indah@londonsumatra.com";
                //    userPost.Mail = "dms3.indah@londonsumatra.com";
                //    userPost.DisplayName = "Indah Naibaho";
                //    userPost.Role = "Leader";
                //}

                response.Model = userPost;//_userADService.GetUser(request.Username);
            }
            catch (Exception e)
            {
                response.DidError = true;
                response.ErrorMessage = "There was an internal error, please contact to technical support.".ToUpper();
                _logger?.Error($"There was an error on '{nameof(Get)}' invocation: {e}".ToUpper());
            }

            return response.ToHttpResponse();
        }
    }
}
