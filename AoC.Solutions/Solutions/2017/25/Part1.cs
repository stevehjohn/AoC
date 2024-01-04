using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2017._25;

[UsedImplicitly]
public class Part1 : Base
{
    private readonly Dictionary<char, State> _states = new();

    private readonly HashSet<int> _ones = new();

    private int _position;

    public override string GetAnswer()
    {
        var parameters = ParseInput();

        var state = parameters.StartState;

        for (var i = 0; i < parameters.Steps; i++)
        {
            state = RunState(state);
        }

        return _ones.Count.ToString();
    }

    private char RunState(char state)
    {
        var stateDefinition = _states[state];

        var action = _ones.Contains(_position) ? stateDefinition.OneAction : stateDefinition.ZeroAction;

        if (action.Write == 1)
        {
            _ones.Add(_position);
        }
        else
        {
            _ones.Remove(_position);
        }

        _position += action.Direction;

        return action.NextState;
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