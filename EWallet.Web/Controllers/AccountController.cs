using EWallet.BLL.Services;
using EWallet.DAL.Entities;
using EWallet.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace EWallet.Web.Controllers;

public class AccountController : BaseController
{
    private readonly IAccountService _accounts;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AccountController(
        IAccountService accounts,
        SignInManager<ApplicationUser> signInManager,
        IStringLocalizer<SharedResource> localizer) : base(localizer)
    {
        _accounts = accounts;
        _signInManager = signInManager;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View(new RegisterViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var result = await _accounts.RegisterAsync(model.UserName!, model.Email!, model.Password!);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, string.IsNullOrEmpty(error.Description) ? Loc["Error_Unexpected"].Value : error.Description);
            }

            return View(model);
        }

        await _signInManager.SignInAsync(result.User!, isPersistent: false);
        TempData["AccountNumber"] = result.AccountNumber;

        return RedirectToAction(nameof(Registered));
    }

    [HttpGet]
    [Authorize]
    public IActionResult Registered()
    {
        var number = TempData["AccountNumber"] as string;
        if (string.IsNullOrEmpty(number))
        {
            return RedirectToAction("Index", "Home");
        }

        return View(model: number);
    }

    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View(new LoginViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user = await _accounts.FindByLoginAsync(model.Login!);
        var result = user is null
            ? Microsoft.AspNetCore.Identity.SignInResult.Failed
            : await _signInManager.PasswordSignInAsync(user, model.Password!, model.RememberMe, lockoutOnFailure: false);

        if (result.Succeeded)
        {
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return LocalRedirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        ModelState.AddModelError(string.Empty, Loc["Error_LoginFailed"]);
        return View(model);
    }

    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}
