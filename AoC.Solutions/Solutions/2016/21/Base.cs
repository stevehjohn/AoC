using AoC.Solutions.Extensions;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2016._21;

public abstract class Base : Solution
{
    public override string Description => "Scrambled letters and hash";

    protected static string Solve(string state, string[] instructions, bool reverse = false)
    {
        foreach (var line in instructions)
        {
            var parts = line.Split(' ', StringSplitOptions.TrimEntries);

            switch (parts[0])
            {
                case "swap":
                    state = parts[1] == "position" ? SwapPosition(state, int.Parse(parts[2]), int.Parse(parts[5])) : SwapLetter(state, parts[2][0], parts[5][0]);

                    break;
                case "rotate":
                    switch (parts[1])
                    {
                        case "left" when reverse:
                            state = RotateRight(state, int.Parse(parts[2]));
                            break;
                        case "left":
                        case "right" when reverse:
                            state = RotateLeft(state, int.Parse(parts[2]));
                            break;
                        case "right":
                            state = RotateRight(state, int.Parse(parts[2]));
                            break;
                        default:
                            state = RotateByIndexOf(state, parts[6][0], reverse);
                            break;
                    }

                    break;
                case "reverse":
                    state = Reverse(state, int.Parse(parts[2]), int.Parse(parts[4]));

                    break;
                case "move":
                    state = reverse ? Move(state, int.Parse(parts[5]), int.Parse(parts[2])) : Move(state, int.Parse(parts[2]), int.Parse(parts[5]));

                    break;
            }
        }

        return new string(state);
    }

    private static string Reverse(string state, int startIndex, int endIndex)
    {
        return $"{state[..startIndex]}{string.Join(string.Empty, state[startIndex..(endIndex + 1)].Reverse())}{state[(endIndex + 1)..]}";
    }

    private static string SwapLetter(string state, char a, char b)
    {
        return state.Replace(a, '-').Replace(b, a).Replace('-', b);
    }

    private static string SwapPosition(string state, int a, int b)
    {
        var character = state[a];

        state = $"{state[..a]}{state[b]}{state[(a + 1)..]}";

        state = $"{state[..b]}{character}{state[(b + 1)..]}";

        return state;
    }

    private static string RotateByIndexOf(string state, char letter, bool reverse)
    {
        var index = state.IndexOf(letter);

        if (reverse)
        {
            index = new[] { 1, 1, 6, 2, 7, 3, 0, 4 }[index];

            state = RotateLeft(state, index);
        }
        else
        {
            if (index >= 4)
            {
                index++;
            }

            index++;

            state = RotateRight(state, index);
        }

        return state;
    }

    private static string RotateLeft(string state, int steps)
    {
        steps.Repetitions(() => state = $"{state[1..]}{state[0]}");

        return state;
    }

    private static string RotateRight(string state, int steps)
    {
        steps.Repetitions(() => state = $"{state[^1]}{state[..^1]}");

        return state;
    }

    private static string Move(string state, int removeAt, int insertAt)
    {
        var character = state[removeAt];

        state = $"{state[..removeAt]}{state[(removeAt + 1)..]}";

        state = $"{state[..insertAt]}{character}{state[insertAt..]}";

        return state;
    }
}