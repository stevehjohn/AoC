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
        var state = "abcdefgh".ToCharArray();

        foreach (var line in Input)
        {
            var parts = line.Split(' ', StringSplitOptions.TrimEntries);

            int leftIdx;

            int rightIdx;

            switch (parts[0])
            {
                case "swap":
                    if (parts[1] == "position")
                    {
                        leftIdx = int.Parse(parts[2]);

                        rightIdx = int.Parse(parts[5]);

                        (state[leftIdx], state[rightIdx]) = (state[rightIdx], state[leftIdx]);
                    }
                    else
                    {
                        state = new string(state).Replace(parts[2][0], '-').Replace(parts[5][0], parts[2][0]).Replace('-', parts[5][0]).ToCharArray();
                    }

                    break;
                case "rotate":
                    if (parts[1] == "left")
                    {

                    }
                    else if (parts[1] == "right")
                    {

                    }
                    else
                    {

                    }

                    break;
                case "reverse":
                    break;
                case "move":
                    var remove = int.Parse(parts[2]);

                    var character = state[remove];

                    var temp = $"{new string(state[..remove])}{new string(state[(remove + 1)..])}";

                    var add = int.Parse(parts[5]);

                    state = $"{temp[..add]}{character}{temp[add..]}".ToCharArray();

                    break;
            }
        }

        return new string(state);
    }
}