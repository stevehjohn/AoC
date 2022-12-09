using AoC.Solutions.Common;

namespace AoC.Solutions.Solutions._2022._09;

public class Part1 : Base
{
    private const int Width = 10_000;

    private Point _head;

    private Point _tail;

    private readonly HashSet<int> _tailVisited = new();

    public override string GetAnswer()
    {
        ProcessInput();

        return _tailVisited.Count.ToString();
    }

    private void ProcessInput()
    {
        _head = new Point(0, 0);

        _tail = new Point(0, 0);

        _tailVisited.Add(_tail.X + _tail.Y * Width);

        Console.WriteLine($"H: ({_head.X}, {_head.Y})   T: ({_tail.X}, {_tail.Y})");

        foreach (var line in Input)
        {
            var distance = int.Parse(line[2..]);

            switch (line[0])
            {
                case 'U':
                    MoveHead(0, distance);
                    break;
                case 'R':
                    MoveHead(distance, 0);
                    break;
                case 'D':
                    MoveHead(0, -distance);
                    break;
                case 'L':
                    MoveHead(-distance, 0);
                    break;
            }

            Console.WriteLine($"H: ({_head.X}, {_head.Y})   T: ({_tail.X}, {_tail.Y})");
        }
    }

    private void MoveHead(int x, int y)
    {
        while (x != 0 || y != 0)
        {
            if (x > 0)
            {
                _head.X++;

                x--;
            }
            else if (x < 0)
            {
                _head.X--;

                x++;
            }

            if (y > 0)
            {
                _head.Y++;

                y--;
            }
            else if (y < 0)
            {
                _head.Y--;

                y++;
            }

            MoveTail();
        }
    }

    private void MoveTail()
    {
        if (Math.Abs(_head.X - _tail.X) < 2 && Math.Abs(_head.Y - _tail.Y) < 2)
        {
            return;
        }

        var distances = new double[9];

        for (var x = -1; x < 2; x++)
        {
            for (var y = -1; y < 2; y++)
            {
                distances[x + 1 + (y + 1) * 3] = Math.Sqrt(Math.Pow(Math.Abs(_head.X - (_tail.X + x)), 2) + Math.Pow(Math.Abs(_head.Y - (_tail.Y + y)), 2));
            }
        }

        var closest = distances.Select((d, i) => new { Distance = d, Index = i })
                               .OrderBy(i => i.Distance)
                               .First()
                               .Index;

        var dX = closest % 3 - 1;

        var dY = closest / 3 - 1;

        _tail.X += dX;

        _tail.Y += dY;

        _tailVisited.Add(_tail.X + _tail.Y * Width);
    }
}