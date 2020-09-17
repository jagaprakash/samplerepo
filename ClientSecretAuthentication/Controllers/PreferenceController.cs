using System;
using System.Net;
using System.Web.Http;
using ClientSecretAuthentication.Helpers;

namespace ClientSecretAuthentication.Controllers
{
    public class PreferenceController : ApiController
    {
        [Authorize]
        [HttpGet]
        public IHttpActionResult GetPreference(int preferenceID)
        {
            int clientID = OwinContextExtensions.GetClientID();
            try
            {
                //var result = Service or DB Call(clientID, propertyID)
                return Json(new
                {
                    PropertyName = string.Format("Preference - {0}", preferenceID),
                    Success = true
                });
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
