using System;
using MiniBankLedger.Domain;
using MiniBankLedger.Repositories.Interface;

namespace MiniBankLedger.Repositories.In_Memory;

public class InMemoryAccountRepository : IAccountRepository
{
    public static readonly List<Account> Accounts = [];
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
}
