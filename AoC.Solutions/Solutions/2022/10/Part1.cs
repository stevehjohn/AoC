namespace AoC.Solutions.Solutions._2022._10;

public class Part1 : Base
{
    public override string GetAnswer()
    {
        var registerX = 1;

        var cycle = 1;

        var strength = 0;

        var strengthTimer = 20;

        foreach (var line in Input)
        {
            switch (line[..4])
            {
                case "noop":
                    Console.WriteLine($"{cycle}: noop");

                    strengthTimer--;

                    if (strengthTimer == 0)
                    {
                        strength += cycle * registerX;

                        strengthTimer = 40;
                    }

                    cycle++;

                    break;

                case "addx":
                    Console.WriteLine($"{cycle}: addx");

                    strengthTimer--;
                    if (strengthTimer == 0)
                    {
                        strength += cycle * registerX;

                        strengthTimer = 40;
                    }

                    cycle++;

                    strengthTimer--;
                    if (strengthTimer == 0)
                    {
                        strength += cycle * registerX;

                        strengthTimer = 40;
                    }

                    Console.WriteLine($"{cycle}: {line[5..]}");

                    registerX += int.Parse(line[5..]);

                    cycle++;

                    break;
            }
        }

        return strength.ToString();
    }
}
