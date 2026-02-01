using TaskCollaboration.Api.DTOs;
using System;
using TaskCollaboration.Api.Exceptions;
using System.Net;
using TaskCollaboration.Api.Exceptions.Base;



namespace TaskCollaboration.Api.Middleware
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
            context.Response.ContentType = "application/json";

            var statusCode = (int)HttpStatusCode.InternalServerError; // Varsayılan 500

            var message = _env.IsDevelopment() ? exception.Message : "Internal Server Error";

            string? stackTrace = _env.IsDevelopment() ? exception.StackTrace : null;

            switch (exception)
            {
                case NotFoundException notFoundException:
                    statusCode = (int)HttpStatusCode.NotFound; // 404
                    message = notFoundException.Message;
                    break;
                case ConflictException conflictException:
                    statusCode = (int)HttpStatusCode.Conflict; // 409
                    message = conflictException.Message;
                    break;
                // Diğer özel exception'lar buraya eklenebilir
                default:
                    break;
            }

            context.Response.StatusCode = statusCode;

            var response = new ApiErrorResponse
            {
                StatusCode = statusCode,
                Message = message,
                StackTrace = stackTrace
            };

            var json = System.Text.Json.JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(json);
        }

        // ...existing code...erine; eğer geliştirme modundaysan (_env.IsDevelopment()) gerçek hatayı (exception.Message) görmek daha faydalı olur.


    };



}





