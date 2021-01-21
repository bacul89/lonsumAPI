using Microsoft.Owin.Security.OAuth;
using Owin;
using System;

namespace ApiTemplate
{
    public partial class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }
        static Startup()
        {
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1)
                //TokenEndpointPath = new PathString("/Token"),
                //AccessTokenExpireTimeSpan = TimeSpan.MaxValue  //preserved
            };
        }

        public static void ConfigureAuth(IAppBuilder app)
        {
            app.UseOAuthBearerTokens(OAuthOptions);
        }

    }
}