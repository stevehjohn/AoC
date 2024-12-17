using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._17;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var a = (long) Math.Pow(8, 15);

        var result = string.Empty;
        
        while (result == string.Empty)
        {
            result = RunProgram(a);

            if (a % 1000000 == 0)
            {
                Console.Write(a);

                Console.CursorLeft = 0;
            }

            a++;
        }
        
        Console.WriteLine(result);

        return (a - 1).ToString();
    }
}