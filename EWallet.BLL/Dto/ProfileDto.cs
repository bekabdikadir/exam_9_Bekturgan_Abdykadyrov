namespace EWallet.BLL.Dto;

public record RequisiteDto(int Id, int VendorId, string VendorName, string Identifier, decimal Balance);

public record ProfileDto(
    string UserName,
    string? Email,
    string AccountNumber,
    decimal Balance,
    IReadOnlyList<RequisiteDto> Requisites);
