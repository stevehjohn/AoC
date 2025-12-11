namespace AoC.Solutions.Solutions._2025._10;

public static class MatrixSolver
{
    public static IEnumerable<Matrix> ParseInput(string[] input)
    {
        foreach (var line in input)
        {
            var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            var buttonTokens = parts[1..^1];

            var joltageToken = parts[^1];

            var buttons = buttonTokens
                .Select(t => t[1..^1]
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray())
                .ToArray();

            var joltages = joltageToken[1..^1]
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            yield return new Matrix(buttons, joltages);
        }
    }

    public static long Solve(Matrix machine)
    {
        var r = machine.Totals.Length;
        
        var c = machine.Values.Length;

        var a = BuildMatrix(machine, r, c);

        RowReduce(a, r, c);

        return BackSubstitute(machine, r, c, a);
    }

    private static int[][] BuildMatrix(Matrix machine, int y, int x)
    {
        var a = new int[y][];

        for (var r = 0; r < y; r++)
        {
            a[r] = new int[x + 1];
            
            a[r][x] = machine.Totals[r];
        }
        
        for (var buttonIndex = 0; buttonIndex < x; buttonIndex++)
        {
            var toggles = machine.Values[buttonIndex];

            for (var t = 0; t < toggles.Length; t++)
            {
                a[toggles[t]][buttonIndex] = 1;
            }
        }

        return a;
    }

    private static void RowReduce(int[][] a, int y, int x)
    {
        for (var r = 0; r < y && r < x; r++)
        {
            var pivot = -1;

            for (var p = r; p < y; p++)
            {
                if (a[p][r] != 0)
                {
                    pivot = p;
                }
            }

            if (pivot == -1)
            {
                continue;
            }

            Swap(a, pivot, r);

            for (var p = r + 1; p < y; p++)
            {
                if (a[p][r] == 0)
                {
                    continue;
                }

                var deltaNom = a[p][r];

                var deltaDen = a[r][r];

                for (var c = 0; c < x; c++)
                {
                    a[p][c] = deltaDen * a[p][c] - a[r][c] * deltaNom;
                }

                a[p][x] = a[p][x] * deltaDen - a[r][x] * deltaNom;
            }
        }
    }

    private static int BackSubstitute(Matrix machine, int y, int x, int[][] a)
    {
        var maximums = new int[x];

        for (var c = 0; c < x; c++)
        {
            var min = int.MaxValue;

            var toggles = machine.Values[c];

            for (var i = 0; i < toggles.Length; i++)
            {
                var idx = toggles[i];

                var val = machine.Totals[idx];

                if (val < min)
                {
                    min = val;
                }
            }

            maximums[c] = min;
        }

        var best = int.MaxValue;

        var pressed = new int[x];

        for (var i = 0; i < x; i++)
        {
            pressed[i] = -1;
        }

        DfsRow(y - 1, 0, x, a, maximums, pressed, ref best);

        return best;
    }

    private static void DfsRow(int row, int sum, int x, int[][] a, int[] maximums, int[] pressed, ref int best)
    {
        if (sum >= best)
        {
            return;
        }

        if (row < 0)
        {
            if (sum < best)
            {
                best = sum;
            }

            return;
        }

        var rowTotal = a[row][x];

        for (var c = 0; c < x; c++)
        {
            var coefficient = a[row][c];

            if (coefficient == 0)
            {
                continue;
            }

            var v = pressed[c];

            if (v >= 0)
            {
                rowTotal -= coefficient * v;
            }
            else
            {
                var bestC = -1;

                var bestMax = int.MaxValue;

                for (var y = 0; y < x; y++)
                {
                    if (a[row][y] != 0 && pressed[y] == -1)
                    {
                        var m = maximums[y];

                        if (m < bestMax)
                        {
                            bestMax = m;

                            bestC = y;
                        }
                    }
                }

                if (bestC == -1)
                {
                    if (rowTotal == 0)
                    {
                        DfsRow(row - 1, sum, x, a, maximums, pressed, ref best);
                    }

                    return;
                }

                for (var p = bestMax; p >= 0; p--)
                {
                    pressed[bestC] = p;

                    DfsRow(row, sum + p, x, a, maximums, pressed, ref best);
                }

                pressed[bestC] = -1;
                
                return;
            }
        }

        if (rowTotal == 0)
        {
            DfsRow(row - 1, sum, x, a, maximums, pressed, ref best);
        }
    }

    private static void Swap<T>(T[] arr, int i0, int i1)
    {
        if (i0 == i1)
        {
            return;
        }

        (arr[i0], arr[i1]) = (arr[i1], arr[i0]);
    }
}
