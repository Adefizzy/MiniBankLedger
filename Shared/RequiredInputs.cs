using System;

namespace MiniBankLedger.Shared;

public class RequiredInputs
{
    public static string IsRequired(Func<string?> func)
    {
        string? result;
        do
        {
            result = (func() ?? "").Trim();
        } while (result is null or "");

        return result;
    }
}
