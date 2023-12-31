using JetBrains.Annotations;
using System.Text;

namespace AoC.Solutions.Solutions._2021._10;

[UsedImplicitly]
public class Part2 : Base
{
    private static readonly Dictionary<char, int> Scores = new()
                                                           {
                                                               { ')', 1 },
                                                               { ']', 2 },
                                                               { '}', 3 },
                                                               { '>', 4 }
                                                           };

    public override string GetAnswer()
    {
        var scores = new List<long>();

        foreach (var input in Input)
        {
            var score = 0L;

            var isCorrupt = IsCorrupt(input);

            if (isCorrupt == '\0')
            {
                var completion = GetCompletion(input);

                foreach (var c in completion)
                {
                    score *= 5;

                    score += Scores[c];
                }

                scores.Add(score);
            }
        }

        scores = scores.OrderBy(s => s).ToList();

        return scores[scores.Count / 2].ToString();
    }

    private static string GetCompletion(string input)
    {
        var stack = new Stack<char>();

        foreach (var c in input)
        {
            if (Pairs.Keys.Contains(c))
            {
                stack.Push(c);

                continue;
            }

            stack.Pop();
        }

        var completion = new StringBuilder();

        while (stack.Any())
        {
            completion.Append(Pairs[stack.Pop()]);
        }

        return completion.ToString();
    }
}