using System.ComponentModel.DataAnnotations;

namespace EWallet.Web.Models;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Validation_Required")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Validation_UserNameLength")]
    [Display(Name = "Field_UserName")]
    public string? UserName { get; set; }

    [Required(ErrorMessage = "Validation_Required")]
    [EmailAddress(ErrorMessage = "Validation_Email")]
    [Display(Name = "Field_Email")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Validation_Required")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Validation_PasswordLength")]
    [DataType(DataType.Password)]
    [Display(Name = "Field_Password")]
    public string? Password { get; set; }

    [Required(ErrorMessage = "Validation_Required")]
    [Compare(nameof(Password), ErrorMessage = "Validation_PasswordCompare")]
    [DataType(DataType.Password)]
    [Display(Name = "Field_ConfirmPassword")]
    public string? ConfirmPassword { get; set; }
}
