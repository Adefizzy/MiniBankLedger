using System;
using MiniBankLedger.Domain.Enums;

namespace MiniBankLedger.Domain;

public class Account
{
    public required ulong AccountNumber { get; set; }
    public required AccountType AccountType { get; set; }
    public AccountStatus Status { get; set; } = AccountStatus.Active;
    public decimal Balance { get; set; } = 0;
    public required int CustomerId { get; set; }
    public DateTime DateOpened { get; set; } = DateTime.UtcNow;
}
