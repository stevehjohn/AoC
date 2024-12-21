using AoC.Solutions.Common;

namespace AoC.Solutions.Solutions._2024._21;

public class DPad
{
    private readonly Dictionary<Point2D, char> _layout = new()
    {
        { new Point2D(1, 0), '^' },
        { new Point2D(2, 0), 'A' },
        { new Point2D(0, 1), '<' },
        { new Point2D(1, 1), 'v' },
        { new Point2D(2, 1), '>' }
    };

    private Point2D _position = new(2, 0);

    public string GetSequence(string code)
    {
        var queue = new PriorityQueue<(Point2D Position, int Digit, string Moves), int>();

        queue.Enqueue((_position, 0, string.Empty), 0);

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
                    
                    return state.Moves;
                }
            }

            queue.Enqueue((state.Position + Point2D.North, state.Digit, $"{state.Moves}^"), state.Moves.Length + 1);
            queue.Enqueue((state.Position + Point2D.East, state.Digit, $"{state.Moves}>"), state.Moves.Length + 1);
            queue.Enqueue((state.Position + Point2D.South, state.Digit, $"{state.Moves}v"), state.Moves.Length + 1);
            queue.Enqueue((state.Position + Point2D.West, state.Digit, $"{state.Moves}<"), state.Moves.Length + 1);
        }

        return null;
    }
}