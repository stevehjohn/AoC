using AoC.Solutions.Extensions;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._16;

[UsedImplicitly]
public class Part1 : Base
{
    private char[,] _map;

    private bool[,] _energised;
    
    private int _width;

    private int _height;
    
    public override string GetAnswer()
    {
        ParseInput();

        SimulateBeams();
        
        return "0";
    }

    private void Dump()
    {
        for (var yy = 0; yy < _height; yy++)
        {
            for (var xx = 0; xx < _width; xx++)
            {
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
        // Console.ReadKey();
    }

    private void SimulateBeams()
    {
        var beams = new Queue<(int X, int Y, char Direction, int StartX, int StartY)>();
        
        beams.Enqueue((0, 0, 'E', -1, 0));

        while (beams.Count > 0)
        {
            var beam = beams.Dequeue();

            _energised[beam.X, beam.Y] = true;

            Dump();

            var (x, y) = MoveBeam(beam.X, beam.Y, beam.Direction);

            if (x == -1)
            {
                continue;
            }
            
            if (x == beam.StartX && y == beam.StartY)
            {
                continue;
            }

            switch (_map[x, y])
            {
                case '.':
                    beams.Enqueue((x, y, beam.Direction, beam.StartX, beam.StartY));
                    
                    continue;
                
                case '\\':
                    beams.Enqueue((x, y, beam.Direction switch
                    {
                        'N' => 'W',
                        'E' => 'S',
                        'S' => 'E',
                        _ => 'N'
                    }, beam.StartX, beam.StartY));
                    
                    continue;
                
                case '/':
                    beams.Enqueue((x, y, beam.Direction switch
                    {
                        'N' => 'E',
                        'E' => 'N',
                        'S' => 'W',
                        _ => 'S'
                    }, beam.StartX, beam.StartY));
                    
                    continue;
                
                case '|':
                    if (beam.Direction is 'N' or 'S')
                    {
                        beams.Enqueue((x, y, beam.Direction, beam.StartX, beam.StartY));
                        
                        continue;
                    }
                    
                    beams.Enqueue((x, y, 'N', x, y));
                    beams.Enqueue((x, y, 'S', x, y));

                    continue;
                
                case '-':
                    if (beam.Direction is 'E' or 'W')
                    {
                        beams.Enqueue((x, y, beam.Direction, beam.StartX, beam.StartY));
                        
                        continue;
                    }
                    
                    beams.Enqueue((x, y, 'E', x, y));
                    beams.Enqueue((x, y, 'W', x, y));

                    continue;
            }
        }
    }

    private (int X, int Y) MoveBeam(int x, int y, char direction)
    {
        switch (direction)
        {
            case 'E':
                if (x == _width - 1)
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
                if (y == _height - 1)
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

    private void ParseInput()
    {
        _width = Input[0].Length;

        _height = Input.Length;

        _map = Input.To2DArray();

        _energised = new bool[_width, _height];
    }
}