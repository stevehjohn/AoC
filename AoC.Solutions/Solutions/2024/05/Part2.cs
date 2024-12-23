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

        var indices = new int[100];
        
        Array.Fill(indices, -1);

        for (var i = 0; i < update.Length; i++)
        {
            indices[update[i]] = i;
        }

        while (swapped)
        {
            swapped = false;
                    
            foreach (var (left, value) in Rules)
            {
                if (indices[left] == -1)
                {
                    continue;
                }

                var leftIndex = indices[left];

                foreach (var right in value)
                {
                    var rightIndex = indices[right];

                    if (rightIndex == -1)
                    {
                        continue;
                    }

                    if (leftIndex > rightIndex)
                    {
                        (update[leftIndex], update[rightIndex]) = (update[rightIndex], update[leftIndex]);

                        indices[left] = rightIndex;

                        indices[right] = leftIndex;

                        swapped = true;
                        
                        break;
                    }
                }
            }
        }

        return update[update.Length / 2];
    }
}