using System.Text;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2018._15;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        for (var i = 1; i < 8; i++)
        {
            var nameSpace = GetType().Namespace ?? string.Empty;

            var parts = nameSpace.Split('.');

            var path = parts.Skip(2).Select(s => s.Replace("_", string.Empty)).ToArray();

            Input = File.ReadAllLines($"{string.Join(Path.DirectorySeparatorChar, path)}{Path.DirectorySeparatorChar}input{i}.txt");

            ParseInput();

            var result = Play();

            Dump();

            foreach (var unit in _units)
            {
                Console.WriteLine($"{(unit.Type == Type.Elf ? 'E' : 'G')}: {unit.Health}    ");
            }

            switch (i)
            {
                case 1:
                    Console.ForegroundColor = result == 27730 ? ConsoleColor.Yellow : ConsoleColor.Red;
                    break;
                case 2:
                    Console.ForegroundColor = result == 36334 ? ConsoleColor.Yellow : ConsoleColor.Red;
                    break;
                case 3:
                    Console.ForegroundColor = result == 39514 ? ConsoleColor.Yellow : ConsoleColor.Red;
                    break;
                case 4:
                    Console.ForegroundColor = result == 27755 ? ConsoleColor.Yellow : ConsoleColor.Red;
                    break;
                case 5:
                    Console.ForegroundColor = result == 28944 ? ConsoleColor.Yellow : ConsoleColor.Red;
                    break;
                case 6:
                    Console.ForegroundColor = result == 18740 ? ConsoleColor.Yellow : ConsoleColor.Red;
                    break;
                case 7:
                    Console.ForegroundColor = result == 243390 ? ConsoleColor.Yellow : ConsoleColor.Red;
                    break;
            }

            Console.WriteLine(result);

            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine();

            _units.Clear();
        }

        return "TESTING";

        //return result.ToString();
    }

    private void Dump()
    {
        for (var y = 0; y < _height; y++)
        {
            var builder = new StringBuilder();

            for (var x = 0; x < _width; x++)
            {
                var unit = _units.SingleOrDefault(u => u.Position.X == x && u.Position.Y == y);

                if (unit != null)
                {
                    builder.Append(unit.Type == Type.Elf ? 'E' : 'G');

                    continue;
                }

                builder.Append(_map[x, y] ? '#' : ' ');
            }

            Console.WriteLine(builder.ToString());
        }
    }
}