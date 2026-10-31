using EWallet.BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace EWallet.Web.Controllers;

[Authorize]
public class ProfileController : BaseController
{
    private readonly IProfileService _profile;

    public ProfileController(IProfileService profile, IStringLocalizer<SharedResource> localizer) : base(localizer)
    {
        _profile = profile;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var profile = await _profile.GetProfileAsync(CurrentUserId);
        if (profile is null)
        {
            return RedirectToAction("Index", "Home");
        }

        return View(profile);
    }
}
