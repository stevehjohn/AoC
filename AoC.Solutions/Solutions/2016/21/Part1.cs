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
                        state = RotateLeft(state);
                    }
                    else if (parts[1] == "right")
                    {
                        state = RotateRight(state);
                    }
                    else
                    {

                    }

                    break;
                case "reverse":
                    break;
                case "move":
                    state = Move(state, int.Parse(parts[2]), int.Parse(parts[5]));

                    break;
            }
        }

        return new string(state);
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

    private static string RotateLeft(string state)
    {
        return $"{state[1..]}{state[0]}";
    }

    private static string RotateRight(string state)
    {
        return $"{state[..^1]}{state[^1]}";
    }

    private static string Move(string state, int removeAt, int insertAt)
    {
        var character = state[removeAt];

        state = $"{state[..removeAt]}{state[(removeAt + 1)..]}";

        state = $"{state[..insertAt]}{character}{state[insertAt..]}";

        return state;
    }
}