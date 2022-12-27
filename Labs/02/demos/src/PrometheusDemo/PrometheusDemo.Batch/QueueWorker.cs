using Prometheus;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Timers;
using Timer = System.Threading.Timer;

namespace PrometheusDemo.Batch
{
    public class QueueWorker
    {
        private static readonly ManualResetEvent _ResetEvent = new ManualResetEvent(false);
        private static readonly Random _Random = new Random();

        private static readonly Counter _JobCounter = Metrics.CreateCounter("worker_jobs_total", "Worker jobs handled", "status");
        private static readonly Gauge _JobGauge = Metrics.CreateGauge("worker_jobs_active", "Worker jobs in process");

        public void Start()
        {
            StartMetricServer();
            using (var timer = new Timer(new TimerCallback(UpdateMetrics), null, 0, 5000))
            {
                _ResetEvent.WaitOne();
            }
        }

        private static void UpdateMetrics(object timerState)
        {
            var jobs = _Random.Next(50, 500);
            var failed = _Random.Next(1, 50);
            var processed = jobs-failed;            

            if (ok)
            {
                requestCounter.Labels("200", "/").Inc();
            }
            else
            {
                requestCounter.Labels("500", "/").Inc();
            }

            var active = _Random.Next(1, 100);
            _JobGauge.Set(active);

            Console.WriteLine($"Metrics updated to random values");
        }

        private void StartMetricServer()
        {
            var metricsPort = 80;
            var server = new MetricServer(metricsPort);
            server.Start();
            Console.WriteLine($"Metrics server listening on port {metricsPort}");
        }
    }
}
