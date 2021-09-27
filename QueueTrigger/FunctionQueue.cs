using System;
using System.Net;
using System.Net.Http;
using ImageEditor;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace QueueTrigger
{
    public static class FunctionQueue
    {
        [FunctionName("FunctionQueue")]
        public static void Run([QueueTrigger("myqueue-items", Connection = "")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");

           foreach (element in queue)
            {
                var client = new WebClient();
                var imgStream = client.DownloadString("Https://source.unsplash.com/random/300x200");
                var renderedImage = ImageHelper.AddTextToImage(imgStream, (myQueueItem, (10, 10), 32, "ffffff"),
                ("A developer", (10, 44), 24, "000000")
                );
            }
        
            
        }

    }
}
