using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Prometheus;
using System;
using System.Threading;

namespace PrometheusDemo.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly static Random _Random = new Random();
        private readonly ILogger<IndexModel> _logger;

        private static readonly Summary _DelaySummary = Metrics.CreateSummary("web_delay_seconds", "Website artificial delay added");

        public string Message { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            Message = string.Empty;
            if (Request.Query.ContainsKey("slow"))
            {
                var delaySeconds = _Random.Next(2, 10);
                _DelaySummary.Observe(delaySeconds);
                Thread.Sleep(delaySeconds*1000);
                Message = $"(after {delaySeconds}s)";
            }
        }
    }
}
