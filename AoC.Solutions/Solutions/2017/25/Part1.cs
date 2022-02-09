namespace AoC.Solutions.Solutions._2017._25;

public class Part1 : Base
{
    private readonly Dictionary<char, State> _states = new();

    public override string GetAnswer()
    {
        var parameters = ParseInput();

        return "TESTING";
    }

    private (char StartState, int Steps) ParseInput()
    {
        var startState = Input[0][^2];

        var split = Input[1].Split(' ', StringSplitOptions.TrimEntries);

        var steps = int.Parse(split[5]);

        for (var i = 3; i < Input.Length; i += 10)
        {
            var zeroAction = new Action(Input[i + 2][^2] - '0', Input[i + 3][^3] == 'h' ? 1 : -1, Input[i + 4][^2]);

            var oneAction = new Action(Input[i + 6][^2] - '0', Input[i + 7][^3] == 'h' ? 1 : -1, Input[i + 8][^2]);

            var state = new State(Input[i][^2], zeroAction, oneAction);

            _states.Add(state.Id, state);
        }

        return (startState, steps);
    }
}