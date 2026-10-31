using System.ComponentModel.DataAnnotations;

namespace EWallet.Web.Models;

public class LinkRequisiteViewModel
{
    [Required(ErrorMessage = "Validation_Required")]
    [Display(Name = "Field_Vendor")]
    public int? VendorId { get; set; }

    [Required(ErrorMessage = "Validation_Required")]
    [StringLength(50, ErrorMessage = "Validation_IdentifierLength")]
    [Display(Name = "Field_Identifier")]
    public string? Identifier { get; set; }
}
