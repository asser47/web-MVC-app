using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;
using WebApplication1.NewFolder;

namespace WebApplication1.Filters
{
    public class LogicActivityFilter : IActionFilter, IAsyncActionFilter
    {
        private readonly ILogger<LogicActivityFilter> _logger;
        public LogicActivityFilter(ILogger<LogicActivityFilter> logger)
        {
            _logger = logger;
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation($"Exuecuting Action {context.ActionDescriptor.DisplayName} on controller {context.Controller} with {JsonSerializer.Serialize(context.ActionArguments)}");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation($"Action {context.ActionDescriptor.DisplayName} finished on controller {context.Controller}");

        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve,
                WriteIndented = true
            };

            _logger.LogInformation($"(ASYNC) Executing Action {context.ActionDescriptor.DisplayName} on controller {context.Controller} with {JsonSerializer.Serialize(context.ActionArguments, options)}");
            await next();
            _logger.LogInformation($"(ASYNC) Action {context.ActionDescriptor.DisplayName} finished on controller {context.Controller}");
        }
    }
}
