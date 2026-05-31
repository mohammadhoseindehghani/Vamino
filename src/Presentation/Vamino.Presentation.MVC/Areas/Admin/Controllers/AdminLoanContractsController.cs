using Microsoft.AspNetCore.Mvc;

namespace Vamino.Presentation.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminLoanContractsController : Controller
    {
        public IActionResult PendingBankReview()
        {
            return View();
        }
    }
}
