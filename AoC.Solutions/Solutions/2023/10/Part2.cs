using AoC.Solutions.Infrastructure;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._10;

[UsedImplicitly]
public class Part2 : Base
{
    private int _width;

    private int _height;

    private readonly IVisualiser<PuzzleState> _visualiser;

    private PuzzleState _puzzleState;
    
    public Part2()
    {
    }

    public Part2(IVisualiser<PuzzleState> visualiser)
    {
        _visualiser = visualiser;
    }
    
    public override string GetAnswer()
    {
        var (x, y) = ParseInput();

        InitialiseVisualisation();

        WalkPipes(x, y);
        
        UpdateMap();

        RemoveJunk();
        
        UpdateMap();

        if (_visualiser == null)
        {
            Parallel.Invoke(
                () => FloodFill(0, 0),
                () => FloodFill(_width - 1, _height - 1),
                () => FloodFill(0, _height - 1),
                () => FloodFill(_width - 1, 0)
            );
        }
        else
        {
            FloodFill(0, 0);
        }

        UpdateMap();

        return CountEnclosed().ToString();
    }

    private void InitialiseVisualisation()
    {
        if (_visualiser != null)
        {
            UpdateMap();
            
            _visualiser.PuzzleStateChanged(_puzzleState);
        }
    }
    
    private void Visualise(int x, int y, char change)
    {
        if (_visualiser != null)
        {
            _puzzleState = new PuzzleState { Map = null, Change = (x, y, change) };

            _visualiser.PuzzleStateChanged(_puzzleState);
        }
    }

    private void UpdateMap()
    {
        if (_visualiser != null)
        {
            _puzzleState = new PuzzleState
            {
                Map = new char[Map.Length][]
            };

            for (var y = 0; y < Map.Length; y++)
            {
                _puzzleState.Map[y] = new char[Map[y].Length];
            }

            for (var y = 0; y < Map.Length; y++)
            {
                Array.Copy(Map[y], 0, _puzzleState.Map[y], 0, Map[y].Length);
            }

            _visualiser.PuzzleStateChanged(_puzzleState);
        }
    }

    private void RemoveJunk()
    {
        for (var y = 0; y < _height; y++)
        {
            for (var x = 0; x < _width; x++)
            {
                if (Map[y][x] == '#')
                {
                    Visualise(x, y, '.');
                    
                    Map[y][x] = '.';
                }
            }
        }
    }

    private void WalkPipes(int x, int y)
    {
        var queue = new Queue<(int X, int Y)>();
        
        queue.Enqueue((x, y));

        while (queue.Count > 0)
        {
            (x, y) = queue.Dequeue();

            Map[y][x] = 'X';
            
            Visualise(x, y, 'X');

            if (Map[y][x - 1] == '#')
            {
                queue.Enqueue((x - 1, y));
            }

            if (Map[y][x + 1] == '#')
            {
                queue.Enqueue((x + 1, y));
            }

            if (Map[y - 1][x] == '#')
            {
                queue.Enqueue((x, y - 1));
            }

            if (Map[y + 1][x] == '#')
            {
                queue.Enqueue((x, y + 1));
            }
        }
    }

    private int CountEnclosed()
    {
        var count = 0;
        
        for (var y = 2; y < _height; y += 3)
        {
            for (var x = 2; x < _width; x += 3)
            {
                if (Map[y][x] == '.')
                {
                    Visualise(x, y, '@');
                    
                    count++;
                }
            }
        }

        return count;
    }

    private void FloodFill(int x, int y)
    {
        var queue = new Queue<(int X, int Y)>();
        
        queue.Enqueue((x, y));

        while (queue.Count > 0)
        {
            (x, y) = queue.Dequeue();
            
            Map[y][x] = '*';
            
            Visualise(x, y, '*');
            
            if (x > 0 && Map[y][x - 1] == '.')
            {
                Map[y][x - 1] = '*';
                
                queue.Enqueue((x - 1, y));
            }

            if (x < _width - 1 && Map[y][x + 1] == '.')
            {
                Map[y][x + 1] = '*';
                
                queue.Enqueue((x + 1, y));
            }

            if (y < _height - 1 && Map[y + 1][x] == '.')
            {
                Map[y + 1][x] = '*';
                
                queue.Enqueue((x, y + 1));
            }

            if (y > 0 && Map[y - 1][x] == '.')
            {
                Map[y - 1][x] = '*';
                
                queue.Enqueue((x, y - 1));
            }
        }
    }

    private (int X, int Y) ParseInput()
    {
        _height = Input.Length * 3 + 2;

        _width = Input[0].Length * 3 + 2;

        Map = new char[_height][];

        var startX = 0;

        var startY = 0;

        for (var y = 0; y < _height; y++)
        {
            Map[y] = new char[_width];
        }

        Array.Fill(Map[0], '.');
        
        Array.Fill(Map[_height - 1], '.');

        for (var y = 1; y < _height - 1; y += 3)
        {
            Array.Fill(Map[y], '.');
            Array.Fill(Map[y + 1], '.');
            Array.Fill(Map[y + 2], '.');

            for (var x = 1; x < _width - 1; x += 3)
            {
                ParseCell(x, y, Input[(y - 1) / 3][(x - 1) / 3]);

                if (Input[(y - 1) / 3][(x - 1) / 3] == 'S')
                {
                    startX = x + 1;

                    startY = y + 1;
                }
            }
        }

        return (startX, startY);
    }

    private void ParseCell(int x, int y, char cell)
    {
        if (cell == '.')
        {
            return;
        }

        Map[y + 1][x + 1] = '#';

        switch (cell)
        {
            case '-':
                Map[y + 1][x] = '#';
                Map[y + 1][x + 2] = '#';
                break;

            case '|':
                Map[y][x + 1] = '#';
                Map[y + 2][x + 1] = '#';
                break;
            
            case 'S':
                Map[y + 1][x] = '#';
                Map[y + 1][x + 2] = '#';
                Map[y][x + 1] = '#';
                Map[y + 2][x + 1] = '#';
                break;
            
            case '7':
                Map[y + 1][x] = '#';
                Map[y + 2][x + 1] = '#';
                break;
            
            case 'J':
                Map[y + 1][x] = '#';
                Map[y][x + 1] = '#';
                break;
            
            case 'F':
                Map[y + 1][x + 2] = '#';
                Map[y + 2][x + 1] = '#';
                break;
            
            case 'L':
                Map[y][x + 1] = '#';
                Map[y + 1][x + 2] = '#';
                break;
        }
    }
}