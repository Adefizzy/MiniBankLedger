using System;

namespace MiniBankLedger.Domain;

public class Transaction
{
    public required long TransactionId {get; init;}
    public required ulong AccountNumber {get; init;}
    public required decimal Amount {get; init;}
    public required decimal BalanceAfter {get; init;}
    public DateTime CreatedAt {get;} = DateTime.UtcNow;
}
