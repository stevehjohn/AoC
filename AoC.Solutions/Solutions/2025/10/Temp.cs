using System.Text;

public record Machine2(string TargetLights, int[][] Buttons, int[] Joltages);

public static class Temp
{
    public static IEnumerable<Machine2> ParseInput(string path)
    {
        foreach (var line in File.ReadLines(path))
        {
            var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            var lightsToken  = parts[0];                 // e.g. "[.#..#...]"
            var buttonTokens = parts[1..^1];             // all middle tokens
            var joltageToken = parts[^1];                // last token

            var lights = lightsToken[1..^1];             // strip [ ]

            var buttons = buttonTokens
                .Select(t =>
                    t[1..^1]                              // strip [ ]
                        .Split(',', StringSplitOptions.RemoveEmptyEntries)
                        .Select(int.Parse)
                        .ToArray())
                .ToArray();

            var joltages = joltageToken[1..^1]
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            yield return new Machine2(lights, buttons, joltages);
        }
    }

    public static long SolvePart2(Machine2 machine)
    {
        var R = machine.Joltages.Length;
        var C = machine.Buttons.Length;

        var A = BuildMatrix(machine, R, C);

        RowReduce(A, R, C);

        return BackSubstitute(machine, R, C, A);
    }

    private static int[][] BuildMatrix(Machine2 machine, int R, int C)
    {
        // Augmented matrix A (R x (C + 1)): Ax = b
        var A = new int[R][];

        for (var r = 0; r < R; r++)
        {
            A[r] = new int[C + 1];
            A[r][C] = machine.Joltages[r];
        }

        foreach (var (buttonIndex, toggles) in machine.Buttons.Select((b, i) => (i, b)))
        {
            foreach (var toggle in toggles)
            {
                A[toggle][buttonIndex] = 1;
            }
        }

        return A;
    }

    private static void RowReduce(int[][] A, int R, int C)
    {
        // Integer Gaussian elimination, preserving your logic
        for (int r = 0; r < R && r < C; r++)
        {
            int pivot = -1;

            for (int p = r; p < R; p++)
            {
                if (A[p][r] != 0)
                {
                    pivot = p;
                }
            }

            if (pivot == -1)
            {
                // Column already all zeroes
                continue;
            }

            Swap(A, pivot, r);

            for (int p = r + 1; p < R; p++)
            {
                if (A[p][r] == 0)
                {
                    continue;
                }

                var deltaNom = A[p][r];
                var deltaDen = A[r][r];

                for (int c = 0; c < C; c++)
                {
                    A[p][c] = deltaDen * A[p][c] - A[r][c] * deltaNom;
                }

                A[p][C] = A[p][C] * deltaDen - A[r][C] * deltaNom;
            }
        }
    }

    private static int BackSubstitute(Machine2 machine, int R, int C, int[][] A)
    {
        // Pre-computed upper bounds for each button
        var maximums = machine.Buttons
            .Select(toggles => toggles.Min(idx => machine.Joltages[idx]))
            .ToArray();

        var best = int.MaxValue;

        var stack = new Stack<(int Row, int[] Pressed)>();
        stack.Push((R - 1, Enumerable.Repeat(-1, C).ToArray()));

        while (stack.Count > 0)
        {
            var (row, pressed) = stack.Pop();

            if (row < 0)
            {
                // All rows consistent → solution
                best = Math.Min(best, pressed.Sum());
                continue;
            }

            var rowTotal = A[row][C];

            // Try to fully resolve the row by substituting known variables
            for (int c = row; c < C; c++)
            {
                if (A[row][c] == 0)
                {
                    continue;
                }

                if (pressed[c] >= 0)
                {
                    rowTotal -= A[row][c] * pressed[c];
                }
                else
                {
                    // Still have an unknown in this row, need to brute force it
                    goto BruteForce;
                }
            }

            // All variables in this row known
            if (rowTotal == 0)
            {
                // Row is consistent; move to the next row up
                stack.Push((row - 1, pressed));
            }

            continue;

        BruteForce:
            // Find the first unknown variable in this row and branch on it
            for (int c = 0; c < C; c++)
            {
                if (A[row][c] != 0 && pressed[c] == -1)
                {
                    var max = maximums[c];

                    for (int p = max; p >= 0; p--)
                    {
                        var nextPressed = (int[])pressed.Clone();
                        nextPressed[c] = p;
                        stack.Push((row, nextPressed));
                    }

                    break;
                }
            }
        }

        Console.WriteLine($"Best for machine: {best}");
        return best;
    }

    private static void Swap<T>(T[] arr, int i0, int i1)
    {
        if (i0 == i1) return;

        var tmp = arr[i0];
        arr[i0] = arr[i1];
        arr[i1] = tmp;
    }
}
