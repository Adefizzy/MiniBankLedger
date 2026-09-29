
using MiniBankLedger.Controllers;
using MiniBankLedger.Domain;
using MiniBankLedger.Input;
using MiniBankLedger.Output;
using MiniBankLedger.Services;

Console.Clear();
bool shouldExit = false;


do
{

    string? mainmenu = MainMenuInput.MainMenu();

    if (int.TryParse(mainmenu, out int menuNumber))
    {
        if (menuNumber == 1)
        {

            CustomerController customerController = new(new CustomerService());

            Customer? customer = customerController.CreateCustomer();
            if (customer is not null)
            {
                CustomerOutput.PrintCustomer(customer);
            }

        }

        if (menuNumber == 2)
        {

        }

        if (menuNumber == 3)
        {

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