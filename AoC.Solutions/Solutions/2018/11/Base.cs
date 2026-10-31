
using AoC.Solutions.Common;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2018._11;

public abstract class Base : Solution
{
    public override string Description => "Max power... cell";

    protected const int GridSize = 300;

    private const int Stride = GridSize + 1;

    private readonly int[] _sums = new int[Stride * Stride];

    protected void CalculateCellPowers(int serial)
    {
        Array.Clear(_sums);

        for (var y = 0; y < GridSize; y++)
        {
            var rowSum = 0;
            
            var row = (y + 1) * Stride;
            
            var previousRow = y * Stride;

            for (var x = 0; x < GridSize; x++)
            {
                rowSum += CalculatePowerLevel(x, y, serial);

                _sums[row + x + 1] = _sums[previousRow + x + 1] + rowSum;
            }
        }
    }

    protected (Point Position, int Max) GetMaxPower(int squareSize)
    {
        var max = 0;
        
        Point position = null;

        var limit = GridSize - squareSize;

        for (var y = squareSize - 1; y < limit; y++)
        {
            var top = y * Stride;
            
            var bottom = (y + squareSize) * Stride;

            for (var x = squareSize - 1; x < limit; x++)
            {
                var right = x + squareSize;

                var sum = _sums[bottom + right] - _sums[top + right] - _sums[bottom + x] + _sums[top + x];

                if (sum <= max)
                {
                    continue;
                }

                max = sum;
                
                position = new Point(x, y);
            }
        }

        return (position, max);
    }

    protected int ParseInput()
    {
        return int.Parse(Input[0]);
    }

    private static int CalculatePowerLevel(int x, int y, int serial)
    {
        var rackId = x + 10;

        var power = rackId * y;
        
        power += serial;
        
        power *= rackId;

        return power / 100 % 10 - 5;
    }
}
