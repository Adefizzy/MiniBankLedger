
using MiniBankLedger.Controllers;

Console.Clear();
bool shouldExit = false;


do
{

    string? mainmenu = MainMenuInput.MainMenu();

    if (int.TryParse(mainmenu, out int menuNumber))
    {
        if (menuNumber == 1)
        {
            CustomerController.CreateCustomer();
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