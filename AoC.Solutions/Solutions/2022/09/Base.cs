using System.Runtime.CompilerServices;
using AoC.Solutions.Common;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2022._09;

public abstract class Base : Solution
{
    public override string Description => "Rope bridge";

    private const int Width = 10_000;

    private int _knotCount;

    private Point[] _knots;

    protected readonly HashSet<int> TailVisited = new();

    protected void ProcessInput(int knots = 2)
    {
        _knotCount = knots;

        _knots = new Point[_knotCount];

        for (var i = 0; i < _knotCount; i++)
        {
            _knots[i] = new Point(0, 0);
        }

        TailVisited.Add(_knots[_knotCount - 1].X + _knots[_knotCount - 1].Y * Width);

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
        }
    }

    private void MoveHead(int x, int y)
    {
        while (x != 0 || y != 0)
        {
            if (x > 0)
            {
                _knots[0].X++;

                x--;
            }
            else if (x < 0)
            {
                _knots[0].X--;

                x++;
            }

            if (y > 0)
            {
                _knots[0].Y++;

                y--;
            }
            else if (y < 0)
            {
                _knots[0].Y--;

                y++;
            }

            MoveNextKnot();

            TailVisited.Add(_knots[_knotCount - 1].X + _knots[_knotCount - 1].Y * Width);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization)]
    private void MoveNextKnot(int currentKnot = 0)
    {
        while (true)
        {
            if (Abs(_knots[currentKnot].X - _knots[currentKnot + 1].X) < 2 && Abs(_knots[currentKnot].Y - _knots[currentKnot + 1].Y) < 2)
            {
                return;
            }

            var distances = new double[9];

            for (var x = -1; x < 2; x++)
            {
                for (var y = -1; y < 2; y++)
                {
                    distances[x + 1 + (y + 1) * 3] = Math.Sqrt(Square(Abs(_knots[currentKnot].X - (_knots[currentKnot + 1].X + x))) + Square(Abs(_knots[currentKnot].Y - (_knots[currentKnot + 1].Y + y))));
                }
            }

            var min = double.MaxValue;

            var minIndex = 0;

            for (var i = 0; i < 9; i++)
            {
                if (distances[i] < min)
                {
                    min = distances[i];

                    minIndex = i;
                }
            }

            var dX = minIndex % 3 - 1;

            var dY = minIndex / 3 - 1;

            _knots[currentKnot + 1].X += dX;

            _knots[currentKnot + 1].Y += dY;

            if (currentKnot < _knotCount - 2)
            {
                currentKnot++;
                
                continue;
            }

            break;
        }
    }

    private static double Square(double value)
    {
        return value * value;
    }

    private static int Abs(int value)
    {
        return (value + (value >> 31)) ^ (value >> 31);
    }
}