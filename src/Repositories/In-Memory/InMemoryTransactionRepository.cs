using System;
using MiniBankLedger.Domain;
using MiniBankLedger.Repositories.Interface;

namespace MiniBankLedger.Repositories.In_Memory;

public class InMemoryTransactionRepository : ITransactionRepository
{
    private static List<Transaction> Transactions = [];
    public Transaction Save(Transaction transaction)
    {
        Transactions.Add(transaction);

        return transaction;
    }

    public Transaction FindOneAndUpdate(long transactionId, Transaction transaction)
    {
        IEnumerable<Transaction> updatedTransactionsQuery = from tranc in Transactions
                                                            select tranc.TransactionId == transactionId ? transaction : tranc;


        List<Transaction> updatedTransactions = [];

        foreach (Transaction tranc in updatedTransactionsQuery)
        {
            updatedTransactions.Add(tranc);
        }

        Transactions = updatedTransactions;

        return transaction;
    }

    public List<Transaction> FindByAccountNumber(ulong accointNumber)
    {
        IEnumerable<Transaction> updatedTransactionsQuery = from tranc in Transactions
                                                            where tranc.AccountNumber == accointNumber
                                                            select tranc;

        List<Transaction> transactions = [];

        foreach (Transaction tranc in updatedTransactionsQuery)
        {
            transactions.Add(tranc);
        }


        return transactions;

    }
}
