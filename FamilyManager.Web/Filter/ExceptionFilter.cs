using Microsoft.AspNetCore.Mvc.Filters;
using System.ComponentModel.DataAnnotations;
using System.Net;
using FamilyManager.Application.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;


namespace FamilyManager.Web.Filter
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var statusCode = context.Exception switch
            {
                System.ComponentModel.DataAnnotations.ValidationException => (int)HttpStatusCode.BadRequest, 
                 KeyNotFoundException => (int)HttpStatusCode.NotFound,
                _ =>(int)HttpStatusCode.InternalServerError
            };
            
            var response = new
            {
                StatusCode = statusCode,
                Messange = context.Exception.Message,
                Errors = context.Exception is FluentValidation.ValidationException validationException
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
