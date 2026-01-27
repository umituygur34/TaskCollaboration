using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TaskCollaboration.Api.api.DTOs;
using System;



namespace TaskCollaboration.Api.api.Middleware
{
    public class ExceptionMiddleware
    {

        //Zincirdeki bir sonraki halka
        private readonly RequestDelegate _next;

        //Hatayı console'a yazmak için
        private readonly ILogger<ExceptionMiddleware> _logger;

        //Geliştirme modunda mıyızz anlamak için?
        public readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                //İsteği bir sonraki adıma gönder(Controller'a gider..)
                await _next(context);
            }
            catch (Exception ex)
            {
                //Bir hata var sırada ben varım.. 

                //Hatayı log'a yaz
                _logger.LogError(ex, ex.Message);


                //Kullanıcıya nazikçe bildirelim
                await HandleExceptionAsync(context, ex);

            }

        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {

            // Burada ApiException DTO'sunu kullanarak 
            // JSON formatında bir cevap döneceksin.

            context.Response.ContentType = "application/json";

            //Global hatalar 500 olur genelde.
            context.Response.StatusCode = 500;

            var response = new ApiException
            {
                StatusCode = context.Response.StatusCode,
                //Message = "Internal Server Error" yerine; eğer geliştirme modundaysan (_env.IsDevelopment()) gerçek hatayı (exception.Message) görmek daha faydalı olur.
                Message = _env.IsDevelopment() ? exception.Message : "Internal Server Error",
                StackTrace = _env.IsDevelopment() ? exception.StackTrace : null


            };

            var json = System.Text.Json.JsonSerializer.Serialize(response);

            await context.Response.WriteAsync(json);


        }




    }
}