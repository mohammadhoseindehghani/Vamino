using System.Net;
using Vamino.Application.Contracts._Common;

namespace Vamino.Presentation.MVC.Middlewares;

public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (AppException ex)
        {
            logger.LogError(ex, "AppException occurred. Status: {StatusCode}, Message: {Message}", ex.StatusCode, ex.Message);

            await HandleAppExceptionAsync(context, ex);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An unexpected error occurred.");

            await HandleUnexpectedExceptionAsync(context, ex);
        }
    }

    private Task HandleAppExceptionAsync(HttpContext context, AppException ex)
    {
        context.Response.StatusCode = ex.StatusCode;

        context.Items["AppExceptionStatusCode"] = ex.StatusCode;
        context.Items["AppExceptionMessage"] = ex.Message; 

        context.Items["AppExceptionErrors"] = ex.Errors; 

        var redirectMessage = string.IsNullOrEmpty(ex.Message) ? "An error occurred." : ex.Message;

        var redirectUrl = $"/Home/Error?statusCode={ex.StatusCode}&message={WebUtility.UrlEncode(redirectMessage)}";

        context.Response.Redirect(redirectUrl);

        return Task.CompletedTask;
    }

    private Task HandleUnexpectedExceptionAsync(HttpContext context, Exception ex)
    {
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        context.Items["AppExceptionStatusCode"] = (int)HttpStatusCode.InternalServerError;
        context.Items["AppExceptionMessage"] = "خطای داخلی سرور";

        context.Items["AppExceptionErrors"] = new string[] { "A server error occurred. Please try again later." };


        var redirectUrl = $"/Home/Error?statusCode={(int)HttpStatusCode.InternalServerError}&message={WebUtility.UrlEncode("خطای داخلی سرور")}";

        context.Response.Redirect(redirectUrl);

        return Task.CompletedTask;
    }

}