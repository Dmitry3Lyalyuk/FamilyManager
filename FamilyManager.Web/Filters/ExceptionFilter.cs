using Microsoft.AspNetCore.Mvc.Filters;
using System.ComponentModel.DataAnnotations;
using System.Net;
using FamilyManager.Application.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Expressions;



namespace FamilyManager.Web.Filter
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var statusCode = context.Exception switch
            {
                Application.Common.Exceptions.ValidationException => (int)HttpStatusCode.BadRequest,
                KeyNotFoundException => (int)HttpStatusCode.NotFound,

                _ =>(int)HttpStatusCode.InternalServerError
            };

            var response = new
            {
                StatusCode = statusCode,
                Messange = context.Exception.Message,
                Errors = context.Exception is Application.Common.Exceptions.ValidationException validationException
           ? validationException.Errors
           : null
            };

            context.Result = new JsonResult(response)
            {
                StatusCode =statusCode
            };
             context.ExceptionHandled = true;
        }
    }
}
