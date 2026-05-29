using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vamino.Application.Contracts.DTOs.Identity;
using Vamino.Application.Features.Authentication.Commands.Login;
using Vamino.Application.Features.User.Commands.RegisterUser;
using Vamino.Presentation.MVC.Areas.Auth.Models;

namespace Vamino.Presentation.MVC.Areas.Auth.Controllers
{
    [Area("Auth")]
    public class AuthController(IMediator mediator) : Controller
    {
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var command = new LoginCommand( new LoginRequestDto(model.UserNameOrEmailOrPhone, model.Password));

            var result = await mediator.Send(command);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                }

                return View(model);
            }

            TempData["Success"] = "ورود با موفقیت انجام شد";

            return RedirectToAction("Index", "Home", new { area = "" });
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var command = new RegisterUserCommand(new RegisterUserRequestDto
            (
                model.FirstName,
                model.LastName,
                model.NationalCode,
                model.PhoneNumber,
                model.Email,
                model.Password,
                model.ConfirmPassword));

            var result = await mediator.Send(command);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                }

                return View(model);
            }

            TempData["Success"] = "ثبت نام با موفقیت انجام شد";

            return RedirectToAction(nameof(Login));
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            TempData["Success"] = "خروج با موفقیت انجام شد";

            return RedirectToAction(nameof(Login));
        }
    }
}
