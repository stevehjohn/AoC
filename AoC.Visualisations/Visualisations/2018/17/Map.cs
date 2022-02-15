namespace AoC.Visualisations.Visualisations._2018._17;

public class Map
{
    private int[,] _map;

    private int _width;

    private int _height;

    private int _originX;

    public void CreateMap(char[,] puzzleMap)
    {
        _width = puzzleMap.GetLength(0);

        _height = puzzleMap.GetLength(1);

        _map = new int[_width, _height];

        FindOrigin(puzzleMap);

        var containers = AddContainers();
    }

    private void FindOrigin(char[,] puzzleMap)
    {
        for (var x = 0; x < _width; x++)
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
            for (var x = 0; x < _width; x++)
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

        for (var x = leftX + 1; x < _width; x++)
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

        return new Rectangle(leftX, Math.Min(leftTopY, rightTopY), rightX - leftX + 1, Math.Max(leftTopY, rightTopY) - bottomY + 1);
    }

    private void AddBackground()
    {
    }
}