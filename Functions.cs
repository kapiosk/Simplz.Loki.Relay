using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Simplz.Loki.Relay.Models;
using MimeKit;
using System;
using System.Linq;

namespace Simplz.Loki.Relay
{
    public static class Functions
    {
        [FunctionName("PostMe")]
        public static async Task<IActionResult> PostMe(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            string requestBody = "";
            try
            {
                requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var message = System.Text.Json.JsonSerializer.Deserialize<SQSMessage>(requestBody);
                var msg = await MimeMessage.LoadAsync(new MemoryStream(Convert.FromBase64String(message.Content)));
                string address = msg.To.Mailboxes.FirstOrDefault(x => x.Address.EndsWith("loggin.belugga.io"))?.Address;
                if (string.IsNullOrEmpty(address))
                    address = msg.To.Mailboxes.FirstOrDefault()?.Address;
                if (string.IsNullOrEmpty(address))
                    address = "Anon";
                address = address.Split('@').Last();
                log.LogWarning("{HtmlBody} {Subject} {ToAddress}", msg.HtmlBody ?? msg.TextBody, msg.Subject, address);
            }
            catch (Exception ex)
            {
                log.LogError(ex, "Error parsing email {RequestBody}", requestBody);
            }

            await Task.Delay(100);
            return new OkResult();
        }
    }
}