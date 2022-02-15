namespace AoC.Visualisations.Visualisations._2018._17;

public class Map
{
    private int[,] _map;

    public int Width { get; private set; }

    private int _height;

    private int _originX;

    private Random Random { get; } = new();

    public void CreateMap(char[,] puzzleMap)
    {
        Width = puzzleMap.GetLength(0);

        _height = puzzleMap.GetLength(1);

        _map = new int[Width, _height];

        FindOrigin(puzzleMap);

        // Use this to not place torches in containers? Or just have them extinguish?
        var containers = AddContainers(puzzleMap);
    }

    public int this[int x, int y] => _map[x, y];

    private void FindOrigin(char[,] puzzleMap)
    {
        for (var x = 0; x < Width; x++)
        {
            if (puzzleMap[x, 0] == '+')
            {
                _originX = x;

                break;
            }
        }
    }

    private List<Rectangle> AddContainers(char[,] puzzleMap)
    {
        var containers = new List<Rectangle>();

        for (var y = 1; y < _height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                if (puzzleMap[x, y] == '#' && puzzleMap[x, y - 1] == '#' && puzzleMap[x + 1, y] == '#')
                {
                    containers.Add(AddContainer(puzzleMap, x, y));
                }
            }
        }

        return containers;
    }

    private Rectangle AddContainer(char[,] puzzleMap, int leftX, int bottomY)
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

        AddContainerToMap(leftX, rightX, leftTopY, rightTopY, bottomY);

        return new Rectangle(leftX, Math.Min(leftTopY, rightTopY), rightX - leftX + 1, Math.Max(leftTopY, rightTopY) - bottomY + 1);
    }

    private void AddContainerToMap(int leftX, int rightX, int leftTop, int rightTop, int bottom)
    {
        var tileRow = Random.Next(3) * 11 + 1;

        _map[leftX, bottom] = tileRow;

        for (var x = leftX + 1; x < rightX; x++)
        {
            _map[x, bottom] = tileRow + 1;
        }

        _map[rightX, bottom] = tileRow + 2;

        for (var y = bottom - 1; y >= leftTop; y--)
        {
            _map[leftX, y] = tileRow + 3;
        }

        for (var y = bottom - 1; y >= rightTop; y--)
        {
            _map[rightX, y] = tileRow + 6;
        }
    }

    private void AddBackground()
    {
    }
}