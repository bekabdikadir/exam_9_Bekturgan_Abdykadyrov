using System.Globalization;
using System.Security.Claims;
using EWallet.BLL.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace EWallet.Web.Controllers;

public abstract class BaseController : Controller
{
    protected BaseController(IStringLocalizer<SharedResource> localizer)
    {
        Loc = localizer;
    }

    protected IStringLocalizer<SharedResource> Loc { get; }

    protected string CurrentUserId => User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;

    protected IActionResult ToJson(OperationResult result, bool includeBalance)
    {
        var args = result.Args
            .Select(a => a is decimal d ? (object)d.ToString("N2", CultureInfo.CurrentUICulture) : a)
            .ToArray();

        var message = Loc[result.MessageKey, args].Value;

        string? balance = includeBalance && result.Balance.HasValue
            ? result.Balance.Value.ToString("N2", CultureInfo.CurrentUICulture)
            : null;

        return Json(new { success = result.Succeeded, message, balance });
    }

    protected IActionResult InvalidModelJson()
    {
        var errors = ModelState.Values
            .SelectMany(v => v.Errors)
            .Select(e => e.ErrorMessage)
            .Where(m => !string.IsNullOrWhiteSpace(m))
            .Distinct();

        return Json(new { success = false, message = string.Join(" ", errors), balance = (string?)null });
    }
}
