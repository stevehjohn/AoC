using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._18;

[UsedImplicitly]
public class Part1 : Base
{
    private readonly List<(char Direction, int Steps)> _moves = new();

    private int _width;

    private int _height;
    
    private char[,] _map;
    
    public override string GetAnswer()
    {
        ParseInput();

        _width = 9;

        _height = 12;
        
        _map = new char[_width, _height];

        DigTrench();
        
        Dump();
        
        return "Unknown";
    }

    private void Dump()
    {
        for (var y = 0; y < _height; y++)
        {
            for (var x = 0; x < _width; x++)
            {
                Console.Write(_map[x, y] == '#' ? '#' : ' ');
            }
            
            Console.WriteLine();
        }
    }

    private void DigTrench()
    {
        int x = 1, y = 1;

        _map[x, y] = '#';

        foreach (var move in _moves)
        {
            var dX = move.Direction switch
            {
                'L' => -1,
                'R' => 1,
                _ => 0
            };
            
            var dY = move.Direction switch
            {
                'U' => -1,
                'D' => 1,
                _ => 0
            };

            for (var i = 0; i < move.Steps; i++)
            {
                x += dX;
                y += dY;

                _map[x, y] = '#';
            }
        }
    }

    private void ParseInput()
    {
        foreach (var line in Input)
        {
            var parts = line.Split(' ');
            
            _moves.Add((parts[0][0], int.Parse(parts[1])));
        }
    }
}