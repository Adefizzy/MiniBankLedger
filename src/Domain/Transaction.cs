using System;
using MiniBankLedger.Domain.Enums;

namespace MiniBankLedger.Domain;

public class Transaction
{
    public string TransactionId {get;}
    public required string AccountNumber {get; init;}
    public required decimal Amount {get; init;}
    public required decimal BalanceAfter {get; init;}
    public required TransactionType TransactionType {get; init;}
    public DateTime CreatedAt {get;} = DateTime.UtcNow;

    public Transaction()
    {
        TransactionId = $"{DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}{AccountNumber}";
    }
}
