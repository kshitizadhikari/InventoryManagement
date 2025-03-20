using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Net;
using System.Text.Json;

namespace WebAPI.Middlewares
{
    public class ExceptionMiddleware(RequestDelegate next)
    {
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        public static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            var defaultErrorCode = (int)HttpStatusCode.InternalServerError;
            ErrorResponse errorResponse;

            switch (ex)
            {
                case DbUpdateException dbEx:
                    errorResponse = HandleDbUpdateException(dbEx);
                    break;

                default:
                    errorResponse = new ErrorResponse
                    {
                        ErrorCode = defaultErrorCode,
                        Title = "Unexpected Error",
                        Message = "An unexpected error has occured. Please try again later."
                    };
                    break;
            }

            response.StatusCode = errorResponse.ErrorCode;
            var jsonResponse = JsonSerializer.Serialize(errorResponse);
            return response.WriteAsync(jsonResponse);
        }

        private static ErrorResponse HandleDbUpdateException(DbUpdateException dbEx)
        {
            if (dbEx.InnerException is PostgresException pgEx && pgEx.SqlState == "23505")
            {
                return new ErrorResponse
                {
                    ErrorCode = (int)HttpStatusCode.Conflict, // 409
                    Title = "Duplicate Entry",
                    Message = "A record with the same values already exists."
                };
            }

            return new ErrorResponse
            {
                ErrorCode = (int)HttpStatusCode.InternalServerError,
                Title = "Database Error",
                Message = "A database error occured. Please try again later."
            };
        }
    }
}
