using System;
using MiniBankLedger.Domain;
using MiniBankLedger.Repositories.Interface;

namespace MiniBankLedger.Repositories.In_Memory;

public class InMemoryAccountRepository : IAccountRepository
{
    private static List<Account> Accounts = [];
    public Account Save(Account account)
    {
        Accounts.Add(account);

        return account;
    }

    public List<Account> FindAccountByCustomerId(int customerId)
    {
        return [.. Accounts.Where(ac => ac.CustomerId == customerId)];
    }

    public List<Account> GetAllAccounts()
    {
        return Accounts;
    }

    public Account? GetAccountByAccountNumber(string accountNumber)
    {
     return Accounts.FirstOrDefault(acc => acc.AccountNumber == accountNumber);
    }


     public Account FindOneAndUpdate(string accountNumber, Account account)
    {
        IEnumerable<Account> updatedTransactionsQuery = from acct in Accounts
                                                        select acct.AccountNumber == accountNumber ? account : acct;


        List<Account> updatedAccounts = [];

        foreach(Account acct in updatedTransactionsQuery)
        {
            updatedAccounts.Add(acct);
        }

        Accounts = updatedAccounts;

        return account;                                             
    }
}
