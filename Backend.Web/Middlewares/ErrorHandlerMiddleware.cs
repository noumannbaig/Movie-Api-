using Backend.Application.Exceptions;
using Backend.Application.Wrappers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.IO;

namespace Backend.Web.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        ///private readonly ILogger logger;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            //this.logger = logger;
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            //context.Request.EnableBuffering();
            //var reader = new StreamReader(context.Request.Body);

            //string body = await reader.ReadToEndAsync();
            try
            {
                //logger.LogInformation($"Request Success => {context.Request?.Method}: {context.Request?.Path.Value}\n{body}");
                await _next(context);

            }
            catch (Exception error)
            {
                //logger.LogError(error,
                //    $"Request Failed => {context.Request?.Method}: {context.Request?.Path.Value}\n{body}\n{error}");
                var response = context.Response;
                response.ContentType = "application/json";
                var responseModel = new ErrorResponse<string>() { success = false, message = error?.Message };

                switch (error)
                {
                    case ApiException e:
                        // custom application error
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;

                    case ValidationException e:
                        // custom application error
                        response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                        responseModel.errors = e.Errors;
                        if (e.Errors != null)
                            responseModel.message = e.Errors[0].errorMessage.ToString();
                        break;

                    case TokenExpiryException e:
                        // custom application error
                        response.StatusCode = (int)HttpStatusCode.Forbidden;
                        responseModel.errors = e.Errors;
                        break;

                    case KeyNotFoundException e:
                        // not found error
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;

                    case NotSupportedException e:
                        // not found error
                        response.StatusCode = (int)HttpStatusCode.NotAcceptable;
                        break;

                    default:
                        // unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var result = JsonSerializer.Serialize(responseModel);

                await response.WriteAsync(result);
            }
        }
    }
}
