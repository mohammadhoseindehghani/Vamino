using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Vamino.Application.Contracts._Common;
using Vamino.Application.Contracts.DTOs.LoanContract;
using Vamino.Application.Contracts.DTOs.LoanGuarantor;
using Vamino.Application.Contracts.DTOs.User;
using Vamino.Application.Features.LoanContract.Commands.Create;
using Vamino.Application.Features.LoanContract.Commands.Delete;
using Vamino.Application.Features.LoanContract.Commands.Update;
using Vamino.Application.Features.LoanContract.Queries.GetAllByUserId;
using Vamino.Application.Features.LoanContract.Queries.GetLoanContract;
using Vamino.Application.Features.LoanGuarantor.Commands.Create;
using Vamino.Application.Features.LoanGuarantor.Queries.GetAllByContractId;
using Vamino.Application.Features.User.Queries.FindByNational;
using Vamino.Application.Features.User.Queries.FindByPhone;
using Vamino.Domain.LoanContractAgg.Enums;
using Vamino.Infrastructure.Identity.Models;
using Vamino.Presentation.MVC.Areas.UserPanel.Models;


namespace Vamino.Presentation.MVC.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class LoanContractsController(IMediator mediator) : Controller
    {

        [HttpGet]
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var currentUserId = GetCurrentUserId();
            if (currentUserId is null)
                return Challenge();

            var result = await mediator.Send(new GetAllByUserIdQuery(currentUserId.Value), cancellationToken);

            var contracts = result.IsSuccess ? result.Data : new List<LoanContractDto>();

            if (!result.IsSuccess)
                TempData["ErrorMessage"] = result.Message;

            var vm = new LoanContractIndexViewModel
            {
                Contracts = contracts
            };

            return View(vm);
        }


        [HttpGet]
        public IActionResult Create() => View();


        [HttpGet]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var currentUserId = GetCurrentUserId();
            if (currentUserId is null)
                return Challenge();

            var getResult = await mediator.Send(new GetLoanContractQuery(id), cancellationToken);

            if (!getResult.IsSuccess)
            {
                TempData["ErrorMessage"] = getResult.Message;
                return RedirectToAction(nameof(Index));
            }

            var contract = getResult.Data;

            if (contract.BorrowerId != currentUserId.Value)
                return Forbid();

            var model = new DeleteLoanContractViewModel
            {
                Id = contract.Id,
                Title = contract.Title,
                Amount = contract.Amount,
                LoanStatus = contract.LoanStatus
            };

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetLoanContractQuery(id), cancellationToken);

            if (!result.IsSuccess)
            {
                TempData["ErrorMessage"] = result.Message;
                return RedirectToAction(nameof(Index));
            }

            var contract = result.Data;

            var model = new UpdateLoanContractViewModel
            {
                Id = contract.Id,
                Title = contract.Title,
                Amount = contract.Amount,
                Description = contract.Description
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetGuarantors(int contractId, CancellationToken cancellationToken)
        {
            if (contractId <= 0)
                return BadRequest(new { message = "شناسه قرارداد نامعتبر است." });

            var currentUserId = GetCurrentUserId();

            var contractResult = await mediator.Send(new GetLoanContractQuery(contractId), cancellationToken);

            if (!contractResult.IsSuccess || contractResult.Data is null)
                return NotFound(new { message = contractResult.Message ?? "قرارداد یافت نشد." });

            var contract = contractResult.Data;

            if (contract.BorrowerId != currentUserId)
                return Forbid();

            var guarantorsResult = await mediator.Send(new GetLoanGuarantorsByContractIdQuery(contractId), cancellationToken);

            if (!guarantorsResult.IsSuccess)
                return BadRequest(new { message = guarantorsResult.Message ?? "خطا در دریافت لیست ضامن‌ها" });

            return Ok(guarantorsResult.Data ?? new List<LoanGuarantorDto>());
        }



        [HttpGet]
        public async Task<IActionResult> AddGuarantor(int id, CancellationToken ct)
        {
            var currentUserId = GetCurrentUserId();
            if (currentUserId is null)
                return Challenge();

            var contractResult = await mediator.Send(new GetLoanContractQuery(id), ct);
            if (!contractResult.IsSuccess || contractResult.Data is null)
                return NotFound();

            var contract = contractResult.Data;

            if (contract.BorrowerId != currentUserId.Value)
                return Forbid();

            if (contract.LoanStatus != LoanStatus.PendingForGuarantors)
            {
                TempData["ErrorMessage"] = "در وضعیت فعلی امکان افزودن ضامن وجود ندارد.";
                return RedirectToAction(nameof(Index));
            }

            var vm = new AddLoanGuarantorViewModel
            {
                LoanContractId = id,
                ContractTitle = contract.Title,
                ContractAmount = contract.Amount
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddGuarantor(AddLoanGuarantorViewModel model, CancellationToken ct)
        {
            var currentUserId = GetCurrentUserId();
            if (currentUserId is null)
                return Challenge();

            var contractResult = await mediator.Send(new GetLoanContractQuery(model.LoanContractId), ct);
            if (!contractResult.IsSuccess || contractResult.Data is null)
                return NotFound();

            var contract = contractResult.Data;

            if (contract.BorrowerId != currentUserId.Value)
                return Forbid();

            if (contract.LoanStatus != LoanStatus.PendingForGuarantors)
            {
                model.ErrorMessage = "در وضعیت فعلی امکان افزودن ضامن وجود ندارد.";
                return View(model);
            }

            model.ContractTitle = contract.Title;
            model.ContractAmount = contract.Amount;

            var hasNational = !string.IsNullOrWhiteSpace(model.NationalCode);
            var hasPhone = !string.IsNullOrWhiteSpace(model.PhoneNumber);

            if (!hasNational && !hasPhone)
            {
                model.ErrorMessage = "برای جستجو، کد ملی یا شماره موبایل را وارد کنید.";
                return View(model);
            }

            if (hasNational && hasPhone)
            {
                model.ErrorMessage = "فقط یکی از فیلدهای کد ملی یا شماره موبایل را پر کنید.";
                return View(model);
            }

            Result<UserSearchResultDto> searchResult;

            if (hasNational)
                searchResult = await mediator.Send(new FindUserForGuarantorByNationalCodeQuery(model.NationalCode!), ct);
            else
                searchResult = await mediator.Send(new FindUserForGuarantorByPhoneQuery(model.PhoneNumber!), ct);

            if (!searchResult.IsSuccess || searchResult.Data is null)
            {
                model.ErrorMessage = searchResult.Message;
                model.FoundUser = null;
                return View(model);
            }

            if (searchResult.Data.Id == currentUserId.Value)
            {
                model.ErrorMessage = "شما نمی‌توانید ضامن قرارداد خودتان باشید.";
                model.FoundUser = null;
                return View(model);
            }

            model.FoundUser = searchResult.Data;
            model.SuccessMessage = "کاربر پیدا شد. می‌توانید درخواست ضمانت را ارسال کنید.";

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateGuarantor(CreateLoanGuarantorViewModel model, CancellationToken ct)
        {
            var currentUserId = GetCurrentUserId();
            if (currentUserId is null)
                return Challenge();

            var contractResult = await mediator.Send(new GetLoanContractQuery(model.LoanContractId), ct);
            if (!contractResult.IsSuccess || contractResult.Data is null)
                return NotFound();

            var contract = contractResult.Data;

            if (contract.BorrowerId != currentUserId.Value)
                return Forbid();

            if (contract.LoanStatus != LoanStatus.PendingForGuarantors)
            {
                TempData["ErrorMessage"] = "در وضعیت فعلی امکان افزودن ضامن وجود ندارد.";
                return RedirectToAction(nameof(Index));
            }

            var command = new CreateLoanGuarantorCommand(
                new CreateLoanGuarantorDto(model.LoanContractId, model.UserId, model.Note)
            );

            var result = await mediator.Send(command, ct);

            if (!result.IsSuccess)
            {
                TempData["ErrorMessage"] = result.Message;
                return RedirectToAction(nameof(AddGuarantor), new { id = model.LoanContractId });
            }

            TempData["SuccessMessage"] = result.Message;
            return RedirectToAction(nameof(AddGuarantor), new { id = model.LoanContractId });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateLoanContractViewModel model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return View(model);

            var borrowerId = GetCurrentUserId();

            if (borrowerId is null)
            {
                ModelState.AddModelError(string.Empty, "امکان شناسایی کاربر وجود ندارد. لطفاً دوباره وارد حساب کاربری خود شوید.");
                return View(model);
            }

            var dto = new CreateLoanContractDto(
                Title: model.Title,
                BorrowerId: borrowerId.Value,
                Amount: model.Amount,
                Description: model.Description
            );

            var command = new CreateLoanContractCommand(dto);

            var result = await mediator.Send(command, cancellationToken);

            if (!result.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View(model);
            }

            TempData["SuccessMessage"] = result.Message;

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateLoanContractViewModel model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return View(model);

            var dto = new UpdateLoanContractDto(
                Title: model.Title,
                Amount: model.Amount,
                Description: model.Description
            );

            var result = await mediator.Send(
                new UpdateLoanContractCommand(model.Id, dto),
                cancellationToken
            );

            if (!result.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View(model);
            }

            TempData["SuccessMessage"] = result.Message;
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(DeleteLoanContractViewModel model, CancellationToken cancellationToken)
        {
            var currentUserId = GetCurrentUserId();
            if (currentUserId is null)
                return Challenge();

            var getResult = await mediator.Send(new GetLoanContractQuery(model.Id), cancellationToken);

            if (!getResult.IsSuccess)
            {
                TempData["ErrorMessage"] = getResult.Message;
                return RedirectToAction(nameof(Index));
            }

            if (getResult.Data.BorrowerId != currentUserId.Value)
                return Forbid();

            var deleteResult = await mediator.Send(new DeleteLoanContractCommand(model.Id), cancellationToken);

            if (!deleteResult.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, deleteResult.Message);

                model.Title = getResult.Data.Title;
                model.Amount = getResult.Data.Amount;
                model.LoanStatus = getResult.Data.LoanStatus;

                return View(model);
            }

            TempData["SuccessMessage"] = deleteResult.Message;
            return RedirectToAction(nameof(Index));
        }









        private int? GetCurrentUserId()
        {
            var userIdClaim = User.FindFirstValue(CustomClaimTypes.DomainUserId);
            return int.TryParse(userIdClaim, out var userId) ? userId : null;
        }

    }
}
