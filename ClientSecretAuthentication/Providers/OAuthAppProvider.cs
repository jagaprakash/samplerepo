using System;
using System.Security.Claims;
using System.Threading.Tasks;
using ClientSecretAuthentication.Helpers;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;

namespace ClientSecretAuthentication.Providers
{
    public partial class OAuthAppProvider : OAuthAuthorizationServerProvider
    {
        private string clientSecret;
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientId;
            context.TryGetFormCredentials(out clientId, out clientSecret);
            if (string.IsNullOrEmpty(clientId))
                context.TryGetBasicCredentials(out clientId, out clientSecret);
            if (!string.IsNullOrEmpty(clientId))
            {
                context.Validated(clientId);
            }
            else
            {
                context.Validated();
            }
            return base.ValidateClientAuthentication(context);
        }
        public override Task GrantClientCredentials(OAuthGrantClientCredentialsContext context)
        {
            return Task.Factory.StartNew(() =>
            {
                try
                {
                    bool isValid = false;
                    //isValid = true; //This should be the Service/DB call to validate the client id, client secret.
                    //ValidateApp(context.ClientId, clientSecret);
                    if (AddClientSecret.ClientSecretList != null && string.IsNullOrEmpty(context.ClientId) && AddClientSecret.ClientSecretList.ContainsKey(context.ClientId))
                    {
                        var clientDetails = AddClientSecret.ClientSecretList[context.ClientId];

                        isValid = context.ClientId.Equals(clientDetails.ClientId) && clientSecret.Equals(clientDetails.Base64Secret);
                    }
                    if (isValid)
                    {
                        var oAuthIdentity = new ClaimsIdentity(context.Options.AuthenticationType);
                        oAuthIdentity.AddClaim(new Claim("ClientID", context.ClientId));
                        var ticket = new AuthenticationTicket(oAuthIdentity, new AuthenticationProperties());
                        context.Validated(ticket);
                    }
                    else
                    {
                        context.SetError("invalid ClientID", context.ClientId);
                        //logger.Error(string.Format("GrantResourceOwnerCredentials(){0}Credentials not valid for ClientID : {1}.", Environment.NewLine, context.ClientId));
                    }
                }
                catch (Exception)
                {
                    context.SetError("Error", "internal server error");
                    //logger.Error(string.Format("GrantResourceOwnerCredentials(){0}Returned tuple is null for ClientID : {1}.", Environment.NewLine, context.ClientId));
                }
            });
        }
    }
}