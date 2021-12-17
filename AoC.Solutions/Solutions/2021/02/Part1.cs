using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2021._02;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var x = 0;
        var y = 0;

        foreach (var line in Input)
        {
            var parts = line.Split(' ');

            var command = parts[0];

            var amount = int.Parse(parts[1]);

            switch (command.ToLower())
            {
                case "forward":
                    x += amount;
                    break;
                case "down":
                    y += amount;
                    break;
                case "up":
                    y -= amount;
                    break;
            }
        }

        return (x * y).ToString();
    }
}