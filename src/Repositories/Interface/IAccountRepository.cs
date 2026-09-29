using System;
using MiniBankLedger.Domain;

namespace MiniBankLedger.Repositories.Interface;

public interface IAccountRepository
{
    public Account Save(Account account);

    public List<Account> FindAccountByCustomerId(int customerId);

    public List<Account> GetAllAccounts();
}
