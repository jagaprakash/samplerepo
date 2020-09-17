using System;
using System.Collections.Concurrent;
using System.Security.Cryptography;
using ClientSecretAuthentication.Models;
using Microsoft.Owin.Security.DataHandler.Encoder;

namespace ClientSecretAuthentication.Helpers
{
    public static class AddClientSecret
    {
        public static ConcurrentDictionary<string, ClientSecret> ClientSecretList = new ConcurrentDictionary<string, ClientSecret>();

        static AddClientSecret()
        {
            ClientSecretList.TryAdd("099153c2625149bc8ecb3e85e03f0022",
                                new ClientSecret
                                {
                                    ClientId = "099153c2625149bc8ecb3e85e03f0022",
                                    Base64Secret = "IxrAjDoa2FqElO7IhrSrUJELhUckePEPVpaePlS_Xaw",
                                    Name = "ResourceServer.Api 1"
                                });
        }

        public static ClientSecret AddClientSecretData(string name)
        {
            var clientId = Guid.NewGuid().ToString("N");

            var key = new byte[32];
            RNGCryptoServiceProvider.Create().GetBytes(key);
            var base64Secret = TextEncodings.Base64Url.Encode(key);

            ClientSecret newClient = new ClientSecret { ClientId = clientId, Base64Secret = base64Secret, Name = name };
            ClientSecretList.TryAdd(clientId, newClient);
            return newClient;
        }

        public static ClientSecret FindClientSecret(string clientId)
        {
            ClientSecret clientSecret = null;
            if (ClientSecretList.TryGetValue(clientId, out clientSecret))
            {
                return clientSecret;
            }
            return null;
        }
    }
}