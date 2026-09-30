using MiniBankLedger.Domain.Enums;

namespace MiniBankLedger.Domain.DTOs;

public record class CreateAccountRequest(
    string AccountType, 
    string CustomerId
    );
