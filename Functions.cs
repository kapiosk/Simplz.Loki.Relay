using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Simplz.Loki.Relay.Models;

namespace Simplz.Loki.Relay
{
    public static class Functions
    {
        [FunctionName("PostMe")]
        public static async Task<IActionResult> PostMe(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var message = System.Text.Json.JsonSerializer.Deserialize<PostMessage>(requestBody);
            log.LogError("{EmailContent} {Label}", message.EmailContent, message.Label);
            await Task.Delay(100);
            return new OkResult();
        }
    }
}