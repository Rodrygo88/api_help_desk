
using System.Net;
using helpDesk.Dtos;
using helpDesk.Exceptions;

namespace helpDesk.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(
            RequestDelegate next,
            ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu uma exceção.");

                if (context.Response.HasStarted)
                    throw;

                context.Response.Clear(); 
                int statusCode;

                switch (ex)
                {
                    case NotFoundException:
                        statusCode = (int)HttpStatusCode.NotFound;
                        break;

                    case EmailExistsException:
                        statusCode = (int)HttpStatusCode.BadRequest;
                        break;

                    case UnauthorizedAccessException:
                        statusCode = (int)HttpStatusCode.Forbidden;
                        break;

                    case InvalidOperationException:
                        statusCode = (int)HttpStatusCode.BadRequest;
                        break;

                    default:
                        statusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var message = statusCode == 500
                    ? "Erro interno no servidor."
                    : ex.Message;

                context.Response.StatusCode = statusCode;

                await context.Response.WriteAsJsonAsync(
                    new ResponseDto
                    {
                        Errors = [message]
                    });
            }
        }
    }

}

