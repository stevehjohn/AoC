using AoC.Solutions.Common;
using AoC.Solutions.Extensions;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._22;

[UsedImplicitly]
public class Part1 : Base
{
    private List<(char C, List<Point> P)> _bricks = new();
    
    public override string GetAnswer()
    {
        ParseInput();
     
        Dump();
        
        SettleBricks();
        
        Dump();
        
        var result = CountSupportingBricks();
        
        return result.ToString();
    }

    private void Dump()
    {
        var output = new char[7, 10];

        foreach (var brick in _bricks)
        {
            foreach (var p in brick.P)
            {
                if (output[p.X, p.Z] == '\0' || output[p.X, p.Z] == brick.C)
                {
                    output[p.X, p.Z] = brick.C;
                }
                else
                {
                    output[p.X, p.Z] = '?';
                }

                if (output[p.Y + 4, p.Z] == '\0' || output[p.Y + 4, p.Z] == brick.C)
                {
                    output[p.Y + 4, p.Z] = brick.C;
                }
                else
                {
                    output[p.Y + 4, p.Z] = '?';
                }
            }
        }

        for (var z = 9; z > 0; z--)
        {
            for (var x = 0; x < 7; x++)
            {
                if (x == 3)
                {
                    Console.Write(' ');
                    
                    continue;
                }

                Console.Write(output[x, z] == '\0' ? '.' : output[x, z]);
            }
            
            Console.WriteLine();
        }
        
        Console.WriteLine();
    }

    private int CountSupportingBricks()
    {
        var count = 0;
        
        foreach (var brick in _bricks)
        {
            if (brick.P[0].Z == 1)
            {
                continue;
            }

            if (! RestedOn(brick.P))
            {
                count++;
            }
        }

        return count;
    }

    private bool RestedOn(List<Point> brick)
    {
        foreach (var item in _bricks)
        {
            if (item.P == brick)
            {
                continue;
            }

            foreach (var left in item.P)
            {
                foreach (var right in brick)
                {
                    if (left.Z == right.Z + 1 && left.X == right.X && left.Y == right.Y)
                    {
                        return false;
                    }
                }
            }
        }

        return true;
    }
    
    private void SettleBricks()
    {
        bool moved;

        do
        {
            moved = false;
                
            foreach (var brick in _bricks)
            {
                if (brick.P[0].Z == 1)
                {
                    continue;
                }

                if (! Resting(brick.P))
                {
                    foreach (var item in brick.P)
                    {
                        item.Z--;
                    }
                    
                    moved = true;
                }
            }
        } while (moved);
    }

    private bool Resting(List<Point> brick)
    {
        foreach (var item in _bricks)
        {
            if (item.P == brick)
            {
                continue;
            }

            foreach (var left in item.P)
            {
                foreach (var right in brick)
                {
                    if (left.Z == right.Z - 1 && left.X == right.X && left.Y == right.Y)
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }

    private void ParseInput()
    {
        var id = 'A';
        
        foreach (var line in Input)
        {
            var parts = line.Split('~');

            var start = Point.Parse(parts[0]);

            var end = Point.Parse(parts[1]);

            if (end.Z < start.Z)
            {
                (start, end) = (end, start);
            }

            var brick = new List<Point> { start };

            while (! start.Equals(end))
            {
                start = new Point(
                    start.X.Converge(end.X),
                    start.Y.Converge(end.Y),
                    start.Z.Converge(end.Z)
                );
                
                brick.Add(start);
            }
            
            _bricks.Add((id, brick));

            id++;
        }

        _bricks = _bricks.OrderBy(b => b.P[0].Z).ToList();
    }
}