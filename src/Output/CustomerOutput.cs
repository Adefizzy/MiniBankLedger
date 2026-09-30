using static System.Console;
using MiniBankLedger.Domain;
using MiniBankLedger.Domain.DTOs;

namespace MiniBankLedger.Output;

public static class CustomerOutput
{

    public static void PrintCustomer(CustomersResponse? customer)
    {
        WriteLine(customer?.ToString());
    }
}
