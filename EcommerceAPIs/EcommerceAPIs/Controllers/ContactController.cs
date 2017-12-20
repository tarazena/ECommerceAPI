using EcommerceAPIs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Http;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace EcommerceAPIs.Controllers
{
    public class ContactController : ApiController
    {
        [HttpPost]
        [Route("v1/contact")]
        public async Task<HttpResponseMessage> Contact([FromBody] Contact contact)
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
            try
            {
                var apiKey = System.Environment.GetEnvironmentVariable("SENDGRID_APIKEY");
                var client = new SendGridClient(apiKey);
                var msg = new SendGridMessage()
                {
                    From = new EmailAddress("tarazena@gmail.com", "devDeploy"),
                    Subject = "Hello World from the SendGrid CSharp SDK!",
                    PlainTextContent = contact.Message,
                    HtmlContent = "<strong>Hello, Email!</strong>"
                };
                msg.AddTo(new EmailAddress(contact.Email, contact.FirstName +" " + contact.LastName));
                var EMAILresponse = await client.SendEmailAsync(msg);
                response = Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            if (Request.Headers.Host.Contains("localhost"))
                response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:8080");
            else
                response.Headers.Add("Access-Control-Allow-Origin", "http://myecommerce.azurewebsites.net");
            return response;
        }
    }
}
