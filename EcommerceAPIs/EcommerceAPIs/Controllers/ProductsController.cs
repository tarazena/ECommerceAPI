using EcommerceAPIs.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
            if (Request.Headers.Host.Contains("localhost"))
                response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:8080");
            else
                response.Headers.Add("Access-Control-Allow-Origin", "http://myecommerce.azurewebsites.net");
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            return response;
        }


        public List<Product> LoadJson()
        {
            List<Product> items = new List<Product>();
            try
            {
                var cb = new SqlConnectionStringBuilder();
                cb.DataSource = ".database.windows.net";
                cb.UserID = "";
                cb.Password = "";
                cb.InitialCatalog = "";

                using (var connection = new SqlConnection(cb.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SELECT TOP 100 * FROM dbo.ProductsTable", connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Product tempItem = new Product();
                            tempItem.id = reader["Id"].ToString();
                            tempItem.name = reader["Name"].ToString();
                            tempItem.price = reader["Price"].ToString();
                            tempItem.memory = reader["Memory"].ToString();
                            tempItem.company = reader["Company"].ToString();
                            tempItem.images = reader["Image"].ToString().Split(',').ToList();
                            items.Add(tempItem);
                        }
                    }

                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
                        
            return items;
        }

    }
}
