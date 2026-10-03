using System;
using MiniBankLedger.Domain.DTOs;
using MiniBankLedger.Input;
using MiniBankLedger.Services;

namespace MiniBankLedger.Controllers;

public class AccountController(AccountService accountService)
{
    public AccountResponse CreateAccount()
    {
        CreateAccountRequest createAccountRequest = AccountInput.CreateAccount();

        return accountService.CreateAccount(createAccountRequest);
    }

    public List<AccountResponse> GetCustomerAccounts()
    {
        string customerId = AccountInput.GetCustomerAccounts();

        return accountService.GetCustomerAccounts(customerId);
    }

    public AccountResponse Deposit()
    {
        DepositRequest depositRequest = AccountInput.GetDepositParam();

        return accountService.Deposit(depositRequest);
    }

    public AccountResponse Withdraw()
    {
        WithdrawalRequest withdrawalRequest = AccountInput.GetWithdrawParam();

        return accountService.Withdraw(withdrawalRequest);
    }

    public decimal ViewAccountBalance()
    {
        string accountNumber = AccountInput.ViewAccountBalance();


        return accountService.ViewBalance(accountNumber);
    }

    public List<TransactionResponse> ViewTransactions()
    {
        string accountNumber = AccountInput.ViewAccountBalance();

        return accountService.ViewTransactions(accountNumber);
    }
}
