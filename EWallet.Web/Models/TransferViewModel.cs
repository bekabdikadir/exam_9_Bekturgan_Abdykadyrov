using System.ComponentModel.DataAnnotations;

namespace EWallet.Web.Models;

public class TransferViewModel
{
    [Required(ErrorMessage = "Validation_Required")]
    [RegularExpression(@"^\d{6}$", ErrorMessage = "Validation_AccountNumber")]
    [Display(Name = "Field_RecipientAccount")]
    public string? RecipientAccount { get; set; }

    [Required(ErrorMessage = "Validation_Required")]
    [Range(0.01, 1000000000, ErrorMessage = "Validation_Amount")]
    [Display(Name = "Field_Amount")]
    public decimal? Amount { get; set; }
}
