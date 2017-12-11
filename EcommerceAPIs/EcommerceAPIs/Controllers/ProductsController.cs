using EcommerceAPIs.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http;

namespace EcommerceAPIs.Controllers
{
    public class ProductsController : ApiController
    {
        [HttpGet]
        [Route("v1/getproductlist")]
        public HttpResponseMessage GetProductList()
        {
            List<Product> products = new List<Product>();

            products = LoadJson();
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, products);
            if (Request.RequestUri.ToString() == "http://myecommerce.azurewebsites.net")
                response.Headers.Add("Access-Control-Allow-Origin", "http://myecommerce.azurewebsites.net");
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            
            return response;
        }


        public List<Product> LoadJson()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "EcommerceAPIs.Static.product-items.json";


            List<Product> items = new List<Product>();
            string dir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader r = new StreamReader(stream))
            {
                string json = r.ReadToEnd();
                items = JsonConvert.DeserializeObject<List<Product>>(json);
            }

            return items;
        }
    }
}
