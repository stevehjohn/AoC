using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2015._13;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        foreach (var person in People)
        {
            Happiness.Add($"{person}Stevo", 0);

            Happiness.Add($"Stevo{person}", 0);
        }

        People = People.Append("Stevo").ToArray();

        var result = Solve();

        return result.ToString();
    }
}