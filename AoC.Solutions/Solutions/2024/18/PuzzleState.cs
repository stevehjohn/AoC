using AoC.Solutions.Common;

namespace AoC.Solutions.Solutions._2024._18;

public class PuzzleState
{
    public const int Size = 73;

    public State State { get; }

    public static char[,] Map { get; private set; }

    public PuzzleState(string[] input, State state)
    {
        if (Map == null)
        {
            Map = new char[Size, Size];
            
            for (var y = 0; y < Size; y++)
            {
                for (var x = 0; x < Size; x++)
                {
                    if (x is 0 or Size - 1 || y is 0 or Size - 1)
                    {
                        Map[x, y] = '#';
                        
                        continue;
                    }

                    Map[x, y] = '.';
                }
            }

            for (var i = 0; i < 1_024; i++)
            {
                var point = new Point2D(input[i]);

                Map[point.X + 1, point.Y + 1] = '#';
            }
        }

        State = state;
    }
}