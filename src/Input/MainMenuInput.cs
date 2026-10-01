
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
        Console.WriteLine("6. Deposit");
        Console.WriteLine("7. Withdraw");
        Console.WriteLine("8. View Account Balance");
        Console.WriteLine("9. View Account Transactions");
        Console.WriteLine("0. Exit");
        Console.Write("Select option: ");
        return Console.ReadLine();
    }
}