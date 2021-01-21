using System;
using ApiTemplate.Cores.Models;
using ApiTemplate.Services;
using Serilog;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ApiTemplate.Extensions;
using ApiTemplate.Responses;
using ITAssetsManagement.Models;
using ITAssetsManagement.Services;

namespace ApiTemplate.Controllers
{
    [RoutePrefix("api/v1/activedirectory")]
    public class ActiveDirectoryController : ApiController
    {
        private readonly ILogger _logger;
        private readonly IUserAdService _userAdService;

        private readonly List<UserAd> _user = new List<UserAd>
        {
            new UserAd
            {
                Id = "1",
                Username = "User1",
                DisplayName = "UserOne",
                FirstName = "User",
                LastName = "One",
                Mail = "UserOne@user1.com",
                Password = "UserOneUser1"
            },
            new UserAd
            {
                Id = "2",
                Username = "User2",
                DisplayName = "UserTwo",
                FirstName = "User",
                LastName = "Two",
                Mail = "UserTwo@user2.com",
                Password = "UserTwoUser2"
            },
        };

        public ActiveDirectoryController(ILogger log, IUserAdService userAdService)
        {
            _logger = log;
            _userAdService = userAdService;
        }

        [HttpGet, Route("getassetcategory")]
        public async Task<HttpResponseMessage> GetAssetsCategory()
        {
            var response = new ListResponse<Asset_Category>();
            try
            {
                response.Model = await AssetService.GetAsset_Category();
            }
            catch (Exception e)
            {
                response.DidError = true;
                response.ErrorMessage = "There was an internal error, please contact to technical support.".ToUpper();
                _logger?.Error($"There was an error on '{nameof(GetAssetsCategory)}' invocation: {e}".ToUpper());
            }

            return response.ToHttpResponse();
        }


        [HttpGet, Route("getassetheader")]
        public async Task<HttpResponseMessage> GetAssetsHeader()
        {
            var response = new ListResponse<RMD_Asset_Header>();
            try
            {
                response.Model = await AssetService.GetRmd_Asset_Header();
            }
            catch (Exception e)
            {
                response.DidError = true;
                response.ErrorMessage = "There was an internal error, please contact to technical support.".ToUpper();
                _logger?.Error($"There was an error on '{nameof(GetAssetsCategory)}' invocation: {e}".ToUpper());
            }

            return response.ToHttpResponse();
        }

        //// GET api/<controller>
        //[HttpGet]
        //[Route("api/v1/activedirectory/getalluser")]
        //public async Task<HttpResponseMessage> Get()
        //{
        //    var response = new ListResponse<AD_Employee>();

        //    try
        //    {
        //        response.Model = await ;
        //        _logger?.Information($"The items have been retrieved successfully.".ToUpper());
        //    }
        //    catch (Exception e)
        //    {
        //        response.DidError = true;
        //        response.ErrorMessage = "There was an internal error, please contact to technical support.".ToUpper();
        //        _logger?.Error($"There was an error on '{nameof(Get)}' invocation: {e}".ToUpper());
        //    }

        //    return response.ToHttpResponse();
        //}

        //// GET api/<controller>
        //[HttpGet]
        //[Route("api/v1/activedirectory/getuser")]
        //public async Task<HttpResponseMessage> GetUser(string name = "",string uid="")
        //{
        //    var response = new ListResponse<AD_Employee>();

        //    try
        //    {
        //        //name = !string.IsNullOrEmpty(name) ? name : "";
        //        if (!string.IsNullOrEmpty(name))
        //        {
        //            response.Model = await UserManagementService.GetAdEmployeesByPredicate(x => x.EmpName.ToLower().Contains(name.ToLower()));
        //        }
        //        if (!string.IsNullOrEmpty(uid))
        //        {
        //            response.Model = await UserManagementService.GetAdEmployeesByPredicate(x => x.LoginUser.ToLower().Contains(uid.ToLower()));
        //        }

        //        _logger?.Information($"The items have been retrieved successfully.".ToUpper());
        //    }
        //    catch (Exception e)
        //    {
        //        response.DidError = true;
        //        response.ErrorMessage = "There was an internal error, please contact to technical support.".ToUpper();
        //        _logger?.Error($"There was an error on '{nameof(Get)}' invocation: {e}".ToUpper());
        //    }

        //    return response.ToHttpResponse();
        //}


        //[HttpGet]
        //[Route("api/v1/activedirectory/getusermail")]
        //public async Task<HttpResponseMessage> GetUserMail(string mail = "")
        //{
        //    var response = new ListResponse<string>();

        //    try
        //    {
        //        mail = !string.IsNullOrEmpty(mail) ? mail : "";
        //        response.Model = await _userAdService.GetAllUserMail(mail);
        //        _logger?.Information($"The items have been retrieved successfully.".ToUpper());
        //    }
        //    catch (Exception e)
        //    {
        //        response.DidError = true;
        //        response.ErrorMessage = "There was an internal error, please contact to technical support.".ToUpper();
        //        _logger?.Error($"There was an error on '{nameof(Get)}' invocation: {e}".ToUpper());
        //    }

        //    return response.ToHttpResponse();
        //}

        //// GET api/<controller>/5
        //public async Task<HttpResponseMessage> Get(string id)
        //{
        //    var response = new SingleResponse<UserAd>();

        //    try
        //    {
        //        response.Model = await Task.FromResult(_user.FirstOrDefault(x => x.Id.Trim().ToLower().Contains(id)));
        //        response.Message = response.Model == null ? "Data was not found":"";
        //    }
        //    catch (Exception ex)
        //    {
        //        response.DidError = true;
        //        response.ErrorMessage = "There was an internal error, please contact to technical support.";

        //        _logger?.Error("There was an error on '{0}' invocation: {1}", nameof(Get), ex);
        //    }

        //    return response.ToHttpResponse();
        //}

        //// POST api/<controller>
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<controller>/5
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<controller>/5
        //public void Delete(int id)
        //{
        //}
    }
}