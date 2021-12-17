using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2021._02;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var x = 0;
        var y = 0;
        var a = 0;

        foreach (var line in Input)
        {
            var parts = line.Split(' ');

            var command = parts[0];

            var amount = int.Parse(parts[1]);

            switch (command.ToLower())
            {
                case "forward":
                    x += amount;
                    y += a * amount;
                    break;
                case "down":
                    a += amount;
                    break;
                case "up":
                    a -= amount;
                    break;
            }
        }

        return (x * y).ToString();
    }
}