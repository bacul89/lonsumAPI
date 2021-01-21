using Lonsum.MyLonsum.Helper;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Jwt;
using Owin;
using System.Web.Http;

[assembly: OwinStartup(typeof(Lonsum.MyLonsum.Startup))]

namespace Lonsum.MyLonsum
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            app.UseWebApi(config);
            ConfigureOAuth(app);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        public void ConfigureOAuth(IAppBuilder app)
        {
            app.UseJwtBearerAuthentication(
                new JwtBearerAuthenticationOptions
                {
                    AuthenticationMode = AuthenticationMode.Active,
                    TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidAudience = ConfigurationHelper.CredentialAudience,
                        ValidIssuer = ConfigurationHelper.CredentialIssuer,
                        IssuerSigningKey = GetSymmetricSecurityKey(),
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true
                    }
                });
        }

        private SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            byte[] data = ConfigurationHelper.GetSymmetricSecurityKeyAsBytes();
            var result = new SymmetricSecurityKey(data);
            return result;
        }
    }
}