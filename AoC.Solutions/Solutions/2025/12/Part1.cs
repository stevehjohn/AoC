using AoC.Solutions.Infrastructure;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2025._12;

[UsedImplicitly]
public class Part1 : Base
{
    private readonly IVisualiser<PuzzleState> _visualiser;

    private readonly PuzzleState _puzzleState;
    
    public Part1() { }

    public Part1(IVisualiser<PuzzleState> visualiser)
    {
        _visualiser = visualiser;

        _puzzleState = new PuzzleState();
    }

    public override string GetAnswer()
    {
        var areas = new Dictionary<int, int>();

        var i = 0;
        
        for (;; i += 5)
        {
            if (Input[i][1] != ':')
            {
                break;
            }

            var area = 0;
            
            for (var y = 1; y < 4; y++)
            {
                for (var x = 0; x < 3; x++)
                {
                    area += Input[i + y][x] == '#' ? 1 : 0;
                }
            }

            if (_visualiser != null)
            {
                var tile = new int[3];
                
                for (var y = 1; y < 4; y++)
                {
                    var row = 0;
                    
                    for (var x = 0; x < 3; x++)
                    {
                        row |= (Input[i + y][x] == '#' ? 1 : 0) << x;
                    }

                    tile[y - 1] = row;
                }
                
                _puzzleState.Tiles.Add(tile);
            }

            areas.Add(i / 5, area);
        }

        var result = 0;

        for (; i < Input.Length; i++)
        {
            var line = Input[i];

            var area = int.Parse(line[..2]) * int.Parse(line[3..5]);

            var presentCounts = line[7..].Split(' ');

            var used = 0;

            for (var x = 0; x < presentCounts.Length; x++)
            {
                var required = int.Parse(presentCounts[x]);

                used += required * areas[x];
            }

            if (used < area)
            {
                result++;
            }

            if (_visualiser != null)
            {
                _puzzleState.Areas.Add(new Area(int.Parse(line[..2]), int.Parse(line[3..5]), presentCounts.Select(int.Parse).ToArray(), used < area));
            }
        }

        if (_visualiser != null)
        {
            _visualiser.PuzzleStateChanged(_puzzleState);
        }

        return result.ToString();
    }
}
