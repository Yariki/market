namespace Market.Shared.Common;

public static class CommonExtensions
{
    public static bool IsNull(this object? obj) => obj == null;

    public static bool IsNotNull(this object? obj) => obj != null;
}