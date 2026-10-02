using System;

namespace MiniBankLedger.Shared;

public static class Utils
{
    public static string GenerateAccountNumber()
    {
        var rand = new Random();

        return string.Concat(Enumerable.Range(0, 10).Select(_ => rand.Next(0, 10)));
    }
}
