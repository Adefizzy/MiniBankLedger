using System;
using MiniBankLedger.Domain;
using MiniBankLedger.Domain.DTOs;
using MiniBankLedger.Domain.Enums;
using MiniBankLedger.Exceptions;
using MiniBankLedger.Repositories.Interface;
using MiniBankLedger.Shared;

namespace MiniBankLedger.Services;

public class AccountService(IAccountRepository accountRepository, ICustomerRepository customerRepository)
{
    public AccountResponse CreateAccount(CreateAccountRequest accountRequest)
    {

        if (!int.TryParse(accountRequest.CustomerId, out int customerId))
        {
            throw new InvalidEntryException("Customer id must be a number");
        }


        if (!Enum.TryParse<AccountType>(accountRequest.AccountType, true, out AccountType acType))
        {

            throw new InvalidEntryException("Account Type must Savings or Current");
        }

        Customer _ = customerRepository.FindById(customerId) ?? throw new NotFoundException($"Customer with the id {accountRequest.CustomerId} does not exist");


        Account account = new()
        {
            AccountNumber = Utils.GenerateAccountNumber(),
            AccountType = acType,
            CustomerId = customerId,
        };

        Account newAccount = accountRepository.Save(account);


        return new AccountResponse(
            AccountNumber: newAccount.AccountNumber,
            AccountType: newAccount.AccountType,
            Balance: newAccount.Balance,
            Status: newAccount.Status,
            CustomerId: newAccount.CustomerId);
    }


    public List<AccountResponse> GetCustomerAccounts(string customerIdVal)
    {
        if (!int.TryParse(customerIdVal, out int customerId))
        {
            throw new InvalidEntryException("Customer id must be a number");
        }


        Customer _ = customerRepository.FindById(customerId) ?? throw new NotFoundException($"Customer with the id {customerIdVal} does not exist");

        List<Account> accounts = accountRepository.FindAccountByCustomerId(customerId);

        return [.. accounts.Select(acc => new AccountResponse(
            AccountNumber: acc.AccountNumber,
            AccountType: acc.AccountType,
            Balance: acc.Balance,
            Status: acc.Status,
            CustomerId: acc.CustomerId
        ))];
    }




}
