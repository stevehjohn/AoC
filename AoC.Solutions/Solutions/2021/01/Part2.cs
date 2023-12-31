using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2021._01;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var window = new List<int>();

        var increased = 0;

        foreach (var line in Input)
        {
            window.Add(int.Parse(line));

            if (window.Count > 4)
            {
                window.RemoveAt(0);
            }

            if (window.Count < 4)
            {
                continue;
            }

            if (window.Skip(1).Take(3).Sum() > window.Take(3).Sum())
            {
                increased++;
            }
        }

        return increased.ToString();
    }
}