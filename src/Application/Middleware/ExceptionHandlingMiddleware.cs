using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using ValidationException = FluentValidation.ValidationException;
using System.Text.Json;
using ObakiSite.Application.Shared.DTO.Response;

namespace ObakiSite.Application.Middleware
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger) => _logger = logger;
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                await HandleExceptionAsync(context, e);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            var statusCode = GetStatusCode(exception);
            var error = new ErrorResponse
            {
                Title = GetTitle(exception),
                StatusCode = statusCode,
                ExceptionMessage = exception.Message,
                Errors = GetErrors(exception)
            };

            var response = ApplicationResponse<ErrorResponse>.Fail(error);
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = statusCode;
            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
        private static int GetStatusCode(Exception exception) =>
            exception switch
            {
                ValidationException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };
        private static string GetTitle(Exception exception) =>
            exception switch
            {
                ApplicationException applicationException => applicationException.Message,
                _ => "Server Error"
            };
        private static IReadOnlyList<string> GetErrors(Exception exception)
        {
            IReadOnlyList<string> errors = new List<string>();
            if (exception is ValidationException validationException)
            {
                errors = validationException.Errors.Select(e => e.ErrorMessage).ToList();
            }
            return errors;
        }

    }
}
