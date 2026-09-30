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
}
