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
        var (accountNumber, Amount) = depositRequest;

        if (accountNumber.Length is < 10 or >= 11)
        {
            throw new InvalidEntryException($"Account number{accountNumber} must be a valid ten digit number");
        }

        Account? account = accountRepository.GetAccountByAccountNumber(accountNumber);

        if (!decimal.TryParse(Amount, out decimal amount) || amount <= 0)
        {
            throw new InvalidEntryException("Amount must be greater than zero");
        }

        if (account is null)
        {
            throw new NotFoundException($"Account with the account number {accountNumber} does not exist");
        }

        account.Balance += amount;
        accountRepository.FindOneAndUpdate(accountNumber, account);

        CreateTransaction(account, amount);


        return new AccountResponse(
            AccountNumber: account.AccountNumber.ToString(),
            AccountType: account.AccountType,
            Balance: account.Balance,
            Status: account.Status,
            CustomerId: account.CustomerId);
    }


    public AccountResponse Withdraw(WithdrawalRequest withdrawalRequest)
    {
        var (accountNumber, Amount) = withdrawalRequest;


        if (accountNumber.Length is < 10 or >= 11)
        {
            throw new InvalidEntryException($"Account number{accountNumber} must be a valid ten digit number");
        }

        Account? account = accountRepository.GetAccountByAccountNumber(accountNumber);

        if (!decimal.TryParse(Amount, out decimal amount) || amount <= 0)
        {
            throw new InvalidEntryException("Amount must be greater than zero");
        }



        if (account is null)
        {
            throw new NotFoundException($"Account with the account number {accountNumber} does not exist");
        }

        if (amount > account.Balance)
        {
            throw new InsufficientFundsException($"You don't have enough balance in your account");
        }

        account.Balance -= amount;
        accountRepository.FindOneAndUpdate(accountNumber, account);

        CreateTransaction(account, amount);

        return new AccountResponse(
            AccountNumber: account.AccountNumber.ToString(),
            AccountType: account.AccountType,
            Balance: account.Balance,
            Status: account.Status,
            CustomerId: account.CustomerId);
    }


    public decimal ViewBalance(string accountNumber)
    {
        if (accountNumber.Length is < 10 or >= 11)
        {
            throw new InvalidEntryException($"Account number{accountNumber} must be a valid ten digit number");
        }

        Account? account = accountRepository.GetAccountByAccountNumber(accountNumber);

        if (account is null)
        {
            throw new InvalidEntryException($"Account with the account number {accountNumber} does not exist");
        }

        return account.Balance;
    }

    public List<TransactionResponse> ViewTransactions(string accountNumber)
    {
        if (accountNumber.Length is < 10 or >= 11)
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


    public TransferResponse Transfer(TransferRequest transferRequest)
    {
        var (source, destination, rawAmount) = transferRequest;

        if (source.Length is < 10 or >= 11)
        {
            throw new InvalidEntryException($"Source account {source} must be a valid 10 digit number");
        }

        if (destination.Length is < 10 or >= 11)
        {
            throw new InvalidEntryException($"Destination account {destination} must be a valid 10 digit number");
        }


        Account? sourceAccount = accountRepository.GetAccountByAccountNumber(source);
        Account? destinationAccount = accountRepository.GetAccountByAccountNumber(destination);

        if (sourceAccount is null)
        {
            throw new NotFoundException($"source account with account number {source} does not exist");
        }

        if (destinationAccount is null)
        {
            throw new NotFoundException($"destination account with the account number {destination} does not exist");
        }

        if (!decimal.TryParse(rawAmount, out decimal amount) || amount <= 0)
        {
            throw new InvalidEntryException("Amount must be greater than zero");
        }


        if (amount > sourceAccount.Balance)
        {
            throw new InsufficientFundsException($"Source account does not have enough balance for this transaction");
        }

        sourceAccount.Balance -= amount;
        destinationAccount.Balance += amount;

        CreateTransaction(sourceAccount, amount);

        CreateTransaction(destinationAccount, amount);

        return new TransferResponse(Source: sourceAccount.AccountNumber, Destination: destinationAccount.AccountNumber, Amount: amount);
    }


    private Transaction CreateTransaction(Account account, decimal amount)
    {
        Transaction transaction = new()
        {
            AccountNumber = account.AccountNumber,
            TransactionId = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
            Amount = amount,
            BalanceAfter = account.Balance,
            TransactionType = TransactionType.Transfer_in
        };

        transactionRepository.Save(transaction);

        return transaction;
    }
}
