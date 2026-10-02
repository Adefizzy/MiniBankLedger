namespace MiniBankLedger.Domain.DTOs;

public record class WithdrawalRequest(string AccountNumber, string Amount);
