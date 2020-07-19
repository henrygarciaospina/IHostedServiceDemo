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
    public class WriteToFileHostedServiceSecond : IHostedService, IDisposable
    {
        private readonly IHostEnvironment environment;
        private readonly string fileName = "File 2.txt";
        private Timer timer;
        public WriteToFileHostedServiceSecond(IHostEnvironment environment) 
        {
            this.environment = environment;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            WriteToFile("WriteToFileHostedServiceSecond : Process Started " + DateTime.Now.ToString("dd/MM/yyyy H:mm:ss"));
            /* El método DoWork se ejecuta cada 7 segundos */
            timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(7));
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
            WriteToFile("WriteToFileHostedServiceSecond : Process Stopped " + DateTime.Now.ToString("dd/MM/yyyy H:mm:ss"));
            /* Desactiva el timer sino es nulo cuando pare la tarea */
            timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        private void DoWork(object state) 
        {
            WriteToFile("WriteToFileHostedServiceSecond : Doing some work at " + DateTime.Now.ToString("dd/MM/yyyy H:mm:ss"));
        }
        public void Dispose()
        {
            /* El signo ? asegura que solo se ejecuta Dispose si el timer no es nulo */
            timer?.Dispose();
        }
    }
}
