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


    public static DepositRequest GetDepositParam()
    {
        string accountNumber = RequiredInputs.IsRequired(() =>
         {
             Write("Enter account number: ");
             return ReadLine();
         });


        string amount = RequiredInputs.IsRequired(() =>
        {
            Write("Enter amount: ");
            return ReadLine();
        });

        return new DepositRequest(AccountNumber: accountNumber, Amount: amount);
    }


    public static WithdrawalRequest GetWithdrawParam()
    {
        string accountNumber = RequiredInputs.IsRequired(() =>
         {
             Write("Enter account number: ");
             return ReadLine();
         });


        string amount = RequiredInputs.IsRequired(() =>
        {
            Write("Enter amount: ");
            return ReadLine();
        });

        return new WithdrawalRequest(AccountNumber: accountNumber, Amount: amount);
    }

    public static string ViewAccountBalance()
    {
         return RequiredInputs.IsRequired(() =>
         {
             Write("Enter account number: ");
             return ReadLine();
         });
    }

}
