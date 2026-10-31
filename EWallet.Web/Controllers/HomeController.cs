using EWallet.BLL.Services;
using EWallet.Web.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace EWallet.Web.Controllers;

public class HomeController : BaseController
{
    private static readonly string[] SupportedCultures = { "ru", "en" };

    private readonly IWalletService _wallet;

    public HomeController(IWalletService wallet, IStringLocalizer<SharedResource> localizer) : base(localizer)
    {
        _wallet = wallet;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View(new TopUpViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> TopUp(TopUpViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return InvalidModelJson();
        }

        var result = await _wallet.TopUpAsync(model.AccountNumber!, model.Amount!.Value);
        return ToJson(result, false);
    }

    [HttpGet]
    public IActionResult SetLanguage(string culture, string? returnUrl)
    {
        if (!SupportedCultures.Contains(culture))
        {
            culture = "ru";
        }

        Response.Cookies.Append(
            CookieRequestCultureProvider.DefaultCookieName,
            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
            new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddYears(1),
                IsEssential = true,
                SameSite = SameSiteMode.Lax
            });

        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
        {
            return LocalRedirect(returnUrl);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Error()
    {
        return View();
    }
}
