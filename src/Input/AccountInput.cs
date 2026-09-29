using System;
using MiniBankLedger.Domain.DTOs;
using MiniBankLedger.Shared;
using static System.Console;


namespace MiniBankLedger.Input;

public class AccountInput
{
    public static CreateAccountRequest CreateAccount()
    {
        string accountType = RequiredInputs.IsRequired(() =>
          {
              Write("Account Type (Savings or Current): ");
              return ReadLine();
          });


          string customerId = RequiredInputs.IsRequired(() =>
          {
              Write("Customer id: ");
              return ReadLine();
          });

          return new CreateAccountRequest(AccountType: accountType, CustomerId: customerId);
    }


    public static string GetCustomerAccounts()
    {
        string customerId = RequiredInputs.IsRequired(() =>
          {
              Write("Enter customer Id: ");
              return ReadLine();
          });


          return customerId;
    }


}
