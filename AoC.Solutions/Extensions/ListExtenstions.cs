namespace AoC.Solutions.Extensions;

public static class ListExtensions
{
    public static void ForAll<T>(this List<T> list, Action<int, T> action)
    {
        for (var i = 0; i < list.Count; i++)
        {
            action(i, list[i]);
        }
    }
}