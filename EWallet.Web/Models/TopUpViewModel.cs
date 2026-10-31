using System.ComponentModel.DataAnnotations;

namespace EWallet.Web.Models;

public class TopUpViewModel
{
    [Required(ErrorMessage = "Validation_Required")]
    [RegularExpression(@"^\d{6}$", ErrorMessage = "Validation_AccountNumber")]
    [Display(Name = "Field_AccountNumber")]
    public string? AccountNumber { get; set; }

    [Required(ErrorMessage = "Validation_Required")]
    [Range(0.01, 1000000000, ErrorMessage = "Validation_Amount")]
    [Display(Name = "Field_Amount")]
    public decimal? Amount { get; set; }
}
