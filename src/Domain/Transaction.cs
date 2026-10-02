using System;
using MiniBankLedger.Domain.Enums;

namespace MiniBankLedger.Domain;

public class Transaction
{
    public required long TransactionId {get; init;}
    public required string AccountNumber {get; init;}
    public required decimal Amount {get; init;}
    public required decimal BalanceAfter {get; init;}
    public required TransactionType TransactionType {get; init;}
    public DateTime CreatedAt {get;} = DateTime.UtcNow;
}
