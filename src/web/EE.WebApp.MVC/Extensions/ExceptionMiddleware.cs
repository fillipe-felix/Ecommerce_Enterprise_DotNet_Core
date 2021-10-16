using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace EE.WebApp.MVC.Extensions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _requestDelegate;

        public ExceptionMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        /// <summary>
        /// Trata o que vou fazer dentro do meu middleware
        /// </summary>
        /// <param name="httpContext"></param>
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _requestDelegate(httpContext);
            }
            catch (CustomHttpResquestException ex)
            {
                HandleRequestExceptionAsync(httpContext, ex);
            }
        }

        /// <summary>
        /// Verifica o statusCode do erro e faz uma verificação
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="customHttpResquestException"></param>
        private static void HandleRequestExceptionAsync(HttpContext httpContext, CustomHttpResquestException customHttpResquestException)
        {
            if (customHttpResquestException.StatusCode == HttpStatusCode.Unauthorized)
            {
                httpContext.Response.Redirect($"/login?ReturnUrl={httpContext.Request.Path}");
                return;
            }

            httpContext.Response.StatusCode = (int)customHttpResquestException.StatusCode;
        }
    }
}