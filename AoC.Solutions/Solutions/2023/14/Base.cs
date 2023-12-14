using AoC.Solutions.Common;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2023._14;

public abstract class Base : Solution
{
    public override string Description => "Parabolic reflector dish";

    protected List<(Point Point, bool Round)> Rocks;
    
    protected int Rows;

    protected int Columns;
    
    protected int GetLoad()
    {
        var load = 0;
        
        foreach (var rock in Rocks)
        {
            if (rock.Round)
            {
                load += Rows - rock.Point.Y;
            }
        }

        return load;
    }

    protected void MoveRocks(
        Func<List<(Point Point, bool Round)>, List<(Point Point, bool Round)>> sort, 
        Func<Point, bool> shouldMove, 
        Func<Point, Point, bool> getBlocking, 
        Action<Point> open, 
        Action<Point, Point> blocked)
    {
        var rocks = sort(Rocks);
        
        foreach (var rock in rocks)
        {
            if (! rock.Round)
            {
                continue;
            }

            if (shouldMove(rock.Point))
            {
                var blocking = rocks.LastOrDefault(r => getBlocking(rock.Point, r.Point));

                if (blocking.Point == null)
                {
                    open(rock.Point);
                }
                else
                {
                    blocked(rock.Point, blocking.Point);
                }
            }
        }

        Rocks = rocks;

        // for (var y = 0; y < Input.Length; y++)
        // {
        //     for (var x = 0; x < Input[0].Length; x++)
        //     {
        //         if (! Rocks.Any(r => r.Point.X == x && r.Point.Y == y))
        //         {
        //             Console.Write('.');
        //         }
        //         else
        //         {
        //             Console.Write(Rocks.First(r => r.Point.X == x && r.Point.Y == y).Round ? 'O' : '#');
        //         }
        //     }
        //     
        //     Console.WriteLine();
        // }
        //
        // Console.WriteLine();
    }

    protected void ParseInput()
    {
        Rocks = new List<(Point Point, bool Round)>();

        Rows = Input.Length;

        Columns = Input[0].Length;
        
        for (var y = 0; y < Rows; y++)
        {
            for (var x = 0; x < Input[0].Length; x++)
            {
                var c = Input[y][x];
                
                if (c != '.')
                {
                    Rocks.Add((new Point(x, y), c == 'O'));
                }
            }
        }
    }
}