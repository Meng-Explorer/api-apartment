using APARTMENT_API.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;

namespace APARTMENT_API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _request;

        public ExceptionMiddleware(RequestDelegate request) 
        {
            _request = request;
        }
        private static async Task HandleException(
            HttpContext context,
            HttpStatusCode statusCode,
            string message,
            List<string>? errors = null
        )
        {
            // If the response has already started we cannot change headers or status code.
            // Avoid writing anything further to the body to prevent corrupting the existing output.
            if (context.Response.HasStarted)
            {
                // Nothing we can do here: headers and status are locked. Return and let the existing response flow.
                return;
            }

            // Reset any existing response before writing error details
            try
            {
                context.Response.Clear();
            }
            catch
            {
                // ignore clear failures
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var response = new
            {
                Success = false,
                StatusCode = (int)statusCode,
                Message = message,
                Data = new { }
            };

            var json = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(json);

        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _request(context);
            }
            catch (BadRequestException ex)
            {
                if (ex.Errors != null && ex.Errors.Count > 0)
                {
                    if (context.Response.HasStarted)
                    {
                        return;
                    }

                    context.Response.Clear();
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    var response = new
                    {
                        success = false,
                        statusCode = 400,
                        message = ex.Message,
                        errors = ex.Errors
                    };
                    var json = JsonSerializer.Serialize(response);
                    await context.Response.WriteAsync(json);
                }
                else
                {
                    await HandleException(context, HttpStatusCode.BadRequest, ex.Message);
                }
            }
            catch (UnauthorizeException ex)
            {
                await HandleException(context, HttpStatusCode.Unauthorized, ex.Message);
            }
            catch (NotFoundException ex)
            {
                await HandleException(context, HttpStatusCode.NotFound, ex.Message);
            }
            catch (APARTMENT_API.Exceptions.ValidationException ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = 400;
                var response = new
                {
                    Success = false,
                    StatusCode = 400,
                    Message = ex.Message,
                    Errors = ex.Errors
                };
                var json = JsonSerializer.Serialize(response);
                await context.Response.WriteAsync(json);
            }
            catch (Exception ex)
            {
                await HandleException(context, HttpStatusCode.InternalServerError, ex.Message);

            }


        }

    }
}
