using EWallet.BLL.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EWallet.Web.Models;

public class ProfileViewModel
{
    public ProfileDto Profile { get; set; } = null!;
    public LinkRequisiteViewModel Link { get; set; } = new();
    public IEnumerable<SelectListItem> Vendors { get; set; } = Array.Empty<SelectListItem>();
}
