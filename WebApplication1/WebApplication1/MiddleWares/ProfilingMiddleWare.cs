using System.Diagnostics;
using System.Net.WebSockets;

namespace WebApplication1.NewFolder
{
    public class ProfilingMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ProfilingMiddleWare> _logger;

        public ProfilingMiddleWare(RequestDelegate next, ILogger<ProfilingMiddleWare> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew(); // Start timing
            await _next(context); // Call the next middleware
            stopwatch.Stop(); // Stop timing
            _logger.LogInformation($"Request `{context.Request.Path}` took `{stopwatch.ElapsedMilliseconds}` ms");
        }
    }
}
