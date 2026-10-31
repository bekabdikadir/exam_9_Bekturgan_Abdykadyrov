using System.ComponentModel.DataAnnotations;
using EWallet.BLL.Dto;

namespace EWallet.Web.Models;

public class JournalViewModel
{
    [DataType(DataType.Date)]
    [Display(Name = "Field_DateFrom")]
    public DateTime? DateFrom { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "Field_DateTo")]
    public DateTime? DateTo { get; set; }

    public IReadOnlyList<TransactionDto> Items { get; set; } = Array.Empty<TransactionDto>();
}
