using System.Web.Http;
using ClientSecretAuthentication.Helpers;
using ClientSecretAuthentication.Models;

namespace ClientSecretAuthentication.Controllers
{
    public class ClientSecretController : ApiController
    {
        [Route("api/addclient")]
        public IHttpActionResult Post(ClientSecretModel clientSecretModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ClientSecret newSecret = AddClientSecret.AddClientSecretData(clientSecretModel.Name);

            return Ok<ClientSecret>(newSecret);

        }
    }
}
