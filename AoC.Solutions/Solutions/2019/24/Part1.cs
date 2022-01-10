using AoC.Solutions.Common;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2019._24;

[UsedImplicitly]
public class Part1 : Base
{
    private readonly HashSet<int> _previousStates = new();

    private readonly bool[,] _grid = new bool[7, 7];

    public override string GetAnswer()
    {
        ParseInput();

        _previousStates.Add(GetBiodiversity());

        while (true)
        {
            PlayRound();

            var bioDiversity = GetBiodiversity();

            if (_previousStates.Contains(bioDiversity))
            {
                Dump();

                return bioDiversity.ToString();
            }

            _previousStates.Add(bioDiversity);
        }
    }

    private void Dump()
    {
        for (var y = 1; y < 6; y++)
        {
            for (var x = 1; x < 6; x++)
            {
                Console.Write(_grid[x, y] ? '#' : '.');
            }

            Console.WriteLine();
        }

        Console.WriteLine();
    }

    private void PlayRound()
    {
        var dies = new List<Point>();

        var infests = new List<Point>();

        for (var y = 1; y < 6; y++)
        {
            for (var x = 1; x < 6; x++)
            {
                var adjacent = AdjacentCount(x, y);

                if (_grid[x, y] && adjacent != 1)
                {
                    dies.Add(new Point(x, y));
                }

                if (! _grid[x, y] && (adjacent == 1 || adjacent == 2))
                {
                    infests.Add(new Point(x, y));
                }
            }
        }

        dies.ForEach(d => _grid[d.X, d.Y] = false);

        infests.ForEach(i => _grid[i.X, i.Y] = true);
    }

    private int GetBiodiversity()
    {
        var i = 1;

        var diversity = 0;

        for (var y = 1; y < 6; y++)
        {
            for (var x = 1; x < 6; x++)
            {
                if (_grid[x, y])
                {
                    diversity += i;
                }

                i *= 2;
            }
        }

        return diversity;
    }

    private int AdjacentCount(int x, int y)
    {
        var count = _grid[x - 1, y] ? 1 : 0;

        count += _grid[x, y - 1] ? 1 : 0;

        count += _grid[x + 1, y] ? 1 : 0;

        count += _grid[x, y + 1] ? 1 : 0;

        return count;
    }

    private void ParseInput()
    {
        var y = 1;
        foreach (var line in Input)
        {
            for (var x = 1; x < 6; x++)
            {
                _grid[x, y] = line[x - 1] == '#';
            }

            y++;
        }
    }
}