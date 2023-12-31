using System.Text;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2016._02;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var x = 1;

        var y = 3;

        var builder = new StringBuilder();

        const string keypad = "..........1.....234...56789...ABC.....D..........";

        foreach (var line in Input)
        {
            foreach (var c in line)
            {
                switch (c)
                {
                    case 'U':
                        if (keypad[(y - 1) * 7 + x] != '.')
                        {
                            y--;
                        }

                        break;
                    case 'D':
                        if (keypad[(y + 1) * 7 + x] != '.')
                        {
                            y++;
                        }

                        break;
                    case 'L':
                        if (keypad[y * 7 + x - 1] != '.')
                        {
                            x--;
                        }

                        break;
                    case 'R':
                        if (keypad[y * 7 + x + 1] != '.')
                        {
                            x++;
                        }

                        break;
                }
            }

            builder.Append(keypad[y * 7 + x]);
        }

        return builder.ToString();
    }
}