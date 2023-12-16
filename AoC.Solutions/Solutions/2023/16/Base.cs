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

    private readonly IVisualiser<PuzzleState> _visualiser;

    private PuzzleState _puzzleState;

    protected Base()
    {
    }

    protected Base(IVisualiser<PuzzleState> visualiser)
    {
        _visualiser = visualiser;
    }

    protected void Visualise((int X, int Y, char Direction, int Id)? beam = null, bool finished = false)
    {
        if (_visualiser != null)
        {
            if (_puzzleState == null)
            {
                _puzzleState = new PuzzleState
                {
                    Map = _map
                };

                _visualiser.PuzzleStateChanged(_puzzleState);
            }

            if (beam != null)
            {
                _puzzleState.Beams.Add(beam.Value);
            }

            if (finished)
            {
                _visualiser.PuzzleStateChanged(_puzzleState);
            }
        }
    }

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

    protected void SimulateBeams(int startX, int startY, char direction)
    {
        var beams = new Stack<(int X, int Y, char Direction, int Id)>();

        _energised = new bool[Width, Height];

        var visited = new HashSet<(int, int, char)>();
        
        beams.Push((startX, startY, direction, 1));

        var beamId = 1;
        
        while (beams.Count > 0)
        {
            var beam = beams.Pop();

            if (! visited.Add((beam.X, beam.Y, beam.Direction)))
            {
                continue;
            }

            var (x, y) = MoveBeam(beam.X, beam.Y, beam.Direction);

            Visualise(beam);
            
            if (x == -1)
            {
                continue;
            }

            _energised[x, y] = true;

            switch (_map[x, y])
            {
                case '.':
                    beams.Push((x, y, beam.Direction, beam.Id));
                    
                    continue;
                
                case '\\':
                    beams.Push((x, y, beam.Direction switch
                    {
                        'N' => 'W',
                        'E' => 'S',
                        'S' => 'E',
                        _ => 'N'
                    }, beam.Id));
                    
                    continue;
                
                case '/':
                    beams.Push((x, y, beam.Direction switch
                    {
                        'N' => 'E',
                        'E' => 'N',
                        'S' => 'W',
                        _ => 'S'
                    }, beam.Id));
                    
                    continue;
                
                case '|':
                    if (beam.Direction is 'N' or 'S')
                    {
                        beams.Push((x, y, beam.Direction, beam.Id));
                        
                        continue;
                    }
                    
                    beams.Push((x, y, 'N', ++beamId));
                    beams.Push((x, y, 'S', ++beamId));

                    continue;
                
                case '-':
                    if (beam.Direction is 'E' or 'W')
                    {
                        beams.Push((x, y, beam.Direction, beam.Id));
                        
                        continue;
                    }
                    
                    beams.Push((x, y, 'E', ++beamId));
                    beams.Push((x, y, 'W', ++beamId));

                    continue;
            }
        }
        
        Visualise(null, true);
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