using Microsoft.Extensions.DependencyInjection;
using System;

namespace PrometheusDemo.Batch
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
               .AddSingleton<QueueWorker>()
               .BuildServiceProvider();

            var worker = serviceProvider.GetService<QueueWorker>();
            worker.Start();
        }
    }
}
