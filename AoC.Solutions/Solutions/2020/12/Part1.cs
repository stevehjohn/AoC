using AoC.Solutions.Common;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2020._12;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var direction = 90;

        var position = new Point();

        foreach (var line in Input)
        {
            var value = int.Parse(line[1..]);

            switch (line[0])
            {
                case 'N':
                    position.Y -= value;

                    break;

                case 'S':
                    position.Y += value;

                    break;

                case 'E':
                    position.X += value;

                    break;

                case 'W':
                    position.X -= value;

                    break;

                case 'L':
                    direction -= value;

                    if (direction < 0)
                    {
                        direction += 360;
                    }

                    break;

                case 'R':
                    direction += value;

                    if (direction >= 360)
                    {
                        direction -= 360;
                    }

                    break;

                default:
                    switch (direction)
                    {
                        case 90:
                            position.X += value;
                            break;
                        
                        case 180:
                            position.Y += value;
                            break;

                        case 270:
                            position.X -= value;
                            break;
                        
                        default:
                            position.Y -= value;
                            break;
                    }

                    break;
            }
        }

        return (Math.Abs(position.X) + Math.Abs(position.Y)).ToString();
    }
}