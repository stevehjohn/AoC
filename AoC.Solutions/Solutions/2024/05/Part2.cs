using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._05;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var result = 0;

        for (var i = 0; i < Updates.Count; i++)
        {
            var update = Updates[i];

            if (! IsCorrect(update))
            {
                result += GetOrderedMiddle(update);
            }
        }

        return result.ToString();
    }

    private int GetOrderedMiddle(int[] update)
    {
        var swapped = true;

        while (swapped)
        {
            swapped = false;
                    
            foreach (var rule in Rules)
            {
                var left = rule.Key;

                foreach (var right in rule.Value)
                {
                    for (var i = 0; i < update.Length - 1; i++)
                    {
                        var leftIndex = Array.IndexOf(update, left);

                        var rightIndex = Array.IndexOf(update, right);

                        if (rightIndex == -1)
                        {
                            continue;
                        }

                        if (leftIndex > rightIndex)
                        {
                            (update[leftIndex], update[rightIndex]) = (update[rightIndex], update[leftIndex]);

                            swapped = true;
                            
                            break;
                        }
                    }
                }
            }
        }

        return update[update.Length / 2];
    }
}