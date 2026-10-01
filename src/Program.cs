
using MiniBankLedger.Controllers;
using MiniBankLedger.Domain;
using MiniBankLedger.Domain.DTOs;
using MiniBankLedger.Input;
using MiniBankLedger.Output;
using MiniBankLedger.Repositories.In_Memory;
using MiniBankLedger.Services;

Console.Clear();
bool shouldExit = false;


do
{

    string? mainmenu = MainMenuInput.MainMenu();

    if (int.TryParse(mainmenu, out int menuNumber))
    {
        InMemoryCustomerRepository inMemoryCustomerRepository = new();
        InMemoryAccountRepository inMemoryAccountRepository = new();
        InMemoryTransactionRepository inMemoryTransactionRepository = new();

        if (menuNumber == 1) // create customer
        {
            CustomerController customerController = new(new CustomerService(inMemoryCustomerRepository));
            CustomersResponse? customer = customerController.CreateCustomer();
            Output.Printer(customer);
        }

        if (menuNumber == 2) // list customers
        {
            CustomerController customerController = new(new CustomerService(inMemoryCustomerRepository));
            CustomersResponse[] allCustomers = customerController.AllCustomers();

            foreach (CustomersResponse cus in allCustomers)
            {
                Output.Printer(cus);
            }
        }

        if (menuNumber == 3) // search customer
        {
            CustomerController customerController = new(new CustomerService(inMemoryCustomerRepository));
            CustomersResponse? customer = customerController.SearchCustomer();
            Output.Printer(customer);
        }

        if (menuNumber == 4) // open account
        {
            AccountController accountController = new(
                new AccountService(
                    inMemoryAccountRepository,
                    inMemoryCustomerRepository,
                    inMemoryTransactionRepository
                    ));
            AccountResponse accountResponse = accountController.CreateAccount();
            Output.Printer(accountResponse);
        }

        if (menuNumber == 5) // view customer account
        {
            AccountController accountController = new(
                new AccountService(
                    inMemoryAccountRepository,
                    inMemoryCustomerRepository,
                    inMemoryTransactionRepository
                    ));
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
                AccountController accountController = new(
               new AccountService(
                   inMemoryAccountRepository,
                   inMemoryCustomerRepository,
                   inMemoryTransactionRepository
                   ));

                AccountResponse accountResponse = accountController.Deposit();

                Output.Printer(accountResponse);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        if (menuNumber == 7)
        {
            try
            {
                AccountController accountController = new(
                              new AccountService(
                                  inMemoryAccountRepository,
                                  inMemoryCustomerRepository,
                                  inMemoryTransactionRepository
                                  ));

                AccountResponse accountResponse = accountController.Withdraw();

                Output.Printer(accountResponse);
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