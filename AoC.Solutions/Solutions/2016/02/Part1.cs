using System.Text;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2016._02;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var x = 1;

        var y = 1;

        var builder = new StringBuilder();

        foreach (var line in Input)
        {
            foreach (var c in line)
            {
                switch (c)
                {
                    case 'U':
                        if (y > 0)
                        {
                            y--;
                        }

                        break;
                    case 'D':
                        if (y < 2)
                        {
                            y++;
                        }

                        break;
                    case 'L':
                        if (x > 0)
                        {
                            x--;
                        }

                        break;
                    case 'R':
                        if (x < 2)
                        {
                            x++;
                        }

                        break;
                }
            }

            builder.Append(y * 3 + x + 1);
        }

        return builder.ToString();
    }
}