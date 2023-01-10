using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Simplz.Loki.Relay.Models;
using MimeKit;
using System.Threading;
using System;

namespace Simplz.Loki.Relay
{
    public static class Functions
    {
        [FunctionName("PostMe")]
        public static async Task<IActionResult> PostMe(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log,
            CancellationToken token)
        {
            var Label = "Test";
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var message = System.Text.Json.JsonSerializer.Deserialize<SQSMessage>(requestBody);

                var msg = await MimeMessage.LoadAsync(new MemoryStream(Convert.FromBase64String(message.Content)), token);
                log.LogError("{HtmlBody} {Label}", msg.HtmlBody ?? msg.TextBody, Label);
            }
            catch (Exception ex)
            {
                Label = "parser";
                log.LogError(ex, "Error parsing email {Label}", Label);
            }

            await Task.Delay(100, token);
            return new OkResult();
        }
    }
}