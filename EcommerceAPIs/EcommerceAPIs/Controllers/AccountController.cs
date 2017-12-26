using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EcommerceAPIs.Controllers
{
    public class AccountController : ApiController
    {
        [HttpGet]
        [Route("v1/account")]
        public HttpResponseMessage GetProductList()
        {
            string account = @"{""firstName"":""John"",""lastName"":""Appleseed"",""email"":""test @test.com"",""state"":""Ohio"",""country"":""USA"",""image"":""person - 2"",""orders"":[{""id"":""1234"",""date"":""6 / 22 / 2017"",""total"":320.69,""shipping"":20,""tax"":15,""status"":""Delivered"",""items"":[{""name"":""iPhone"",""company"":""Apple"",""description"":""Memory: 16GB, Color: Black"",""price"":110,""quantity"":2,""image"":""a_iphone_2g_1""},{""name"":""iPhone 3G"",""company"":""Apple"",""description"":""Memory: 16GB, Color: Black"",""price"":110,""quantity"":5,""image"":""a_iphone_3g_1""}],""invoiceAddress"":{""name"":""John Appleseed"",""address1"":""1234 Ohio Blvd"",""address2"":""Apt 5"",""city"":""Cleveland"",""zip"":""44144"",""state"":""Ohio"",""country"":""USA""},""shippingAddress"":{""name"":""John Appleseed"",""address1"":""1234 Ohio Blvd"",""address2"":"""",""city"":""Cleveland"",""zip"":""44144"",""state"":""Ohio"",""country"":""USA""}},{""id"":""5678"",""date"":""9 / 25 / 2017"",""total"":515,""shipping"":30,""tax"":25,""status"":""Cancelled"",""items"":[{""name"":""iPhone 4"",""company"":""Apple"",""description"":""Memory: 16GB, Color: White"",""price"":230,""quantity"":2,""image"":""a_iphone_4_1""}],""invoiceAddress"":{""name"":""John Appleseed"",""address1"":""1234 Ohio Blvd"",""address2"":""Apt 5"",""city"":""Cleveland"",""zip"":""44144"",""state"":""Ohio"",""country"":""USA""},""shippingAddress"":{""name"":""Doe Mary"",""address1"":""555 Chagrin Blvd"",""address2"":""Suite 5"",""city"":""Cleveland"",""zip"":""44120"",""state"":""Ohio"",""country"":""USA""}},{""id"":""999"",""date"":""12 / 12 / 2017"",""total"":1055,""shipping"":65,""tax"":40,""status"":""Action Needed"",""items"":[{""name"":""iPhone X"",""company"":""Apple"",""description"":""Memory: 64GB, Color: Black"",""price"":950,""quantity"":1,""image"":""a_iphone_2g_1""}],""invoiceAddress"":{""name"":""John Appleseed"",""address1"":""1234 Ohio Blvd"",""address2"":""Apt 5"",""city"":""Cleveland"",""zip"":""44144"",""state"":""Ohio"",""country"":""USA""},""shippingAddress"":{""name"":""John Appleseed"",""address1"":""1234 Ohio Blvd"",""address2"":""Apt 5"",""city"":""Cleveland"",""zip"":""44144"",""state"":""Ohio"",""country"":""USA""}}]}";
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, JObject.Parse(account));
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            return response;
        }
    }
}
