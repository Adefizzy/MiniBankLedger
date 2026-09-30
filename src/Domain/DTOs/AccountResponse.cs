using System;
using MiniBankLedger.Domain.Enums;

namespace MiniBankLedger.Domain.DTOs;

public record class AccountResponse(
    string AccountNumber,
    AccountType AccountType,
    AccountStatus Status,
    decimal Balance,
    int CustomerId
  );

