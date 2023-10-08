using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;

namespace Vjezba.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "Invalid JSON input");

                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                ProblemDetails problem = new()
                {
                    Status = (int)HttpStatusCode.BadRequest,
                    Type = "Error",
                    Title = "Invalid JSON input",
                    Detail = ex.Message
                };

                string json = JsonSerializer.Serialize(problem);

                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(json);
            }

            catch (DbUpdateException ex) when (ex.InnerException is Npgsql.PostgresException pgEx)
            {
                if (pgEx.SqlState == "23505")
                {                  

                    _logger.LogError(pgEx, "Unique constraint violation");

                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                    ProblemDetails problem = new()
                    {
                        Status = (int)HttpStatusCode.BadRequest,
                        Type = "Error",
                        Title = "Unique constraint violation",
                        Detail = "The provided data violates a unique constraint in the database."
                    };

                    string json = JsonSerializer.Serialize(problem);

                    context.Response.ContentType = "application/json";

                    await context.Response.WriteAsync(json);
                }
                else
                {
                    _logger.LogError(ex, "Foreign key constraint violation");

                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                    ProblemDetails problem = new()
                    {
                        Status = (int)HttpStatusCode.BadRequest,
                        Type = "Error",
                        Title = "Foreign key constraint violation",
                        Detail = ex.Message
                    };

                    string json = JsonSerializer.Serialize(problem);

                    context.Response.ContentType = "application/json";

                    await context.Response.WriteAsync(json);
                }
            }
            catch (Exception ex)
            {

                _logger.LogError(0, ex, "");

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                ProblemDetails problem = new()
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Type = "Error",
                    Title = "Error",
                    Detail = ex.Message
                };

                string json = JsonSerializer.Serialize(problem);

                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(json);
            }
        }
    }
}

