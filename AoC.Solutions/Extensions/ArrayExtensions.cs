namespace AoC.Solutions.Extensions;

public static class ArrayExtensions
{
    extension<T>(T[,] array)
    {
        public void ForAll(Action<int, int, T> action)
        {
            for (var y = 0; y <= array.GetUpperBound(1); y++)
            {
                for (var x = 0; x <= array.GetUpperBound(0); x++)
                {
                    action(x, y, array[x, y]);
                }
            }
        }
    }

    extension<T>(T[] array)
    {
        public IEnumerable<T[]> GetPermutations()
        {
            return GetPermutationsInternal(array.Length, array);
        }
    }

    private static IEnumerable<T[]> GetPermutationsInternal<T>(int remainingIterations, T[] array)
    {
        while (remainingIterations > 0)
        {
            if (remainingIterations == 1)
            {
                yield return (T[]) array.Clone();
            }

            for (var i = 0; i < remainingIterations - 1; i++)
            {
                var permutations = GetPermutationsInternal(remainingIterations - 1, array);

                foreach (var permutation in permutations)
                {
                    yield return permutation;
                }

                if (remainingIterations % 2 == 0)
                {
                    (array[i], array[remainingIterations - 1]) = (array[remainingIterations - 1], array[i]);
                }
                else
                {
                    (array[0], array[remainingIterations - 1]) = (array[remainingIterations - 1], array[0]);
                }
            }

            remainingIterations -= 1;
        }
    }
}