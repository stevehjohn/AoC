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
        
        return CountEnergised().ToString();
    }

    private int CountEnergised()
    {
        var count = 0;
        
        for (var y = 0; y < _height; y++)
        {
            for (var x = 0; x < _width; x++)
            {
                count += _energised[x, y] ? 1 : 0;
            }
        }

        return count;
    }

    private void Dump(int x, int y)
    {
        for (var yy = 0; yy < _height; yy++)
        {
            for (var xx = 0; xx < _width; xx++)
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

    private void SimulateBeams()
    {
        var beams = new Stack<(int X, int Y, char Direction)>();

        var visited = new HashSet<(int, int, char)>();
        
        beams.Push((-1, 0, 'E'));

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