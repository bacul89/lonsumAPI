using ApiTemplate.Extensions;
using ApiTemplate.Requests;
using ApiTemplate.Responses;
using ApiTemplate.Services;
using Microsoft.Owin.Infrastructure;
using Microsoft.Owin.Security;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ApiTemplate.Controllers
{
    public class ConnectController : ApiController
    {
        private readonly IUserAdService _userAdService;

        public ConnectController(IUserAdService userAdService)
        {
            _userAdService = userAdService;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("api/v1/connect/authorize")]
        public async Task<HttpResponseMessage> Authorize()
        {
            var response = new Response {Message = await Task.FromResult("Authorize")};
            return response.ToHttpResponse();
        }

        [HttpPost]
        [Route("api/v1/connect/token")]
        public HttpResponseMessage Token([FromBody]LoginRequest login)
        {
            var response = new SingleResponse<LoginAccessRequest>();

            try
            {
                if (!_userAdService.ValidateCredentials(login.Username, login.Password, out var identity,out var role))
                {
                    //return BadRequest]
                    response.DidError = true;
                    response.Message = "Incorrect user or password";
                }
                else
                {
                    var ticket = new AuthenticationTicket(identity, new AuthenticationProperties());
                    var currentUtc = new SystemClock().UtcNow;
                    ticket.Properties.IssuedUtc = currentUtc;
                    //ticket.Properties.ExpiresUtc = currentUtc.Add(TimeSpan.FromMinutes(30));

                    response.Model = new LoginAccessRequest
                    {
                        UserName = login.Username,
                        Role = role,
                        AccessToken = Startup.OAuthOptions.AccessTokenFormat.Protect(ticket)
                    };
                }
            }
            catch (Exception ex)
            {
                response.DidError = true;
                response.Message = "Something error happened";
                response.ErrorMessage = ex.Message;
            }
            

            return response.ToHttpResponse();
        }

        [Authorize(Roles = "leader")]
        [HttpGet]
        [Route("api/v1/connect/ping")]
        public HttpResponseMessage Ping()
        {
            var response = new Response {Message = $"Entering Ping(): User={User.Identity.Name}"};
            return response.ToHttpResponse();
        }

        [HttpPost]
        [Route("api/v1/connect/endsession")]
        public async Task<HttpResponseMessage> EndSession()
        {
            var response = new Response { Message = await Task.FromResult("EndSession") };
            return response.ToHttpResponse();
        }
    }
}
