using AoC.Solutions.Common;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2015._18;

[UsedImplicitly]
public class Part1 : Base
{
    private const int GridSize = 100;

    private List<Point> _lights = new();

    public override string GetAnswer()
    {
        ParseInput();

        for (var i = 0; i < 100; i++)
        {
            RunStep();

            Console.WriteLine(i);
        }

        return _lights.Count.ToString();
    }

    private void RunStep()
    {
        var newLights = new List<Point>();

        for (var y = 0; y < GridSize; y++)
        {
            for (var x = 0; x < GridSize; x++)
            {
                var position = new Point(x, y);

                if (_lights.Contains(position))
                {
                    if (CountNeighbors(position) is 2 or 3)
                    {
                        newLights.Add(position);
                    }
                }
                else
                {
                    if (CountNeighbors(position) == 3)
                    {
                        newLights.Add(position);
                    }
                }
            }
        }

        _lights = newLights;
    }

    private int CountNeighbors(Point position)
    {
        var count = 0;

        count += _lights.Contains(new Point(position.X - 1, position.Y - 1)) ? 1 : 0;

        count += _lights.Contains(new Point(position.X, position.Y - 1)) ? 1 : 0;

        count += _lights.Contains(new Point(position.X + 1, position.Y - 1)) ? 1 : 0;

        count += _lights.Contains(new Point(position.X - 1, position.Y)) ? 1 : 0;

        count += _lights.Contains(new Point(position.X + 1, position.Y)) ? 1 : 0;

        count += _lights.Contains(new Point(position.X - 1, position.Y + 1)) ? 1 : 0;

        count += _lights.Contains(new Point(position.X, position.Y + 1)) ? 1 : 0;

        count += _lights.Contains(new Point(position.X + 1, position.Y + 1)) ? 1 : 0;

        return count;
    }

    private void ParseInput()
    {
        for (var y = 0; y < GridSize; y++)
        {
            for (var x = 0; x < GridSize; x++)
            {
                if (Input[y][x] == '#')
                {
                    _lights.Add(new Point(x, y));
                }
            }
        }
    }
}