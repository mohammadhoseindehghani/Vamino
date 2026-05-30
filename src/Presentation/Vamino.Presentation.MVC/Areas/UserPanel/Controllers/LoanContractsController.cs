using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Vamino.Presentation.MVC.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    //[Authorize]
    public class LoanContractsController(IMediator mediator) : Controller
    {

        public async Task<IActionResult> Index()
        {
            return View(); 
        }

        public IActionResult Create() => View();
        public IActionResult Delete() => View();
        public IActionResult Edit() => View();

    }
}
