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
    }
}
