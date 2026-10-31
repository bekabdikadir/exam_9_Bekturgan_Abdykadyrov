using EWallet.DAL.Entities;

namespace EWallet.BLL.Dto;

public record TransactionDto(DateTime CreatedAt, TransactionType Type, decimal Amount, string? Counterparty);
