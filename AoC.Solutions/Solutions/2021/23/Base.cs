using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2021._23;

public abstract class Base : Solution
{
    public override string Description => "Hotel amphipod";

    private int[] _initialAmphipodState;

    private int _lowestCost;

    private PriorityQueue<(int[] State, int Index, int Cost), int> _queue;

    private HashSet<int> _encounteredStates;

    private static int _burrowDepth = 2;

    private static readonly int[] Hallway = [0, 1, 3, 5, 7, 9, 10];

    protected void ParseInput(bool insertExtra = false)
    {
        var state = new List<int>();

        if (insertExtra)
        {
            _burrowDepth = 4;

            var newInput = new List<string>();

            newInput.AddRange(Input.Take(3));

            newInput.Add("  #D#C#B#A#");

            newInput.Add("  #D#B#A#C#");

            newInput.AddRange(Input.Skip(3).Take(2));

            Input = newInput.ToArray();
        }

        for (var y = 1; y < 2 + _burrowDepth; y++)
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

                state.Add(Encode(x - 1, y - 1, (c - '@') * 2));
            }
        }

        _initialAmphipodState = state.ToArray();

#if DEBUG && DUMP
        Console.Clear();

        Console.CursorVisible = false;

        Dump(_initialAmphipodState);
#endif
    }

    protected int Solve()
    {
        _lowestCost = int.MaxValue;

        _queue = new PriorityQueue<(int[] State, int Index, int Cost), int>();

        _encounteredStates = [];

        for (var i = 0; i < _initialAmphipodState.Length; i++)
        {
            // Only enqueue top pods here.
            if (DecodeY(_initialAmphipodState[i]) != 1)
            {
                continue;
            }

            _queue.Enqueue((Copy(_initialAmphipodState), i, 0), 0);

            IsNewState(_initialAmphipodState, i);
        }

        ProcessQueue();

        return _lowestCost;
    }

    private void ProcessQueue()
    {
        while (_queue.Count > 0)
        {
            var (state, index, cost) = _queue.Dequeue();

            var moves = GetMovesAndCosts(state, index);

            foreach (var move in moves)
            {
                var moveCost = cost + move.Cost;

                if (moveCost >= _lowestCost)
                {
                    continue;
                }

                // Check against hash set.
                if (! IsNewState(move.State, index))
                {
                    continue;
                }

                // Check all home, if so, update cost and continue.
                if (AllHome(move.State))
                {
                    if (moveCost < _lowestCost)
                    {
                        _lowestCost = moveCost;
                    }

                    continue;
                }

#if DEBUG && DUMP
                Console.CursorTop = 9;

                Console.WriteLine($" {_queue.Count}      ");
#endif

                for (var i = 0; i < state.Length; i++)
                {
                    // Same pod can't move twice in a row...?
                    if (i == index)
                    {
                        continue;
                    }

                    _queue.Enqueue((move.State, i, moveCost), moveCost);
                }
            }
        }
    }

    private static int GetCostMultiplier(int type)
    {
        return type switch
        {
            4 => 10,
            6 => 100,
            8 => 1000,
            _ => 1
        };
    }

    private bool IsNewState(int[] state, int index)
    {
        var hash = 0;

        for (var i = 0; i < state.Length; i++)
        {
            hash = HashCode.Combine(hash, state[i]);
        }

        hash = HashCode.Combine(hash, index);

        if (! _encounteredStates.Add(hash))
        {
            return false;
        }

        return true;
    }

    private static bool AllHome(int[] state)
    {
        for (var i = 0; i < state.Length; i++)
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
        var result = new List<(int[] State, int Cost)>();

        var pod = Decode(state[index]);

        int y;

        // Is home?
        if (pod.X == pod.Home && pod.Y > 0)
        {
            if (pod.Y == _burrowDepth)
            {
                return result;
            }

            var same = true;

            // Check not blocking a different type.
            for (y = pod.Y + 1; y <= _burrowDepth; y++)
            {
                if (TypeInPosition(state, pod.X, y) != pod.Home)
                {
                    same = false;

                    break;
                }
            }

            if (same)
            {
                return result;
            }
        }

        // Can get home from current position?
        int cost;

        for (y = _burrowDepth; y > 0; y--)
        {
            var typeInPosition = TypeInPosition(state, pod.Home, y);

            if (typeInPosition == 0)
            {
                cost = CostToGetTo(state, pod.X, pod.Y, pod.Home, y) * GetCostMultiplier(pod.Home);

                if (cost > 0)
                {
                    result.Add((MakeMove(state, pod.X, pod.Y, pod.Home, y), cost));

                    return result;
                }

                // Spot is empty, but can't get there.
                break;
            }

            if (typeInPosition != pod.Home)
            {
                break;
            }
        }

        // Can get to hall? If so, add all possible positions.
        if (pod.Y > 0)
        {
            foreach (var x in Hallway)
            {
                cost = CostToGetTo(state, pod.X, pod.Y, x, 0) * GetCostMultiplier(pod.Home);

                if (cost > 0)
                {
                    result.Add((MakeMove(state, pod.X, pod.Y, x, 0), cost));
                }
            }
        }

        return result;
    }

    private static int[] MakeMove(int[] state, int startX, int startY, int endX, int endY)
    {
        var newState = Copy(state);

        for (var i = 0; i < state.Length; i++)
        {
            var pod = Decode(state[i]);

            if (pod.X == startX && pod.Y == startY)
            {
                newState[i] = Encode(endX, endY, pod.Home);

                break;
            }
        }

        return newState;
    }

    private static int CostToGetTo(int[] state, int startX, int startY, int endX, int endY)
    {
        var cost = 0;

        // Emerge from burrow
        while (startX != endX && startY > 0)
        {
            startY--;

            if (TypeInPosition(state, startX, startY) != 0)
            {
                return 0;
            }

            cost++;
        }

        // Travel hall
        while (startX != endX)
        {
            startX += startX > endX ? -1 : 1;

            if (TypeInPosition(state, startX, startY) != 0)
            {
                return 0;
            }

            cost++;
        }

        // Enter burrow
        while (startY < endY)
        {
            startY++;

            if (TypeInPosition(state, startX, startY) != 0)
            {
                return 0;
            }

            cost++;
        }

        return cost;
    }

    private static int TypeInPosition(int[] state, int x, int y)
    {
        for (var i = 0; i < state.Length; i++)
        {
            var pod = Decode(state[i]);

            if (pod.X == x && pod.Y == y)
            {
                return pod.Home;
            }
        }

        return 0;
    }

    private static int[] Copy(int[] source)
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

    private static (int X, int Y, int Home) Decode(int state)
    {
        var x = DecodeX(state);

        var y = DecodeY(state);

        var home = DecodeHome(state);

        return (x, y, home);
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
    private static void Dump(int[] state)
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

        for (var y = 1; y < _burrowDepth + 1; y++)
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

    private static char GetType(int home)
    {
        return (char) ('@' + (char) (DecodeHome(home) / 2));
    }
#endif
}