using AoC.Solutions.Extensions;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2023._14;

public abstract class Base : Solution
{
    public override string Description => "Parabolic reflector dish";

    private char[,] Rocks;

    private int _rows;

    private int _columns;
    
    protected int GetLoad()
    {
        var load = 0;
        
        for (var y = 0; y < _rows; y++)
        {
            for (var x = 0; x < _columns; x++)
            {
                if (Rocks[x, y] == 'O')
                {
                    load += _rows - y;
                }
            }
        }

        return load;
    }

    protected (int Hash, int Load) MoveRocks(int dX, int dY)
    {
        if (dY == -1)
        {
            for (var y = 0; y < _rows; y++)
            {
                for (var x = 0; x < _columns; x++)
                {
                    if (Rocks[x, y] == 'O')
                    {
                        MoveRock(x, y, dX, dY);
                    }
                }
            }
            
            return (0, 0);
        }
        
        if (dX == -1)
        {
            for (var x = 0; x < _columns; x++)
            {
                for (var y = 0; y < _rows; y++)
                {
                    if (Rocks[x, y] == 'O')
                    {
                        MoveRock(x, y, dX, dY);
                    }
                }
            }
            
            return (0, 0);
        }
        
        if (dY == 1)
        {
            for (var y = _rows - 1; y >= 0; y--)
            {
                for (var x = 0; x < _columns; x++)
                {
                    if (Rocks[x, y] == 'O')
                    {
                        MoveRock(x, y, dX, dY);
                    }
                }
            }

            return (0, 0);
        }
        
        if (dX == 1)
        {
            var hash = 0;

            var load = 0;
            
            for (var y = 0; y < _rows; y++)
            {
                var hashString = new char[_columns];
            
                for (var x = _columns - 1; x >= 0; x--)
                {
                    if (Rocks[x, y] == 'O')
                    {
                        MoveRock(x, y, dX, dY);
                    }
                }

                for (var x = 0; x < _columns; x++)
                {
                    hashString[x] = Rocks[x, y];

                    if (Rocks[x, y] == 'O')
                    {
                        load += _rows - y;
                    }
                }

                hash = HashCode.Combine(hash, new string(hashString).GetHashCode());
            }

            return (hash, load);
        }

        return (0, 0);
    }

    private void MoveRock(int x, int y, int dX, int dY)
    {
        if (dY == -1)
        {
            var oY = y;
            
            while (y > 0 && Rocks[x, y - 1] == '.')
            {
                y--;
            }

            if (oY != y)
            {
                Rocks[x, y] = 'O';
                Rocks[x, oY] = '.';
            }
            
            return;
        }
        
        if (dX == -1)
        {
            var oX = x;
            
            while (x > 0 && Rocks[x - 1, y] == '.')
            {
                x--;
            }

            if (oX != x)
            {
                Rocks[x, y] = 'O';
                Rocks[oX, y] = '.';
            }
            
            return;
        }
        
        if (dY == 1)
        {
            var oY = y;
            
            while (y < _rows - 1 && Rocks[x, y + 1] == '.')
            {
                y++;
            }

            if (oY != y)
            {
                Rocks[x, y] = 'O';
                Rocks[x, oY] = '.';
            }
            
            return;
        }
        
        if (dX == 1)
        {
            var oX = x;
            
            while (x < _columns - 1 && Rocks[x + 1, y] == '.')
            {
                x++;
            }

            if (oX != x)
            {
                Rocks[x, y] = 'O';
                Rocks[oX, y] = '.';
            }
        }
    }

    protected void ParseInput()
    {
        _rows = Input.Length;

        _columns = Input[0].Length;
        
        Rocks = Input.To2DArray();
    }
}