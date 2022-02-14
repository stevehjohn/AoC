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

            int leftIdx;

            int rightIdx;

            switch (parts[0])
            {
                case "swap":
                    //if (parts[1] == "position")
                    //{
                    //    leftIdx = int.Parse(parts[2]);

                    //    rightIdx = int.Parse(parts[5]);

                    //    (state[leftIdx], state[rightIdx]) = (state[rightIdx], state[leftIdx]);
                    //}
                    //else
                    //{
                    //    state = new string(state).Replace(parts[2][0], '-').Replace(parts[5][0], parts[2][0]).Replace('-', parts[5][0]).ToCharArray();
                    //}

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

    //private static string SwapPosition(string state, int a, int b)
    //{
    //    var character = state[a];

    //    state = $"{state[..a]}{state[(a + 1)..]}";


    //}

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