using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._02;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var sum = 0;

        foreach (var line in Input)
        {
            sum += GetPower(line);
        }

        return sum.ToString();
    }

    private static int GetPower(string data)
    {
        data = data.Split(':', StringSplitOptions.TrimEntries)[1];

        var rounds = data.Split(';', StringSplitOptions.TrimEntries);

        var red = 0;

        var green = 0;

        var blue = 0;
        
        foreach (var round in rounds)
        {
            var draws = round.Split(',', StringSplitOptions.TrimEntries);

            foreach (var draw in draws)
            {
                var parts = draw.Split(' ', StringSplitOptions.TrimEntries);

                var number = int.Parse(parts[0]);

                switch (parts[1])
                {
                    case "red":
                        if (number > red)
                        {
                            red = number;
                        }
                        break;
                    
                    case "green":
                        if (number > green)
                        {
                            green = number;
                        }
                        break;
                    
                    case "blue":
                        if (number > blue)
                        {
                            blue = number;
                        }
                        break;
                }
            }
        }

        return red * green * blue;
    }
}