using System.Net;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AzureResume.Function
{
    public class MultiResponse
    {
        [CosmosDBOutput("azureresume", "counter", Connection = "AzureResumeConnectionString", CreateIfNotExists = true)]
        public VisitorCounter UpdatedCounter { get; set; }
        public HttpResponseMessage Response { get; set; }

    }

    public class GetResumeCounter
    {
        private readonly ILogger<GetResumeCounter> _logger;

        public GetResumeCounter(ILogger<GetResumeCounter> logger)
        {
            _logger = logger;
        }

        [Function("GetResumeCounter")]
        public MultiResponse Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req,
            [CosmosDBInput(
                databaseName:"azureresume", 
                containerName: "counter", 
                Connection = "AzureResumeConnectionString",
                Id = "1",
                PartitionKey = "1")] VisitorCounter counter
        )
        {
             _logger.LogInformation("counter.Count: {count}", counter.count);
             _logger.LogInformation("counter.Id: {id}", counter.id);
            counter.count += 1;

            var jsonToReturn = JsonConvert.SerializeObject(counter);
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK) {
                Content = new StringContent(jsonToReturn, Encoding.UTF8, "application/json")
            };
            _logger.LogInformation("response: {message}", response.Content);

            MultiResponse multiResponse = new MultiResponse()
            {
                UpdatedCounter = counter,
                Response = response
            };

             _logger.LogInformation("UpdatedCounter.Count: {count}", multiResponse.UpdatedCounter.count);
             _logger.LogInformation("UpdatedCounter.Id: {id}", multiResponse.UpdatedCounter.id);
            return multiResponse;
        }
    }
}

