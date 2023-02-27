using AMH_MarketPlace.CustomExceptions.ExceptionsHandlers;
using System.Net;

namespace AMH_MarketPlace.CustomExceptions.Middlewares
{
    public class MiddlewareException : IMiddleware
    {
        private readonly ILogger<MiddlewareException> _logger;

        public MiddlewareException(ILogger<MiddlewareException> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (NotFoundException e)
            {
                await HandleExceptionAsync(context, e);
                _logger.LogError(e.Message);
            }
            catch (UnAuthorizeException e)
            {
                await HandleExceptionAsync(context, e);
                _logger.LogError(e.Message);
            }
            catch (NotNullException e)
            {
                await HandleExceptionAsync(context, e);
                _logger.LogError(e.Message);
            }
            catch (ForbidenException e)
            {
                await HandleExceptionAsync(context, e);
                _logger.LogError(e.Message);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(context, e);
                _logger.LogError(e.Message);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var errorResponse = new ErrorResponse();
            switch (exception)
            {
                case NotFoundException:
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    errorResponse.StatusCode = (int)HttpStatusCode.NotFound;
                    errorResponse.Message = new[] { exception.Message };
                    break;
                case UnAuthorizeException:
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    errorResponse.StatusCode = (int)HttpStatusCode.Unauthorized;
                    errorResponse.Message = new[] { exception.Message };
                    break;
                case NotNullException:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    errorResponse.StatusCode = (int)HttpStatusCode.BadRequest;
                    errorResponse.Message = new[] { exception.Message };
                    break;
                case ForbidenException:
                    context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    errorResponse.StatusCode = (int)HttpStatusCode.Forbidden;
                    errorResponse.Message = new[] { exception.Message };
                    break;
                case not null:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    errorResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                    errorResponse.Message = new[] {"Internal server error"};
                    break;
            }

            await context.Response.WriteAsJsonAsync(errorResponse);
        }
    }
}
