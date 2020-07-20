using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace IHostedServiceDemo.Services
{
    public class WriteToFileHostedService : IHostedService, IDisposable
    {
        private readonly IHostEnvironment environment;
        private readonly string fileName = "File 1.txt";
        private Timer timer;
        public WriteToFileHostedService(IHostEnvironment environment) 
        {
            this.environment = environment;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            WriteToFile("WriteToFileHostedService : Process Started " + DateTime.Now.ToString("dd/MM/yyyy H:mm:ss"));
            /* El método DoWork se ejecuta cada 5 segundos */
            timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
            return Task.CompletedTask;
        }

        private void WriteToFile(string message)
        {
            var path = $@"{environment.ContentRootPath}\wwwroot\{fileName}";
            using StreamWriter writer = new StreamWriter(path, append: true);
            writer.WriteLine(message);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            WriteToFile("WriteToFileHostedService : Process Stopped " + DateTime.Now.ToString("dd/MM/yyyy H:mm:ss"));
            /* Desactiva el timer sino es nulo cuando pare la tarea */
            timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        private void DoWork(object state) 
        {
            WriteToFile("WriteToFileHostedService : Doing some work at " + DateTime.Now.ToString("dd/MM/yyyy H:mm:ss"));
        }
        public void Dispose()
        {
            /* El signo ? asegura que solo se ejecuta Dispose si el timer no es nulo */
            timer?.Dispose();
        }
    }
}
