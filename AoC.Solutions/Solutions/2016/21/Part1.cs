using AoC.Solutions.Extensions;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2016._21;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var endState = Solve();

        return endState;
    }

    private string Solve()
    {
        // ReSharper disable once StringLiteralTypo
        var state = "abcdefgh";

        foreach (var line in Input)
        {
            var parts = line.Split(' ', StringSplitOptions.TrimEntries);

            switch (parts[0])
            {
                case "swap":
                    if (parts[1] == "position")
                    {
                        state = SwapPosition(state, int.Parse(parts[2]), int.Parse(parts[5]));
                    }
                    else
                    {
                        state = SwapLetter(state, parts[2][0], parts[5][0]);
                    }

                    break;
                case "rotate":
                    if (parts[1] == "left")
                    {
                        state = RotateLeft(state, int.Parse(parts[2]));
                    }
                    else if (parts[1] == "right")
                    {
                        state = RotateRight(state, int.Parse(parts[2]));
                    }
                    else
                    {
                        state = RotateByIndexOf(state, parts[6][0]);
                    }

                    break;
                case "reverse":
                    state = Reverse(state, int.Parse(parts[2]), int.Parse(parts[4]));

                    break;
                case "move":
                    state = Move(state, int.Parse(parts[2]), int.Parse(parts[5]));

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

    private static string RotateByIndexOf(string state, char letter)
    {
        var index = state.IndexOf(letter);

        state = RotateRight(state, index + 1);

        if (index >= 4)
        {
            state = RotateRight(state, 1);
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