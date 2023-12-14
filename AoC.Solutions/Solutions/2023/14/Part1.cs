using AoC.Solutions.Common;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._14;

[UsedImplicitly]
public class Part1 : Base
{
    private List<(Point Point, bool Round)> _rocks;

    private int _rows;
    
    public override string GetAnswer()
    {
        ParseInput();

        MoveRocks();

        var result = GetLoad();
        
        // for (var y = 0; y < Input.Length; y++)
        // {
        //     for (var x = 0; x < Input[0].Length; x++)
        //     {
        //         if (! _rocks.Any(r => r.Point.X == x && r.Point.Y == y))
        //         {
        //             Console.Write('.');
        //         }
        //         else
        //         {
        //             Console.Write(_rocks.First(r => r.Point.X == x && r.Point.Y == y).Round ? 'O' : '#');
        //         }
        //     }
        //     
        //     Console.WriteLine();
        // }

        return result.ToString();
    }

    private int GetLoad()
    {
        var load = 0;
        
        foreach (var rock in _rocks)
        {
            if (rock.Round)
            {
                load += _rows - rock.Point.Y;
            }
        }

        return load;
    }

    private void MoveRocks()
    {
        foreach (var rock in _rocks)
        {
            if (! rock.Round)
            {
                continue;
            }

            if (rock.Point.Y > 0)
            {
                var above = _rocks.LastOrDefault(r => r.Point.X == rock.Point.X && r.Point.Y < rock.Point.Y);

                if (above.Point == null)
                {
                    rock.Point.Y = 0;
                }
                else
                {
                    rock.Point.Y = above.Point.Y + 1;
                }
            }
        }
    }

    private void ParseInput()
    {
        _rocks = new List<(Point Point, bool Round)>();

        _rows = Input.Length;
        
        for (var y = 0; y < _rows; y++)
        {
            for (var x = 0; x < Input[0].Length; x++)
            {
                var c = Input[y][x];
                
                if (c != '.')
                {
                    _rocks.Add((new Point(x, y), c == 'O'));
                }
            }
        }
    }
}