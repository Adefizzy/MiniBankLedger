using static System.Console;
using MiniBankLedger.Domain;

namespace MiniBankLedger.Output;

public static class CustomerOutput
{

    public static void PrintCustomer(Customer? customer)
    {
        WriteLine("Customer Created");
        WriteLine(customer?.ToString());
    }
}
