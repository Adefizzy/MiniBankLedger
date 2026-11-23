using MiniBankLedger.Domain.Enums;

namespace MiniBankLedger.Domain.DTOs;

public record class TransactionResponse(string TransactionId, TransactionType TransactionType, decimal Amount, decimal Balance);
