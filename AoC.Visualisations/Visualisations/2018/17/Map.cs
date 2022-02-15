namespace AoC.Visualisations.Visualisations._2018._17;

public class Map
{
    private int[,] _map;

    public int Width { get; private set; }

    public int Height { get; private set; }

    private Random Random { get; } = new();

    public void CreateMap(char[,] puzzleMap)
    {
        Width = puzzleMap.GetLength(0);

        Height = puzzleMap.GetLength(1);

        _map = new int[Width, Height];

        AddBackgroundBricks();

        AddContainers(puzzleMap);
    }

    public int this[int x, int y] => _map[x, y];

    private void AddContainers(char[,] puzzleMap)
    {
        for (var y = 1; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                if (puzzleMap[x, y] == '#' && puzzleMap[x, y - 1] == '#' && puzzleMap[x + 1, y] == '#')
                {
                    AddContainer(puzzleMap, x, y);
                }
            }
        }
    }

    private void AddContainer(char[,] puzzleMap, int leftX, int bottomY)
    {
        var leftTopY = 0;

        var rightTopY = 0;

        var rightX = 0;

        for (var y = bottomY - 1; y > 0; y--)
        {
            if (puzzleMap[leftX, y] != '#')
            {
                leftTopY = y + 1;

                break;
            }
        }

        for (var x = leftX + 1; x < Width; x++)
        {
            if (puzzleMap[x, bottomY] != '#')
            {
                rightX = x - 1;

                break;
            }
        }

        for (var y = bottomY - 1; y > 0; y--)
        {
            if (puzzleMap[rightX, y] != '#')
            {
                rightTopY = y + 1;

                break;
            }
        }

        AddContainerToMap(leftX, rightX, leftTopY, rightTopY, bottomY, puzzleMap[leftX + 1, leftTopY] == '#');
    }

    private void AddContainerToMap(int leftX, int rightX, int leftTop, int rightTop, int bottom, bool closed)
    {
        var tileRow = Random.Next(3) * 11 + 1;

        _map[leftX, bottom] = tileRow;

        for (var x = leftX + 1; x < rightX; x++)
        {
            _map[x, bottom] = tileRow + 1;

            if (tileRow == 1 && bottom < Height - 1 && Random.Next(20) == 0)
            {
                _map[x, bottom + 1] = 40;

                _map[x + 1, bottom + 1] = 41;
            }
        }

        _map[rightX, bottom] = tileRow + 2;

        for (var y = bottom - 1; y >= leftTop; y--)
        {
            _map[leftX, y] = tileRow + 3;

            if (Random.Next(5) == 0 && rightX - leftX > 3)
            {
                _map[leftX + 1, y] = tileRow + 4;
            }

            if (Random.Next(5) == 0 && leftX > 0)
            {
                _map[leftX - 1, y] = tileRow + 7;
            }
        }

        for (var y = bottom - 1; y >= rightTop; y--)
        {
            _map[rightX, y] = tileRow + 6;

            if (Random.Next(5) == 0 && rightX - leftX > 3)
            {
                _map[rightX - 1, y] = tileRow + 5;
            }

            if (Random.Next(5) == 0 && rightX < Width)
            {
                _map[rightX + 1, y] = tileRow + 8;
            }
        }

        for (var y = bottom - 1; y >= Math.Max(leftTop, rightTop); y--)
        {
            for (var x = leftX + 1; x < rightX; x++)
            {
                if (_map[x, y] > 33 && _map[x, y] < 40)
                {
                    _map[x, y] += 11;
                }
            }
        }

        if (closed)
        {
            for (var x = leftX + 1; x < rightX; x++)
            {
                _map[x, leftTop] = tileRow + 1;
            }
        }
    }

    private void AddBackgroundBricks()
    {
        for (var y = 1; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                if (Random.Next(50) == 0)
                {
                    AddBrickClump(x, y);
                }
            }
        }
    }

    private void AddBrickClump(int x, int y)
    {
        var startLeft = x - 1 - Random.Next(8);

        var startRight = x + 1 + Random.Next(8);

        AddClumpRow(startLeft, startRight, y);

        var left = startLeft;

        var right = startRight;

        for (var mY = y; mY > y - Random.Next(4); mY++)
        {
            left += Random.Next(3);
        
            right -= Random.Next(3);

            if (left > right)
            {
                break;
            }

            AddClumpRow(left, right, mY);
        }

        left = startLeft;

        right = startRight;

        for (var mY = y; mY < y + Random.Next(4); mY--)
        {
            left += Random.Next(3);
        
            right -= Random.Next(3);

            if (left > right)
            {
                break;
            }

            AddClumpRow(left, right, mY);
        }
    }

    private void AddClumpRow(int left, int right, int y)
    {
        SafeSetMap(left, y, 34 + Random.Next(6));

        SafeSetMap(right, y, 34 + Random.Next(6));

        for (var x = left + 1; x <= right - 1; x++)
        {
            SafeSetMap(x, y, 34);
        }
    }

    private void SafeSetMap(int x, int y, int value)
    {
        if (x >= 0 && x < Width && y >= 0 && y < Height)
        {
            _map[x, y] = value;
        }
    }
}