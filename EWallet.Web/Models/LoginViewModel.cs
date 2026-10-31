using System.ComponentModel.DataAnnotations;

namespace EWallet.Web.Models;

public class LoginViewModel
{
    [Required(ErrorMessage = "Validation_Required")]
    [Display(Name = "Field_Login")]
    public string? Login { get; set; }

    [Required(ErrorMessage = "Validation_Required")]
    [DataType(DataType.Password)]
    [Display(Name = "Field_Password")]
    public string? Password { get; set; }

    [Display(Name = "Field_RememberMe")]
    public bool RememberMe { get; set; }
}
