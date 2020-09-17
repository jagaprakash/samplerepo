using System;
using System.Configuration;
using System.Web.Configuration;
using ClientSecretAuthentication.Providers;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;

namespace ClientSecretAuthentication
{
    public partial class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }
        static Startup()
        {
            string expiry = WebConfigurationManager.AppSettings["tokenexpiry"];
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/token"),
                Provider = new OAuthAppProvider(),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(Convert.ToDouble(ConfigurationManager.AppSettings["tokenexpiry"])),
                AllowInsecureHttp = Convert.ToBoolean(ConfigurationManager.AppSettings["allowinsecurehttp"])
            };

        }

        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseOAuthBearerTokens(OAuthOptions);
        }
    }
}