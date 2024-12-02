using AoC.Solutions.Common;
using AoC.Solutions.Exceptions;
using AoC.Solutions.Solutions._2019.Computer;
using JetBrains.Annotations;
using System.Text;

namespace AoC.Solutions.Solutions._2019._17;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        GetMap();

        ExpandMapByOne();

        var steps = GetSteps();

        var cpu = new Cpu();

        cpu.Initialise(65536);

        cpu.LoadProgram(Input);

        cpu.Memory[0] = 2;

        var stepsCopy = steps;

        var a = FindRepetition(stepsCopy);

        stepsCopy = stepsCopy.Replace(a, string.Empty);

        var b = FindRepetition(stepsCopy);

        stepsCopy = stepsCopy.Replace(b, string.Empty);

        b = b[1..];

        var c = FindRepetition(stepsCopy);

        c = c[2..];

        var userInput = steps.Replace(a, "A").Replace(b, "B").Replace(c, "C");

        userInput = $"{userInput}\n{a}\n{b}\n{c}\nn\n";

        foreach (var chr in Encoding.ASCII.GetBytes(userInput))
        {
            cpu.UserInput.Enqueue(chr);
        }

        cpu.Run();

        return cpu.UserOutput.Last().ToString();
    }

    private static string FindRepetition(string input)
    {
        var length = 2;

        var last = input[..length];

        // ReSharper disable once StringIndexOfIsCultureSpecific.1
        while (input[1..].IndexOf(last) > -1)
        {
            length++; 
            
            last = input[..length];
        }

        while (! char.IsNumber(last[^1]))
        {
            last = last[..^1];
        }

        return last;
    }

    private string GetSteps()
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

                commands.Append($",{turn.Value.Command}");

                bot.Direction = turn.Value.Direction;
            }

            steps++;

            bot.Position.X += bot.Direction.X;

            bot.Position.Y += bot.Direction.Y;
        }

        return commands.ToString()[1..];
    }

    private (Point Direction, char Command)? TryTurn((Point Position, Point Direction) bot)
    {
        if (bot.Direction.X == 0)
        {
            if (Map[bot.Position.X + 1, bot.Position.Y] == '#')
            {
                return (new Point(1, 0), bot.Direction.Y == -1 ? 'R' : 'L');
            }

            if (Map[bot.Position.X - 1, bot.Position.Y] == '#')
            {
                return (new Point(-1, 0), bot.Direction.Y == -1 ? 'L' : 'R');
            }
        }

        if (bot.Direction.Y == 0)
        {
            if (Map[bot.Position.X, bot.Position.Y + 1] == '#')
            {
                return (new Point(0, 1), bot.Direction.X == -1 ? 'L' : 'R');
            }

            if (Map[bot.Position.X, bot.Position.Y - 1] == '#')
            {
                return (new Point(0, -1), bot.Direction.X == -1 ? 'R' : 'L');
            }
        }

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

    private void ExpandMapByOne()
    {
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
    }
}