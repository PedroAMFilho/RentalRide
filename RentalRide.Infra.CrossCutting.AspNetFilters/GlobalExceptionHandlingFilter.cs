using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using RentalRide.Domain.UserBaseContext.Commands.Outputs;
using System.Net;

namespace RentalRide.Infra.CrossCutting.AspNetFilters
{
    public class GlobalExceptionHandlingFilter : IExceptionFilter
    {
        private readonly ILogger<GlobalExceptionHandlingFilter> _logger;
        private readonly IHostingEnvironment _hostingEnvironment;

        public GlobalExceptionHandlingFilter(
            ILogger<GlobalExceptionHandlingFilter> logger,
            IHostingEnvironment hostingEnvironment)
        {
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
        }

        public void OnException(ExceptionContext context)
        {
            #region --> Global error via json

            var status = HttpStatusCode.InternalServerError;
            var contextException = context.Exception;
            var response = context.HttpContext.Response;

            _logger.LogError(1, contextException, contextException.Message);

            CommandResult commandResult = new(
                false,
                $"Error (1). {status}: {context.Exception.Message}. " +
                $"Try again or contact system admin",
                new { });

            if (contextException.GetType() == typeof(UnauthorizedAccessException))
            {
                status = HttpStatusCode.Unauthorized;
                commandResult = new CommandResult(
                    false,
                    $"Error (2). {status}: {context.Exception.Message}" +
                    $"Try again or contact system admin",
                    new { });
            }
            else if (contextException.GetType() == typeof(NullReferenceException))
            {
                status = HttpStatusCode.NotFound;
                commandResult = new CommandResult(
                    false,
                    $"Error (3). {status}: {context.Exception.Message}" +
                    $"Try again or contact system admin",
                    new { });
            }

            response.StatusCode = (int)status;
            response.ContentType = "application/json";
            context.Result = new JsonResult(commandResult);

            #endregion
        }
    }
}
