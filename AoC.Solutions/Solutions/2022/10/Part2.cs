namespace AoC.Solutions.Solutions._2022._10;

public class Part2 : Base
{
    public override string GetAnswer()
    {
        var registerX = 1;

        var cycle = 1;

        var strength = 0;

        var column = 0;

        foreach (var line in Input)
        {
            switch (line[..4])
            {
                case "noop":
                    column++;

                    if (column == 40)
                    {
                        column = 0;

                        Console.WriteLine();
                    }

                    Console.Write(Math.Abs(registerX - column) < 2 ? "#" : ".");

                    cycle++;

                    break;

                case "addx":
                    column++;

                    if (column == 40)
                    {
                        column = 0;

                        Console.WriteLine();
                    }

                    cycle++;

                    Console.Write(Math.Abs(registerX - column) < 2 ? "#" : ".");

                    column++;

                    if (column == 40)
                    {
                        column = 0;

                        Console.WriteLine();
                    }

                    registerX += int.Parse(line[5..]);

                    Console.Write(Math.Abs(registerX - column) < 2 ? "#" : ".");
                    
                    cycle++;

                    break;
            }
        }

        return strength.ToString();
    }
}