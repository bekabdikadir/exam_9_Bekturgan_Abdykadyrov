using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EWallet.Web.Models;

public class PaymentViewModel
{
    [Required(ErrorMessage = "Validation_Required")]
    [Display(Name = "Field_Vendor")]
    public int? VendorId { get; set; }

    [Required(ErrorMessage = "Validation_Required")]
    [StringLength(50, ErrorMessage = "Validation_IdentifierLength")]
    [Display(Name = "Field_Identifier")]
    public string? Identifier { get; set; }

    [Required(ErrorMessage = "Validation_Required")]
    [Range(0.01, 1000000000, ErrorMessage = "Validation_Amount")]
    [Display(Name = "Field_Amount")]
    public decimal? Amount { get; set; }

    public IEnumerable<SelectListItem>? Vendors { get; set; }
}
