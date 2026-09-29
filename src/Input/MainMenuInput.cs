
namespace MiniBankLedger.Input;
static class MainMenuInput
{
    public static string? MainMenu()
    {
        Console.WriteLine("=== Mini Banking Ledger === \n\n");
        Console.WriteLine("1. Create Customer");
        Console.WriteLine("2. List Customers");
        Console.WriteLine("3. Search Customer");
        Console.WriteLine("4. Open Account");
        Console.WriteLine("5. View Customer Account");
        Console.WriteLine("0. Exit");
        Console.WriteLine();
        Console.WriteLine();
        Console.Write("Select option: ");

        return Console.ReadLine();
    }
}