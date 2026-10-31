using EWallet.BLL.Services;
using EWallet.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;

namespace EWallet.Web.Controllers;

[Authorize]
public class WalletController : BaseController
{
    private readonly IWalletService _wallet;
    private readonly IPaymentService _payments;
    private readonly ITransactionService _transactions;

    public WalletController(
        IWalletService wallet,
        IPaymentService payments,
        ITransactionService transactions,
        IStringLocalizer<SharedResource> localizer) : base(localizer)
    {
        _wallet = wallet;
        _payments = payments;
        _transactions = transactions;
    }

    [HttpGet]
    public IActionResult Transfer()
    {
        return View(new TransferViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Transfer(TransferViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return InvalidModelJson();
        }

        var result = await _wallet.TransferAsync(CurrentUserId, model.RecipientAccount!, model.Amount!.Value);
        return ToJson(result, true);
    }

    [HttpGet]
    public async Task<IActionResult> Payment(int? vendorId, string? identifier)
    {
        var model = new PaymentViewModel
        {
            VendorId = vendorId,
            Identifier = identifier,
            Vendors = await LoadVendorsAsync()
        };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Payment(PaymentViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return InvalidModelJson();
        }

        var result = await _payments.PayAsync(CurrentUserId, model.VendorId!.Value, model.Identifier!, model.Amount!.Value);
        return ToJson(result, true);
    }

    [HttpGet]
    public async Task<IActionResult> Journal(DateTime? dateFrom, DateTime? dateTo)
    {
        var items = await _transactions.GetAsync(CurrentUserId, dateFrom, dateTo);

        var model = new JournalViewModel
        {
            DateFrom = dateFrom,
            DateTo = dateTo,
            Items = items
        };

        return View(model);
    }

    private async Task<IEnumerable<SelectListItem>> LoadVendorsAsync()
    {
        var vendors = await _payments.GetVendorsAsync();
        return vendors.Select(v => new SelectListItem(v.Name, v.Id.ToString())).ToList();
    }
}
