using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._02;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var sum = 0;

        var game = 1;
        
        foreach (var line in Input)
        {
            if (IsPossible(line, 12, 13, 14))
            {
                sum += game;
            }

            game++;
        }

        return sum.ToString();
    }

    private static bool IsPossible(string data, int red, int green, int blue)
    {
        data = data.Split(':', StringSplitOptions.TrimEntries)[1];

        var rounds = data.Split(';', StringSplitOptions.TrimEntries);

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
                            return false;
                        }
                        break;
                    
                    case "green":
                        if (number > green)
                        {
                            return false;
                        }
                        break;
                    
                    case "blue":
                        if (number > blue)
                        {
                            return false;
                        }
                        break;
                }
            }
        }

        return true;
    }
}