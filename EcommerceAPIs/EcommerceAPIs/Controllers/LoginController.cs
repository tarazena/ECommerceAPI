using EcommerceAPIs.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Azure;
using Microsoft.WindowsAzure; // Namespace for CloudConfigurationManager
using Microsoft.WindowsAzure.Storage; // Namespace for CloudStorageAccount
using Microsoft.WindowsAzure.Storage.Blob; // Namespace for Blob storage types
using System.IO;
using System.Security.Cryptography;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace EcommerceAPIs.Controllers
{
    public class LoginController : ApiController
    {
        [HttpPost]
        [Route("v1/login")]
        public HttpResponseMessage Login([FromBody] Login credentials)
        {
            HttpResponseMessage response;
            if (credentials.UserID != System.Environment.GetEnvironmentVariable("LOGINUSERID") || credentials.Password != System.Environment.GetEnvironmentVariable("LOGINPASSWORD"))
            {
                response = Request.CreateResponse(HttpStatusCode.Unauthorized, "Invalid User ID or Password");
            }
            else
            {
                string guid = Guid.NewGuid().ToString();

                JObject token = JObject.Parse(@"{ 'token': '" + guid + @"' }");
                storeTokenToCloud(token);
                response = Request.CreateResponse(HttpStatusCode.Accepted, token);
            }
            return response;
        }

        private void storeTokenToCloud(JObject token)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(System.Environment.GetEnvironmentVariable("COLUDSTORAGECONNECTIONSTRING"));
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("ecommercestorage");
            CloudBlockBlob blockBlob = container.GetBlockBlobReference("token.txt");

            using (StreamWriter writer = new StreamWriter(blockBlob.OpenWrite()))
            {
                writer.Write(token);
            }
        }

        private string GenerateToken(string username, int expireMinutes = 30)
        {
            var hmac = new HMACSHA256();
            var Secret = Convert.ToBase64String(hmac.Key);

            var symmetricKey = Convert.FromBase64String(Secret);
            var tokenHandler = new JwtSecurityTokenHandler();

            var now = DateTime.UtcNow;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                        {
                        new Claim(ClaimTypes.Name, username)
                    }),

                Expires = now.AddMinutes(Convert.ToInt32(expireMinutes)),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var stoken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(stoken);

            return token;
        }
    }
}
