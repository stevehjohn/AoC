using System.Numerics;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2017._25;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var startState = Input[0][^2] - 'A';

        var stepsLine = Input[1];
        
        var start = stepsLine.IndexOf("after ", StringComparison.Ordinal) + 6;
        
        var end = stepsLine.IndexOf(' ', start);
        
        var steps = int.Parse(stepsLine.AsSpan(start, end - start));

        var transitions = new Transition[26, 2];

        for (var i = 3; i < Input.Length; i += 10)
        {
            var state = Input[i][^2] - 'A';

            transitions[state, 0] = new Transition(Input[i + 2][^2] - '0', Input[i + 3][^3] == 'h' ? 1 : -1, Input[i + 4][^2] - 'A');

            transitions[state, 1] = new Transition(Input[i + 6][^2] - '0', Input[i + 7][^3] == 'h' ? 1 : -1, Input[i + 8][^2] - 'A');
        }

        var tape = new ulong[(2L * steps + 65) / 64];

        var position = steps;

        var stateIndex = startState;

        for (var i = 0; i < steps; i++)
        {
            var word = position >> 6;
            
            var bit = 1UL << (position & 63);

            var value = (tape[word] & bit) == 0 ? 0 : 1;

            var transition = transitions[stateIndex, value];

            if (transition.Write == 1)
            {
                tape[word] |= bit;
            }
            else
            {
                tape[word] &= ~bit;
            }

            position += transition.Direction;
            
            stateIndex = transition.NextState;
        }

        var checksum = 0;

        foreach (var word in tape)
        {
            checksum += BitOperations.PopCount(word);
        }

        return checksum.ToString();
    }
}
