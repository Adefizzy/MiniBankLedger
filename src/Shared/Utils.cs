using System;

namespace MiniBankLedger.Shared;

public static class Utils
{
    public static ulong GenerateAccountNumber()
    {
        var rand = new Random();

        return ulong.Parse(string.Concat(Enumerable.Range(0, 10).Select(_ => rand.Next(0, 10))));
    }
}
