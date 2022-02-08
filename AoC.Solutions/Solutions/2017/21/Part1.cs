using System.Text;
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

        RunCycle();

        return "TESTING";
    }

    private void RunCycle()
    {
        var pattern = _patterns[HashPattern(_state, 3)];
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

            var result = ParsePattern(parts[1], sideLength + 1);

            patterns.TryAdd(HashPattern(pattern, sideLength), result);

            pattern = Rotate(pattern, sideLength);

            patterns.TryAdd(HashPattern(pattern, sideLength), result);

            pattern = Rotate(pattern, sideLength);

            patterns.TryAdd(HashPattern(pattern, sideLength), result);

            pattern = Rotate(pattern, sideLength);

            patterns.TryAdd(HashPattern(pattern, sideLength), result);

            pattern = Rotate(pattern, sideLength);

            pattern = FlipVertical(pattern, sideLength);

            patterns.TryAdd(HashPattern(pattern, sideLength), result);
        
            pattern = FlipVertical(pattern, sideLength);
            
            pattern = FlipHorizontal(pattern, sideLength);

            patterns.TryAdd(HashPattern(pattern, sideLength), result);
        }

        return patterns;
    }
    
    public char[,] Rotate(char[,] pattern, int sideLength)
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

    public char[,] FlipVertical(char[,] pattern, int sideLength)
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

    public char[,] FlipHorizontal(char[,] pattern, int sideLength)
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

    //private static string PatternToString(char[,] pattern, int sideLength)
    //{
    //    var builder = new StringBuilder();

    //    for (var y = 0; y < sideLength; y++)
    //    {
    //        for (var x = 0; x < sideLength; x++)
    //        {
    //            builder.Append(pattern[x, y]);
    //        }
    //    }

    //    return builder.ToString();
    //}

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