using System.Security.Claims;
using EWallet.BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace EWallet.Web.ViewComponents;

public class BalanceViewComponent : ViewComponent
{
    private readonly IWalletService _wallet;

    public BalanceViewComponent(IWalletService wallet)
    {
        _wallet = wallet;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        if (UserClaimsPrincipal.Identity?.IsAuthenticated != true)
        {
            return Content(string.Empty);
        }

        var userId = UserClaimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
        var balance = await _wallet.GetBalanceAsync(userId);

        return View(balance);
    }
}
