using System;
using MiniBankLedger.Domain;
using MiniBankLedger.Domain.DTOs;
using MiniBankLedger.Domain.Enums;
using MiniBankLedger.Exceptions;
using MiniBankLedger.Repositories.Interface;
using MiniBankLedger.Shared;

namespace MiniBankLedger.Services;

public class AccountService(
    IAccountRepository accountRepository,
    ICustomerRepository customerRepository,
    ITransactionRepository transactionRepository
    )
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
            AccountNumber: newAccount.AccountNumber.ToString(),
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
            AccountNumber: acc.AccountNumber.ToString(),
            AccountType: acc.AccountType,
            Balance: acc.Balance,
            Status: acc.Status,
            CustomerId: acc.CustomerId
        ))];
    }


    public AccountResponse Deposit(DepositRequest depositRequest)
    {
        if (!ulong.TryParse(depositRequest.AccountNumber, out ulong accountNumber) || accountNumber.ToString().Length < 10)
        {
            throw new InvalidEntryException($"Account number{accountNumber} must be a valid ten digit number");
        }

        Account? account = accountRepository.GetAccountByAccountNumber(accountNumber);

        if (!decimal.TryParse(depositRequest.Amount, out decimal amount) || amount <= 0)
        {
            throw new InvalidEntryException("Amount must be greater than zero");
        }

        if (account is null)
        {
            throw new InvalidEntryException($"Account with the account number {accountNumber} does not exist");
        }

        account.Balance += amount;
        accountRepository.FindOneAndUpdate(accountNumber, account);

        Transaction transaction = new()
        {
            AccountNumber = accountNumber,
            TransactionId = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
            Amount = amount,
            BalanceAfter = account.Balance,
            TransactionType = TransactionType.Deposit
        };
        transactionRepository.Save(transaction);


        return new AccountResponse(
            AccountNumber: account.AccountNumber.ToString(),
            AccountType: account.AccountType,
            Balance: account.Balance,
            Status: account.Status,
            CustomerId: account.CustomerId);
    }


    public AccountResponse Withdraw(WithdrawalRequest withdrawalRequest)
    {
        if (!ulong.TryParse(withdrawalRequest.AccountNumber, out ulong accountNumber) || accountNumber.ToString().Length < 10)
        {
            throw new InvalidEntryException($"Account number{accountNumber} must be a valid ten digit number");
        }

        Account? account = accountRepository.GetAccountByAccountNumber(accountNumber);

        if (!decimal.TryParse(withdrawalRequest.Amount, out decimal amount) || amount <= 0)
        {
            throw new InvalidEntryException("Amount must be greater than zero");
        }



        if (account is null)
        {
            throw new InvalidEntryException($"Account with the account number {accountNumber} does not exist");
        }

        if (amount > account.Balance)
        {
            throw new InsufficientFundsException($"You don't have enough balance in your account");
        }

        account.Balance -= amount;
        accountRepository.FindOneAndUpdate(accountNumber, account);

        Transaction transaction = new()
        {
            AccountNumber = accountNumber,
            TransactionId = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
            Amount = amount,
            BalanceAfter = account.Balance,
            TransactionType = TransactionType.Withdrawal
        };
        transactionRepository.Save(transaction);

        return new AccountResponse(
            AccountNumber: account.AccountNumber.ToString(),
            AccountType: account.AccountType,
            Balance: account.Balance,
            Status: account.Status,
            CustomerId: account.CustomerId);
    }


    public string ViewBalance(string AccountNumber)
    {
        if (!ulong.TryParse(AccountNumber, out ulong accountNumber) || accountNumber.ToString().Length < 10)
        {
            throw new InvalidEntryException($"Account number{accountNumber} must be a valid ten digit number");
        }

        Account? account = accountRepository.GetAccountByAccountNumber(accountNumber);

        if (account is null)
        {
            throw new InvalidEntryException($"Account with the account number {accountNumber} does not exist");
        }

        return $"Current Account Balance is {account.Balance:C}";
    }

    public List<TransactionResponse> ViewTransactions(string AccountNumber)
    {
        if (!ulong.TryParse(AccountNumber, out ulong accountNumber) || accountNumber.ToString().Length < 10)
        {
            throw new InvalidEntryException($"Account number{accountNumber} must be a valid ten digit number");
        }

        if (accountRepository.GetAccountByAccountNumber(accountNumber) is null)
        {
            throw new InvalidEntryException($"Account with the account number {accountNumber} does not exist");
        }

        return [.. transactionRepository.FindByAccountNumber(accountNumber)
                    .Select(tranc => new TransactionResponse(
                            TransactionId: tranc.TransactionId,
                            TransactionType: tranc.TransactionType,
                            Amount: tranc.Amount,
                            Balance: tranc.BalanceAfter
                            ))];

    }


}
