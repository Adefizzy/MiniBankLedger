
using MiniBankLedger.Controllers;
using MiniBankLedger.Repositories.In_Memory;
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

            customerController.CreateCustomer();
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