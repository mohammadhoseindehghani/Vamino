using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Vamino.Presentation.MVC.Models;

namespace Vamino.Presentation.MVC.Controllers
{
    public class HomeController(ILogger<HomeController> logger) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult AccessDenied()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var statusCode = HttpContext.Items["AppExceptionStatusCode"] as int?;
            var message = HttpContext.Items["AppExceptionMessage"] as string;
            var errorMessages = HttpContext.Items["AppExceptionErrors"] as string[]; 

            if (statusCode == null && !string.IsNullOrEmpty(Request.Query["statusCode"]))
            {
                if (int.TryParse(Request.Query["statusCode"], out var sc))
                {
                    statusCode = sc;
                }
            }
            if (string.IsNullOrEmpty(message) && !string.IsNullOrEmpty(Request.Query["message"]))
            {
                message = Request.Query["message"];
            }

            var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            if (statusCode == null && exceptionFeature != null)
            {
                statusCode = exceptionFeature.Error switch
                {
                    BadHttpRequestException => StatusCodes.Status400BadRequest,
                    _ => StatusCodes.Status500InternalServerError
                };
                message = exceptionFeature.Error.Message;
                if (errorMessages == null)
                {
                    errorMessages = [exceptionFeature.Error.Message];
                }
            }

            var finalStatusCode = statusCode ?? HttpContext.Response.StatusCode;
            var finalMessage = string.IsNullOrEmpty(message) ? "An error occurred." : message;
            if (finalStatusCode == 500 && string.IsNullOrEmpty(finalMessage))
            {
                finalMessage = "خطای داخلی سرور";
            }

            var finalErrorMessages = errorMessages ?? Array.Empty<string>();


            var errorViewModel = new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                StatusCode = finalStatusCode,
                ErrorMessage = finalMessage,
                ValidationErrors = finalErrorMessages 
            };

            return View(errorViewModel);
        }
    }
}
