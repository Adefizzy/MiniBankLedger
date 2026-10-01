namespace MiniBankLedger.Domain.DTOs;

public record class DepositRequest(string AccountNumber, string Amount);

