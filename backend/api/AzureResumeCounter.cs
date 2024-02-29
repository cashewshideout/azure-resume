using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs.Extensions.CosmosDB;
using Newtonsoft.Json;
using System.Net.Http;

namespace AzureResume.Function
{
    public static class AzureResumeCounter
    {
        [FunctionName("AzureResumeCounter")]
        public static HttpResponseMessage Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            [CosmosDB(
                databaseName:"azureresume", 
                containerName: "counter", 
                Connection = "AzureResumeConnectionString",
                Id = "1",
                PartitionKey = "1")] VisitorCounter counter,
            [CosmosDB(
                databaseName: "azureresume",
                containerName: "counter",
                Connection = "AzureResumeConnectionString",
                Id = "1",
                PartitionKey = "1")]out VisitorCounter updatedcounter,
            ILogger log)
        {
            log.LogInformation("Before Count: " + counter.count.ToString());

            updatedcounter = counter;
            updatedcounter.count++;

            var jsonToReturn = JsonConvert.SerializeObject(updatedcounter);

            log.LogInformation("After Count: " + updatedcounter.count.ToString());
            return new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new StringContent(jsonToReturn, System.Text.Encoding.UTF8, "application/json")
            };
        }
    }
}
