using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vamino.Application.Features.LoanGuarantor.Commands.Approve;
using Vamino.Application.Features.LoanGuarantor.Commands.Reject;
using Vamino.Application.Features.LoanGuarantor.Queries.GetAllByUserId;
using Vamino.Application.Features.LoanGuarantor.Queries.GetById;
using Vamino.Domain.LoanGuarantorAgg.Enums;
using Vamino.Infrastructure.Identity.Extension;
using Vamino.Presentation.MVC.Areas.UserPanel.Models;

namespace Vamino.Presentation.MVC.Areas.UserPanel.Controllers
{

    [Area("UserPanel")]
    [Authorize]
    public class GuarantorRequestsController(IMediator mediator) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index(
            string? search,
            GuarantorStatus? status,
            CancellationToken ct)
        {
            var domainUserId = User.GetDomainUserId();

            if (!domainUserId.HasValue)
            {
                TempData["Error"] = "شناسه کاربر در سیستم یافت نشد. لطفاً دوباره وارد حساب کاربری شوید.";
                return RedirectToAction("Index", "Home", new { area = "UserPanel" });
            }

            var result = await mediator.Send(
                new GetLoanGuarantorsByUserIdQuery(domainUserId.Value),
                ct);

            if (!result.IsSuccess)
            {
                TempData["Error"] = result.Message ?? "دریافت درخواست‌های ضمانت با خطا مواجه شد.";

                return View(new GuarantorRequestsIndexViewModel
                {
                    Search = search,
                    Status = status,
                    Items = []
                });
            }

            var items = result.Data
                .Select(x => new GuarantorRequestItemViewModel
                {
                    Id = x.Id,
                    LoanContractId = x.LoanContractId,
                    UserId = x.UserId,
                    Status = x.Status,
                    Note = x.Note,
                    RespondedAt = x.RespondedAt,
                    CreatedAt = x.CreatedAt,

                    ContractTitle = $"قرارداد شماره {x.LoanContractId}",
                    Amount = null
                })
                .AsQueryable();

            if (status.HasValue)
            {
                items = items.Where(x => x.Status == status.Value);
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                var normalizedSearch = search.Trim();

                items = items.Where(x =>
                    x.Id.ToString().Contains(normalizedSearch) ||
                    x.LoanContractId.ToString().Contains(normalizedSearch) ||
                    x.ContractTitle.Contains(normalizedSearch) ||
                    (x.Note != null && x.Note.Contains(normalizedSearch)));
            }

            var model = new GuarantorRequestsIndexViewModel
            {
                Search = search,
                Status = status,
                Items = items
                    .OrderByDescending(x => x.CreatedAt)
                    .ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Approve(ApproveGuarantorRequestViewModel model, CancellationToken ct)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "درخواست نامعتبر است.";
                return RedirectToAction(nameof(Index));
            }

            var ownershipCheck = await EnsureCurrentUserOwnsGuarantorRequestAsync(model.Id, ct);
            if (ownershipCheck is not null)
                return ownershipCheck;

            var result = await mediator.Send(new ApproveLoanGuarantorCommand(model.Id), ct);

            if (result.IsSuccess)
                TempData["Success"] = result.Message ?? "درخواست ضمانت تایید شد.";
            else
                TempData["Error"] = result.Message ?? "تایید درخواست ضمانت با شکست مواجه شد.";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reject(RejectGuarantorRequestViewModel model, CancellationToken ct)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "درخواست نامعتبر است.";
                return RedirectToAction(nameof(Index));
            }

            var ownershipCheck = await EnsureCurrentUserOwnsGuarantorRequestAsync(model.Id, ct);
            if (ownershipCheck is not null)
                return ownershipCheck;

            var result = await mediator.Send(new RejectLoanGuarantorCommand(model.Id), ct);

            if (result.IsSuccess)
                TempData["Success"] = result.Message ?? "درخواست ضمانت رد شد.";
            else
                TempData["Error"] = result.Message ?? "رد درخواست ضمانت با شکست مواجه شد.";

            return RedirectToAction(nameof(Index));
        }

        private async Task<IActionResult?> EnsureCurrentUserOwnsGuarantorRequestAsync(int guarantorRequestId, CancellationToken ct)
        {
            var domainUserId = User.GetDomainUserId();

            if (!domainUserId.HasValue)
            {
                TempData["Error"] = "شناسه کاربر در سیستم یافت نشد. لطفاً دوباره وارد حساب کاربری شوید.";
                return RedirectToAction("Index", "Home", new { area = "UserPanel" });
            }

            var result = await mediator.Send(new GetLoanGuarantorByIdQuery(guarantorRequestId), ct);

            if (!result.IsSuccess || result.Data is null)
            {
                TempData["Error"] = result.Message ?? "درخواست ضمانت یافت نشد.";
                return RedirectToAction(nameof(Index));
            }

            if (result.Data.UserId != domainUserId.Value)
            {
                TempData["Error"] = "شما مجاز به انجام عملیات روی این درخواست ضمانت نیستید.";
                return RedirectToAction(nameof(Index));
            }

            return null;
        }
    }
}
