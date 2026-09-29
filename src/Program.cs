
using MiniBankLedger.Controllers;
using MiniBankLedger.Domain;
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

        if (menuNumber == 1) // create customer
        {
            Customer? customer = customerController.CreateCustomer();
            CustomerOutput.PrintCustomer(customer);
        }

        if (menuNumber == 2) // list customers
        {

        }

        if (menuNumber == 3) // search customer
        {
            Customer? customer = customerController.SearchCustomer();
            CustomerOutput.PrintCustomer(customer);
        }

        if (menuNumber == 4)
        {

        }

        if (menuNumber == 5)
        {

        }

        if (menuNumber == 0)
        {

        }
    }
} while (!shouldExit);