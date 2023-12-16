using AoC.Solutions.Extensions;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2023._16;

public abstract class Base : Solution
{
    public override string Description => "The floor will be lava";
    
    private char[,] _map;

    private bool[,] _energised;
    
    protected int Width;

    protected int Height;

    protected int CountEnergised()
    {
        var count = 0;
        
        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                count += _energised[x, y] ? 1 : 0;
            }
        }

        return count;
    }

    private void Dump(int x, int y)
    {
        for (var yy = 0; yy < Height; yy++)
        {
            for (var xx = 0; xx < Width; xx++)
            {
                if (xx == x && yy == y)
                {
                    Console.Write('O');
                    continue;
                }

                if (_energised[xx, yy])
                {
                    Console.Write('#');
                }
                else
                {
                    Console.Write(_map[xx, yy]);
                }
            }
                
            Console.WriteLine();
        }
            
        Console.WriteLine();
        Console.ReadKey();
    }

    protected void SimulateBeams(int startX, int startY, char direction)
    {
        var beams = new Stack<(int X, int Y, char Direction)>();

        _energised = new bool[Width, Height];

        var visited = new HashSet<(int, int, char)>();
        
        beams.Push((startX, startY, direction));

        while (beams.Count > 0)
        {
            var beam = beams.Pop();

            if (! visited.Add(beam))
            {
                continue;
            }

            // Dump(beam.X, beam.Y);

            var (x, y) = MoveBeam(beam.X, beam.Y, beam.Direction);

            if (x == -1)
            {
                continue;
            }

            _energised[x, y] = true;

            switch (_map[x, y])
            {
                case '.':
                    beams.Push((x, y, beam.Direction));
                    
                    continue;
                
                case '\\':
                    beams.Push((x, y, beam.Direction switch
                    {
                        'N' => 'W',
                        'E' => 'S',
                        'S' => 'E',
                        _ => 'N'
                    }));
                    
                    continue;
                
                case '/':
                    beams.Push((x, y, beam.Direction switch
                    {
                        'N' => 'E',
                        'E' => 'N',
                        'S' => 'W',
                        _ => 'S'
                    }));
                    
                    continue;
                
                case '|':
                    if (beam.Direction is 'N' or 'S')
                    {
                        beams.Push((x, y, beam.Direction));
                        
                        continue;
                    }
                    
                    beams.Push((x, y, 'N'));
                    beams.Push((x, y, 'S'));

                    continue;
                
                case '-':
                    if (beam.Direction is 'E' or 'W')
                    {
                        beams.Push((x, y, beam.Direction));
                        
                        continue;
                    }
                    
                    beams.Push((x, y, 'E'));
                    beams.Push((x, y, 'W'));

                    continue;
            }
        }
    }

    private (int X, int Y) MoveBeam(int x, int y, char direction)
    {
        switch (direction)
        {
            case 'E':
                if (x == Width - 1)
                {
                    return (-1, -1);
                }

                x++;
                break;

            case 'W':
                if (x == 0)
                {
                    return (-1, -1);
                }

                x--;
                break;

            case 'S':
                if (y == Height - 1)
                {
                    return (-1, -1);
                }

                y++;
                break;

            case 'N':
                if (y == 0)
                {
                    return (-1, -1);
                }

                y--;
                break;
        }

        return (x, y);
    }

    protected void ParseInput()
    {
        Width = Input[0].Length;

        Height = Input.Length;

        _map = Input.To2DArray();
    }
}