using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2023._14;

public abstract class Base : Solution
{
    public override string Description => "Parabolic reflector dish";

    protected char[,] Rocks;
    
    protected int Rows;

    protected int Columns;
    
    protected int GetLoad()
    {
        var load = 0;
        
        for (var y = 0; y < Rows; y++)
        {
            for (var x = 0; x < Columns; x++)
            {
                if (Rocks[x, y] == 'O')
                {
                    load += Rows - y;
                }
            }
        }

        return load;
    }

    protected int MoveRocks(int dX, int dY)
    {
        if (dY == -1)
        {
            for (var y = 0; y < Rows; y++)
            {
                for (var x = 0; x < Columns; x++)
                {
                    if (Rocks[x, y] == 'O')
                    {
                        MoveRock(x, y, dX, dY);
                    }
                }
            }
            
            return 0;
        }
        
        if (dX == -1)
        {
            for (var x = 0; x < Columns; x++)
            {
                for (var y = 0; y < Rows; y++)
                {
                    if (Rocks[x, y] == 'O')
                    {
                        MoveRock(x, y, dX, dY);
                    }
                }
            }
            
            return 0;
        }
        
        if (dY == 1)
        {
            for (var y = Rows - 1; y >= 0; y--)
            {
                for (var x = 0; x < Columns; x++)
                {
                    if (Rocks[x, y] == 'O')
                    {
                        MoveRock(x, y, dX, dY);
                    }
                }
            }
            
            return 0;
        }
        
        if (dX == 1)
        {
            for (var x = Columns - 1; x >= 0; x--)
            {
                for (var y = 0; y < Rows; y++)
                {
                    if (Rocks[x, y] == 'O')
                    {
                        MoveRock(x, y, dX, dY);
                    }
                }
            }

            var hash = 0;
            
            for (var y = 0; y < Rows; y++)
            {
                var rowString = new char[Columns];
            
                for (var x = 0; x < Columns; x++)
                {
                    rowString[x] = Rocks[x, y];
                }

                hash = HashCode.Combine(hash, new string(rowString).GetHashCode());
            }

            return hash;
        }

        return 0;
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
            
            while (y < Rows - 1 && Rocks[x, y + 1] == '.')
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
            
            while (x < Columns - 1 && Rocks[x + 1, y] == '.')
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
        Rows = Input.Length;

        Columns = Input[0].Length;
        
        Rocks = new char[Input[0].Length, Input.Length];

        var y = 0;

        foreach (var line in Input)
        {
            for (var x = 0; x < line.Length; x++)
            {
                Rocks[x, y] = line[x];
            }

            y++;
        }
    }
}