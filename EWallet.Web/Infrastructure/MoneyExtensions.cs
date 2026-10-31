using System.Globalization;

namespace EWallet.Web.Infrastructure;

public static class MoneyExtensions
{
    public static string ToMoney(this decimal value)
    {
        return value.ToString("N2", CultureInfo.CurrentUICulture);
    }
}
