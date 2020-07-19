using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IHostedServiceDemo.Services
{
    public class WriteToFileHostedService : IHostedService
    {
        private readonly IHostEnvironment environment;
        private readonly string fileName = "File 1.txt";

        public WriteToFileHostedService(IHostEnvironment environment) 
        {
            this.environment = environment;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            WriteToFile("WriteToFileHostedService : Process Started " + DateTime.Now.ToString("H:mm:ss"));
            return Task.CompletedTask;
        }

        private void WriteToFile(string message)
        {
            var path = $@"{environment.ContentRootPath}\wwwroot\{fileName}";
            using (StreamWriter writer = new StreamWriter(path, append: true))
            {
                writer.WriteLine(message);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            WriteToFile("WriteToFileHostedService : Process Stopped " + DateTime.Now.ToString("H:mm:ss"));
            return Task.CompletedTask;
        }
    }
}
