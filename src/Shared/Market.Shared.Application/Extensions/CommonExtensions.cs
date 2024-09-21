namespace Market.Shared.Application.Extensions;

public static class CommonExtensions
{
    public static bool IsNull(this object obj) => obj == null;

    public static bool IsNotNull(this object obj) => obj != null;


    public static void IfNotNullSet<T>(this T obj, Action<T> action)
    {
        if (obj.IsNotNull())
        {
            action(obj);
        }
    }

}