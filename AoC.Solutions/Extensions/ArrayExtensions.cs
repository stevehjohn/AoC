namespace AoC.Extensions;

public static class ArrayExtensions
{
    public static List<T[]> GetPermutations<T>(this T[] array)
    {
        var permutations = new List<T[]>();

        GetPermutationsInternal(array.Length, array, permutations);

        return permutations;
    }

    private static void GetPermutationsInternal<T>(int remainingIterations, T[] array, List<T[]> permutations)
    {
        if (remainingIterations == 1)
        {
            permutations.Add((T[]) array.Clone());

            return;
        }

        for (var i = 0; i < remainingIterations - 1; i++)
        {
            GetPermutationsInternal(remainingIterations - 1, array, permutations);

            if (remainingIterations % 2 == 0)
            {
                (array[i], array[remainingIterations - 1]) = (array[remainingIterations - 1], array[i]);
            }
            else
            {
                (array[0], array[remainingIterations - 1]) = (array[remainingIterations - 1], array[0]);
            }
        }

        GetPermutationsInternal(remainingIterations - 1, array, permutations);
    }
}