﻿using FamilyManager.Application.Common.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace FamilyManager.Web
{
    public class CustomExceptionHandler : IExceptionHandler
    {
        private readonly Dictionary<Type, Func<HttpContext, Exception, Task>> _exceptionHandlers;
        public CustomExceptionHandler()
        {
            _exceptionHandlers = new()
            {
                {typeof(ValidationException), HanleValidationExeption }
            };
        }
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            var exeptionType = exception.GetType();

            if (_exceptionHandlers.ContainsKey(exeptionType))
            {
                await _exceptionHandlers[exeptionType].Invoke(httpContext, exception);

                return true;
            }

            return false;
        }
        public async Task HanleValidationExeption(HttpContext httpContext, Exception ex)
        {
            var exception = (ValidationException)ex;
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            await httpContext.Response.WriteAsJsonAsync(new ValidationProblemDetails(exception.Errors)
            {
                Title = ex.Message,
                Status = StatusCodes.Status400BadRequest,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
            });
        }
    }
}
