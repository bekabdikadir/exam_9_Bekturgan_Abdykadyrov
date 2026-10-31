namespace EWallet.BLL.Services;

public static class WalletDefaults
{
    public const decimal InitialBalance = 100000m;
    public const decimal MaxAmount = 1_000_000_000m;

    public static bool IsAmountValid(decimal amount)
    {
        return amount > 0 && amount <= MaxAmount && decimal.Round(amount, 2) == amount;
    }
}
