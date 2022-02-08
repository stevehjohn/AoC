using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2017._21;

[UsedImplicitly]
public class Part1 : Base
{
    private Dictionary<int, char[,]> _patterns;

    private char[,] _state;

    public override string GetAnswer()
    {
        _patterns = ParseInput();

        _state = new[,] { { '.', '.', '#' }, { '#', '.', '#' }, { '.', '#', '#' } };

        Console.WriteLine("--------\n");

        Dump(_state);

        for (var i = 0; i < 2; i++)
        {
            RunCycle();

            Dump(_state);
        }

        return "TESTING";
    }

    private void RunCycle()
    {
        int size;

        int nextSize;

        char[,] newState;

        if (_state.GetLength(0) % 2 == 0)
        {
            size = 2;

            nextSize = 3;

            var newLength = _state.GetLength(0) / 2 * 3;

            newState = new char[newLength, newLength];
        }
        else
        {
            size = 3;

            nextSize = 2;

            var newLength = (_state.GetLength(0) / 3 + 1) * 2;

            newState = new char[newLength, newLength];
        }

        var tile = new char[size, size];

        for (var i = 0; i < _state.GetLength(0) / size; i += size)
        {
            for (var y = 0; y < size; y++)
            {
                for (var x = 0; x < size; x++)
                {
                    tile[x, y] = _state[i * size + x, i * size + y];
                }
            }

            var newTile = _patterns[HashPattern(tile, size)];

            for (var y = 0; y < size + 1; y++)
            {
                for (var x = 0; x < size + 1; x++)
                {
                    newState[i * (size + 1) + x, i * (size + 1) + y] = newTile[x, y];
                }
            }
        }

        _state = newState;
    }

    private Dictionary<int, char[,]> ParseInput()
    {
        var patterns = new Dictionary<int, char[,]>();

        foreach (var line in Input)
        {
            var parts = line.Split(" => ", StringSplitOptions.TrimEntries);

            var sideLength = 2;

            if (parts[0].Length == 11)
            {
                sideLength = 3;
            }

            var pattern = ParsePattern(parts[0], sideLength);

            Dump(pattern);

            var result = ParsePattern(parts[1], sideLength + 1);

            patterns.TryAdd(HashPattern(pattern, sideLength), result);

            pattern = Rotate(pattern, sideLength);

            Dump(pattern);

            patterns.TryAdd(HashPattern(pattern, sideLength), result);

            pattern = Rotate(pattern, sideLength);

            Dump(pattern);

            patterns.TryAdd(HashPattern(pattern, sideLength), result);

            pattern = Rotate(pattern, sideLength);

            Dump(pattern);

            patterns.TryAdd(HashPattern(pattern, sideLength), result);

            pattern = Rotate(pattern, sideLength);

            pattern = FlipVertical(pattern, sideLength);

            Dump(pattern);

            patterns.TryAdd(HashPattern(pattern, sideLength), result);
        
            pattern = FlipVertical(pattern, sideLength);
            
            pattern = FlipHorizontal(pattern, sideLength);

            Dump(pattern);

            patterns.TryAdd(HashPattern(pattern, sideLength), result);
        }

        return patterns;
    }

    private void Dump(char[,] tile)
    {
        for (var y = 0; y < tile.GetLength(0); y++)
        {
            for (var x = 0; x < tile.GetLength(1); x++)
            {
                Console.Write(tile[x, y]);
            }

            Console.WriteLine();
        }

        Console.WriteLine();
    }

    private char[,] Rotate(char[,] pattern, int sideLength)
    {
        var newPattern = new char[sideLength, sideLength];

        for (var y = 0; y < sideLength; y++)
        {
            for (var x = 0; x < sideLength; x++)
            {
                newPattern[x, y] = pattern[y, sideLength - 1 - x];
            }
        }

        return newPattern;
    }

    private char[,] FlipVertical(char[,] pattern, int sideLength)
    {
        var newPattern = new char[sideLength, sideLength];

        for (var y = 0; y < sideLength; y++)
        {
            for (var x = 0; x < sideLength; x++)
            {
                newPattern[x, y] = pattern[x, sideLength - 1 - y];
            }
        }

        return newPattern;
    }

    private char[,] FlipHorizontal(char[,] pattern, int sideLength)
    {
        var newPattern = new char[sideLength, sideLength];

        for (var y = 0; y < sideLength; y++)
        {
            for (var x = 0; x < sideLength; x++)
            {
                newPattern[x, y] = pattern[sideLength - 1 - x, y];
            }
        }

        return newPattern;
    }

    private static char[,] ParsePattern(string pattern, int sideLength)
    {
        var grid = new char[sideLength, sideLength];

        for (var y = 0; y < sideLength; y++)
        {
            for (var x = 0; x < sideLength; x++)
            {
                grid[x, y] = pattern[y * (sideLength + 1) + x];
            }
        }

        return grid;
    }

    private static int HashPattern(char[,] pattern, int sideLength)
    {
        var hash = 0;

        for (var y = 0; y < sideLength; y++)
        {
            for (var x = 0; x < sideLength; x++)
            {
                hash = HashCode.Combine(hash, pattern[x, y]);
            }
        }

        return hash;
    }
}