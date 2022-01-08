using System.Text;
using AoC.Solutions.Common;
using AoC.Solutions.Exceptions;
using JetBrains.Annotations;
using Microsoft.VisualBasic;

namespace AoC.Solutions.Solutions._2019._17;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        GetMap();

        var newMap = new char[Width + 2, Height + 2];

        for (var y = 0; y < Height + 2; y++)
        {
            for (var x = 0; x < Width + 2; x++)
            {
                if (x == 0 || y == 0 || x == Width + 1 || y == Height + 1)
                {
                    newMap[x, y] = '.';

                    continue;
                }

                newMap[x, y] = Map[x - 1, y - 1];
            }
        }

        Map = newMap;

        GetSteps();

        return "TESTING";
    }

    private void GetSteps()
    {
        var bot = GetBot();

        var commands = new StringBuilder();

        var steps = 0;

        while (true)
        {
            if (Map[bot.Position.X + bot.Direction.X, bot.Position.Y + bot.Direction.Y] != '#')
            {
                if (steps > 0)
                {
                    commands.Append($",{steps}");
                }

                steps = 0;

                var turn = TryTurn(bot);

                if (turn == null)
                {
                    break;
                }
            }

            steps++;

            bot.Position.X += bot.Direction.X;

            bot.Position.Y += bot.Direction.Y;
        }
    }

    private (Point Direction, char Command)? TryTurn((Point Position, Point Direction) bot)
    {
        return null;
    }

    private (Point Position, Point Direction) GetBot()
    {
        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                var c = Map[x, y];

                if (c != '#' && c != '.')
                {
                    var direction = c switch
                    {
                        '^' => new Point(0, -1),
                        'v' => new Point(0, 1),
                        '<' => new Point(-1, 0),
                        '>' => new Point(1, 0),
                        _ => throw new PuzzleException("Bot orientation not understood.")
                    };

                    return (new Point(x, y), direction);
                }
            }
        }

        throw new PuzzleException("Bot not found.");
    }
}