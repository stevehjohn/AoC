using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2020._23;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();
        
        ExpandInput();

        var cups = Cups;
        
        var current = Input[0][0] - '0';
        
        const int max = 1_000_000;

        for (var move = 0; move < 10_000_000; move++)
        {
            var first = cups[current];
            
            var second = cups[first];
            
            var third = cups[second];

            var destination = current - 1;

            if (destination == 0)
            {
                destination = max;
            }

            while (destination == first || destination == second || destination == third)
            {
                destination--;

                if (destination == 0)
                {
                    destination = max;
                }
            }

            cups[current] = cups[third];
            
            cups[third] = cups[destination];
            
            cups[destination] = first;

            current = cups[current];
        }

        var firstCup = cups[1];
        
        var secondCup = cups[firstCup];

        return ((long)firstCup * secondCup).ToString();
    }

    private void ExpandInput()
    {
        var input = new int[1_000_001];

        Buffer.BlockCopy(Cups, 0, input, 0, Cups.Length * sizeof(int));

        for (var i = Cups.Length; i < input.Length; i++)
        {
            input[i] = i + 1;
        }

        input[Input[0][^1] - '0'] = Cups.Length;
        
        input[1_000_000] = Input[0][0] - '0';

        Cups = input;
        
        Max = 1_000_000;
    }
}