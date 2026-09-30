
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
        CustomerController customerController = new(new CustomerService(new InMemoryCustomerRepository()));
        AccountController accountController = new(new AccountService(new InMemoryAccountRepository(), new InMemoryCustomerRepository()));

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

        if (menuNumber == 0) // Exit
        {
            shouldExit = true;
        }
    }
} while (!shouldExit);