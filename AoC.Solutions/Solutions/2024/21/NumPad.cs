using AoC.Solutions.Common;

namespace AoC.Solutions.Solutions._2024._21;

public class NumPad
{
    private readonly Dictionary<Point2D, char> _layout = new()
    {
        { new Point2D(0, 0), '7' },
        { new Point2D(1, 0), '8' },
        { new Point2D(2, 0), '9' },
        { new Point2D(0, 1), '4' },
        { new Point2D(1, 1), '5' },
        { new Point2D(2, 1), '6' },
        { new Point2D(0, 2), '1' },
        { new Point2D(1, 2), '2' },
        { new Point2D(2, 2), '3' },
        { new Point2D(1, 3), '0' },
        { new Point2D(2, 3), 'A' }
    };

    private Point2D _position = new(2, 3);

    public List<string> GetSequences(string code)
    {
        var queue = new PriorityQueue<(Point2D Position, int Digit, string Moves), int>();

        queue.Enqueue((_position, 0, string.Empty), 0);

        var results = new List<string>();

        var length = int.MaxValue;
        
        while (queue.Count > 0)
        {
            var state = queue.Dequeue();

            if (! _layout.TryGetValue(state.Position, out var value))
            {
                continue;
            }
            
            if (value == code[state.Digit])
            {
                state.Moves = $"{state.Moves}A";

                state.Digit++;

                if (state.Digit == code.Length)
                {
                    _position = state.Position;

                    if (state.Moves.Length <= length)
                    {
                        length = state.Moves.Length;
                    }
                    else
                    {
                        break;
                    }
                    
                    Console.WriteLine(state.Moves);
                    
                    results.Add(state.Moves);
                }
            }

            queue.Enqueue((state.Position + Point2D.North, state.Digit, $"{state.Moves}^"), state.Digit + state.Moves.Length + 1);
            queue.Enqueue((state.Position + Point2D.East, state.Digit, $"{state.Moves}>"), state.Digit + state.Moves.Length + 1);
            queue.Enqueue((state.Position + Point2D.South, state.Digit, $"{state.Moves}v"), state.Digit + state.Moves.Length + 1);
            queue.Enqueue((state.Position + Point2D.West, state.Digit, $"{state.Moves}<"), state.Digit + state.Moves.Length + 1);
        }

        return results;
    }
}