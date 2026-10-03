
using MiniBankLedger.Controllers;
using MiniBankLedger.Domain;
using MiniBankLedger.Domain.DTOs;
using MiniBankLedger.Input;
using MiniBankLedger.Output;
using MiniBankLedger.Repositories.In_Memory;
using MiniBankLedger.Services;

Console.Clear();
bool shouldExit = false;

InMemoryCustomerRepository inMemoryCustomerRepository = new();
InMemoryAccountRepository inMemoryAccountRepository = new();
InMemoryTransactionRepository inMemoryTransactionRepository = new();
CustomerController customerController = new(new CustomerService(inMemoryCustomerRepository));
AccountController accountController = new(
               new AccountService(
                   inMemoryAccountRepository,
                   inMemoryCustomerRepository,
                   inMemoryTransactionRepository
                   ));

do
{

    string? mainmenu = MainMenuInput.MainMenu();

    if (int.TryParse(mainmenu, out int menuNumber))
    {
        if (menuNumber == 1) // create customer
        {
            CustomersResponse? customer = customerController.CreateCustomer();
            Output.Printer(customer);
        }

        if (menuNumber == 2) // list customers
        {
            CustomersResponse[] allCustomers = customerController.AllCustomers();

            foreach (CustomersResponse cus in allCustomers)
            {
                Output.Printer(cus);
            }
        }

        if (menuNumber == 3) // search customer
        {
            CustomersResponse? customer = customerController.SearchCustomer();
            Output.Printer(customer);
        }

        if (menuNumber == 4) // open account
        {

            AccountResponse accountResponse = accountController.CreateAccount();
            Output.Printer(accountResponse);
        }

        if (menuNumber == 5) // view customer account
        {
            List<AccountResponse> accountResponses = accountController.GetCustomerAccounts();

            foreach (AccountResponse acc in accountResponses)
            {
                Output.Printer(acc);
            }
        }

        if (menuNumber == 6) // Deposit
        {
            try
            {
                AccountResponse accountResponse = accountController.Deposit();
                Output.Printer(accountResponse);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        if (menuNumber == 7) // Withdrawal
        {
            try
            {
                AccountResponse accountResponse = accountController.Withdraw();
                Output.Printer(accountResponse);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        if (menuNumber == 8)
        {
            try
            {
                decimal accountBalance = accountController.ViewAccountBalance();
                Console.WriteLine($"Current Account Balance is {accountBalance:C}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        if (menuNumber == 9)
        {
            try
            {
                List<TransactionResponse> transactionResponses = accountController.ViewTransactions();
                Output.Printer(transactionResponses);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        if (menuNumber == 0) // Exit
        {
            shouldExit = true;
        }
    }
} while (!shouldExit);