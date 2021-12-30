#define DUMP
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2021._23;

public abstract class Base : Solution
{
    public override string Description => "Hotel amphipod";

    private int[] _initialAmphipodState;

    private int _lowestCost;

    private Queue<(int[] State, int Index, int Cost)> _queue;

    private HashSet<int> _encounteredStates;

    // Stack for each room?
    protected void ParseInput()
    {
        _initialAmphipodState = new int[8];

        var count = 0;

        for (var y = 1; y < 4; y++)
        {
            for (var x = 1; x < 12; x += 2)
            {
                if (x >= Input[y].Length)
                {
                    continue;
                }

                var c = Input[y][x];

                if (c == '#' || c == '.' || c == ' ')
                {
                    continue;
                }

                _initialAmphipodState[count] = Encode(x - 1, y - 1, (c - '@') * 2);

                count++;
            }
        }

#if DEBUG && DUMP
        Console.Clear();

        Console.CursorVisible = false;

        Dump(_initialAmphipodState);
#endif
    }

    protected int Solve()
    {
        _lowestCost = int.MaxValue;

        _queue = new Queue<(int[] State, int Index, int Cost)>();

        _encounteredStates = new HashSet<int>();

        for (var i = 0; i < _initialAmphipodState.Length; i++)
        {
            // Could only enqueue top pods here.
            _queue.Enqueue((Copy(_initialAmphipodState), i, 0));

            IsNewState(_initialAmphipodState, i, 0);
        }

        var cost = ProcessQueue();

        return cost;
    }

    private int ProcessQueue()
    {
        while (_queue.Count > 0)
        {
            var (state, index, cost) = _queue.Dequeue();

            var moves = GetMovesAndCosts(state, index);

            foreach (var move in moves)
            {
                if (cost + move.Cost >= _lowestCost)
                {
                    continue;
                }

                // Check all home, if so, update cost and continue.
                if (AllHome(move.State))
                {
                    if (cost + move.Cost < _lowestCost)
                    {
                        _lowestCost = cost + move.Cost;
                    }

                    continue;
                }

                // Re: hashing, add the index (and maybe cost) to the hash.
                // What about hashes of the first enqueued states?

#if DEBUG && DUMP
                Dump(state);

                Console.WriteLine($"{new string('.', _queue.Count)} ");
#endif

                for (var i = 0; i < state.Length; i++)
                {
                    // Same pod can't move twice in a row...?
                    if (i == index)
                    {
                        continue;
                    }

                    _queue.Enqueue((move.State, i, cost + move.Cost));
                }
            }
        }


        return int.MaxValue;
    }

    private bool IsNewState(int[] state, int index, int cost)
    {
        var hash = 0;

        for (var i = 0; i < state.Length; i++)
        {
            hash = HashCode.Combine(hash, state[i]);
        }

        hash = HashCode.Combine(hash, index);

        hash = HashCode.Combine(hash, cost);

        if (_encounteredStates.Contains(hash))
        {
            return false;
        }

        _encounteredStates.Add(hash);

        return true;
    }

    private static bool AllHome(int[] state)
    {
        for (var i = 0; i <= state.Length; i++)
        {
            var pod = Decode(state[i]);

            if (pod.Y == 0 || pod.X != pod.Home)
            {
                return false;
            }
        }

        return true;
    }

    private static List<(int[] State, int Cost)> GetMovesAndCosts(int[] state, int index)
    {
        return null;
    }

    private int[] Copy(int[] source)
    {
        var copy = new int[source.Length];

        Buffer.BlockCopy(source, 0, copy, 0, source.Length * sizeof(int));

        return copy;
    }

    private static int Encode(int x, int y, int home)
    {
        var result = ((x & 255) << 16)
                     | ((y & 255) << 8)
                     | (home & 255);

        return result;
    }

    private static (int X, int Y, int Home, int Id) Decode(int state)
    {
        var x = DecodeX(state);

        var y = DecodeY(state);

        var home = DecodeHome(state);

        var id = state & 255;

        return (x, y, home, id);
    }

    private static int DecodeX(int state)
    {
        return (state >> 16) & 255;
    }

    private static int DecodeY(int state)
    {
        return (state >> 8) & 255;
    }

    private static int DecodeHome(int state)
    {
        return state & 255;
    }

#if DEBUG && DUMP
    private void Dump(int[] state)
    {
        Console.CursorTop = 1;

        Console.CursorLeft = 0;

        Console.WriteLine(" #############");

        Console.Write(" #");

        var previousColour = Console.ForegroundColor;

        for (var x = 0; x < 11; x++)
        {
            var pod = state.SingleOrDefault(s => DecodeX(s) == x && DecodeY(s) == 0);

            if (pod == 0)
            {
                Console.Write('.');

                continue;
            }

            Console.ForegroundColor = ConsoleColor.Blue;

            Console.Write(GetType(pod));

            Console.ForegroundColor = previousColour;
        }

        Console.WriteLine('#');

        for (var y = 1; y < 3; y++)
        {
            Console.Write(y == 1 ? " ###" : "   #");

            for (var x = 2; x < 10; x += 2)
            {
                var pod = state.SingleOrDefault(s => DecodeX(s) == x && DecodeY(s) == y);

                if (pod == 0)
                {
                    Console.Write(".#");

                    continue;
                }

                Console.ForegroundColor = ConsoleColor.Blue;

                Console.Write(GetType(pod));

                Console.ForegroundColor = previousColour;

                Console.Write('#');
            }

            Console.WriteLine(y == 1 ? "##" : string.Empty);
        }

        Console.WriteLine("   #########");

        Console.WriteLine();
    }

    private char GetType(int home)
    {
        return (char) ('@' + (char) (DecodeHome(home) / 2));
    }
#endif
}