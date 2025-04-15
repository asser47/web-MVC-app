using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Reflection.Metadata.Ecma335;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class ConfigController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly IOptionsMonitor<AttachmentOptions> attachmentOptions;

        public ConfigController(IConfiguration configuration, IOptionsMonitor<AttachmentOptions> attachmentOptions)
        {
            this.configuration = configuration;
            this.attachmentOptions = attachmentOptions;
            var value = attachmentOptions.CurrentValue;
        }
        [HttpGet]
        [Route("")]
        public ActionResult GetConfig()
        {
            var config = new {
                EnvName = configuration["ASPNETCORE_ENVIRONMENT"],
                AllowedHosts = configuration["AllowedHosts"],
                DefaultConnection = configuration.GetConnectionString("DefaultConnection"),
                DefaultLogLevel = configuration["Logging:LogLevel:Default"],
                TestKey = configuration["TestKey"],
                SigningKey = configuration["SigningKey"],
                AttachmentOptions = attachmentOptions.CurrentValue
            }
                ;
            return Ok(config);
        }
        }
        

    }

