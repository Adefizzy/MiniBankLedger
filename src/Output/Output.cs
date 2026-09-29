using static System.Console;

namespace MiniBankLedger.Output;

public class Output
{
     public static void Printer<T>(T value)
    {
        WriteLine(value?.ToString());
    }
}
