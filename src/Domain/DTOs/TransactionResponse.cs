using MiniBankLedger.Domain.Enums;

namespace MiniBankLedger.Domain.DTOs;

public record class TransactionResponse(long TransactionId, TransactionType TransactionType, decimal Amount, decimal Balance);
