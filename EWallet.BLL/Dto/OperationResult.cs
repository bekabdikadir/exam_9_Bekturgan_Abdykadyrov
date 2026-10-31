namespace EWallet.BLL.Dto;

public class OperationResult
{
    public bool Succeeded { get; private init; }
    public string MessageKey { get; private init; } = string.Empty;
    public decimal? Balance { get; private init; }
    public object[] Args { get; private init; } = Array.Empty<object>();

    public static OperationResult Ok(string messageKey, decimal? balance = null, params object[] args)
    {
        return new OperationResult
        {
            Succeeded = true,
            MessageKey = messageKey,
            Balance = balance,
            Args = args
        };
    }

    public static OperationResult Fail(string messageKey)
    {
        return new OperationResult
        {
            Succeeded = false,
            MessageKey = messageKey
        };
    }
}
