using EcommerceAPIs.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
            //List<Product> products = new List<Product>();

            //products = LoadJson();

            string products = @"[{""images"":[""a_iphone_2g_1"",""a_iphone_2g_2"",""a_iphone_2g_3""],""id"":""1"",""name"":""iPhone"",""price"":""100"",""memory"":""16"",""company"":""Apple""},{""images"":[""a_iphone_3g_1"",""a_iphone_3g_2"",""a_iphone_3g_3""],""id"":""2"",""name"":""iPhone 3G"",""price"":""110"",""memory"":""16"",""company"":""Apple""},{""images"":[""a_iphone_4_1"",""a_iphone_4_2"",""a_iphone_4_3""],""id"":""3"",""name"":""iPhone 4"",""price"":""140"",""memory"":""16"",""company"":""Apple""},{""images"":[""a_iphone_4_1"",""a_iphone_4_2"",""a_iphone_4_3""],""id"":""4"",""name"":""iPhone 4s"",""price"":""180"",""memory"":""32"",""company"":""Apple""},{""images"":[""a_iphone_5_1"",""a_iphone_5_2""],""id"":""5"",""name"":""iPhone 5"",""price"":""200"",""memory"":""64"",""company"":""Apple""},{""images"":[""a_iphone_6plus_1"",""a_iphone_6plus_2""],""id"":""6"",""name"":""iPhone 6 Plus"",""price"":""220"",""memory"":""128"",""company"":""Apple""},{""images"":[""a_iphone_6_1"",""a_iphone_6_2""],""id"":""7"",""name"":""iPhone 6"",""price"":""210"",""memory"":""128"",""company"":""Apple""},{""images"":[""a_iphone_2g_1"",""a_iphone_2g_2"",""a_iphone_2g_3""],""id"":""8"",""name"":""iPhone"",""price"":""100"",""memory"":""16"",""company"":""Apple""},{""images"":[""a_iphone_3g_1"",""a_iphone_3g_2"",""a_iphone_3g_3""],""id"":""9"",""name"":""iPhone 3G"",""price"":""110"",""memory"":""16"",""company"":""Apple""},{""images"":[""a_iphone_4_1"",""a_iphone_4_2"",""a_iphone_4_3""],""id"":""10"",""name"":""iPhone 4"",""price"":""140"",""memory"":""16"",""company"":""Apple""},{""images"":[""a_iphone_4_1"",""a_iphone_4_2"",""a_iphone_4_3""],""id"":""11"",""name"":""iPhone 4s"",""price"":""180"",""memory"":""32"",""company"":""Apple""},{""images"":[""a_iphone_5_1"",""a_iphone_5_2""],""id"":""12"",""name"":""iPhone 5"",""price"":""200"",""memory"":""64"",""company"":""Apple""},{""images"":[""a_iphone_6plus_1"",""a_iphone_6plus_2""],""id"":""13"",""name"":""iPhone 6 Plus"",""price"":""220"",""memory"":""128"",""company"":""Apple""},{""images"":[""a_iphone_6_1"",""a_iphone_6_2""],""id"":""14"",""name"":""iPhone 6"",""price"":""210"",""memory"":""128"",""company"":""Apple""},{""images"":[""a_iphone_2g_1"",""a_iphone_2g_2"",""a_iphone_2g_3""],""id"":""15"",""name"":""iPhone"",""price"":""100"",""memory"":""16"",""company"":""Apple""},{""images"":[""a_iphone_3g_1"",""a_iphone_3g_2"",""a_iphone_3g_3""],""id"":""16"",""name"":""iPhone 3G"",""price"":""110"",""memory"":""16"",""company"":""Apple""},{""images"":[""a_iphone_4_1"",""a_iphone_4_2"",""a_iphone_4_3""],""id"":""17"",""name"":""iPhone 4"",""price"":""140"",""memory"":""16"",""company"":""Apple""},{""images"":[""a_iphone_4_1"",""a_iphone_4_2"",""a_iphone_4_3""],""id"":""18"",""name"":""iPhone 4s"",""price"":""180"",""memory"":""32"",""company"":""Apple""},{""images"":[""a_iphone_5_1"",""a_iphone_5_2""],""id"":""19"",""name"":""iPhone 5"",""price"":""200"",""memory"":""64"",""company"":""Apple""},{""images"":[""a_iphone_6plus_1"",""a_iphone_6plus_2""],""id"":""20"",""name"":""iPhone 6 Plus"",""price"":""220"",""memory"":""128"",""company"":""Apple""},{""images"":[""a_iphone_6_1"",""a_iphone_6_2""],""id"":""21"",""name"":""iPhone 6"",""price"":""210"",""memory"":""128"",""company"":""Apple""}]";
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, JArray.Parse(products));
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            return response;
        }


        public List<Product> LoadJson()
        {
            List<Product> items = new List<Product>();
            try
            {
                var cb = new SqlConnectionStringBuilder();
                cb.DataSource = System.Environment.GetEnvironmentVariable("SQLPRODUCTSDATASOURCE");
                cb.UserID = System.Environment.GetEnvironmentVariable("SQLUSERID");
                cb.Password = System.Environment.GetEnvironmentVariable("SQLPASSWORD");
                cb.InitialCatalog = System.Environment.GetEnvironmentVariable("SQLPRODUCTSINITIALCATALOG");

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
