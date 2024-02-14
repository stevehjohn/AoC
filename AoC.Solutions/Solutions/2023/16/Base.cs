using AoC.Solutions.Extensions;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2023._16;

public abstract class Base : Solution
{
    public override string Description => "The floor will be lava";

    private char[,] _map;

    protected int Width;

    protected int Height;

    protected readonly IVisualiser<PuzzleState> Visualiser;

    private PuzzleState _puzzleState;

    protected Base()
    {
    }

    protected Base(IVisualiser<PuzzleState> visualiser)
    {
        Visualiser = visualiser;
    }

    private void Visualise((int X, int Y, char Direction, int Id, int SourceId)? beam = null, bool finished = false, int startX = 0, int startY = 0, char startDirection = '\0')
    {
        if (Visualiser != null)
        {
            if (_puzzleState == null)
            {
                _puzzleState = new PuzzleState
                {
                    Map = _map,
                    StartDirection = startDirection,
                    LaserX = startX,
                    LaserY = startY
                };
                
                Visualiser.PuzzleStateChanged(_puzzleState);
            }

            if (startDirection != '\0')
            {
                _puzzleState.LaserX = startX;

                _puzzleState.LaserY = startY;

                _puzzleState.StartDirection = startDirection;
            }

            if (beam != null)
            {
                _puzzleState.Beams.Add(beam.Value);
            }

            if (finished)
            {
                Visualiser.PuzzleStateChanged(_puzzleState);

                _puzzleState = null;
            }
        }
    }

    protected int CountEnergised(bool[,] energised)
    {
        var count = 0;
        
        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                count += energised[x, y] ? 1 : 0;
            }
        }

        return count;
    }

    protected bool[,] SimulateBeams(int startX, int startY, char direction)
    {
        Visualise(null, false, startX, startY, direction);
        
        var beams = new Stack<(int X, int Y, char Direction, int Id, int SourceId)>();

        var energised = new bool[Width, Height];

        var visited = new bool[Width, Height, 4];
        
        beams.Push((startX, startY, direction, 1, 0));

        var beamId = 1;
        
        while (beams.Count > 0)
        {
            var beam = beams.Pop();

            var i = beam.Direction switch
            {
                'N' => 0,
                'E' => 1,
                'S' => 2,
                _ => 3
            };

            if (beam.X >= 0 && beam.X < Width && beam.Y >= 0 && beam.Y < Height)
            {
                if (visited[beam.X, beam.Y, i])
                {
                    continue;
                }

                visited[beam.X, beam.Y, i] = true;
            }

            var (x, y) = MoveBeam(beam.X, beam.Y, beam.Direction);

            Visualise(beam);
            
            if (x == -1)
            {
                continue;
            }

            energised[x, y] = true;

            switch (_map[x, y])
            {
                case '.':
                    beams.Push((x, y, beam.Direction, beam.Id, beam.SourceId));
                    
                    continue;
                
                case '\\':
                    beams.Push((x, y, beam.Direction switch
                    {
                        'N' => 'W',
                        'E' => 'S',
                        'S' => 'E',
                        _ => 'N'
                    }, beam.Id, beam.SourceId));
                    
                    continue;
                
                case '/':
                    beams.Push((x, y, beam.Direction switch
                    {
                        'N' => 'E',
                        'E' => 'N',
                        'S' => 'W',
                        _ => 'S'
                    }, beam.Id, beam.SourceId));
                    
                    continue;
                
                case '|':
                    if (beam.Direction is 'N' or 'S')
                    {
                        beams.Push((x, y, beam.Direction, beam.Id, beam.SourceId));
                        
                        continue;
                    }
                    
                    beams.Push((x, y, 'N', ++beamId, beam.Id));
                    beams.Push((x, y, 'S', ++beamId, beam.Id));

                    continue;
                
                case '-':
                    if (beam.Direction is 'E' or 'W')
                    {
                        beams.Push((x, y, beam.Direction, beam.Id, beam.SourceId));
                        
                        continue;
                    }
                    
                    beams.Push((x, y, 'E', ++beamId, beam.Id));
                    beams.Push((x, y, 'W', ++beamId, beam.Id));

                    continue;
            }
        }
        
        Visualise(null, true);

        return energised;
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