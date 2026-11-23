using System;
using MiniBankLedger.Domain;


namespace MiniBankLedger.Repositories.Interface;

public interface ITransactionRepository
{
    public Transaction Save(Transaction transaction);

    public Transaction FindOneAndUpdate(string transactionId, Transaction transaction);

    public List<Transaction> FindByAccountNumber(string accountNumber);
}
