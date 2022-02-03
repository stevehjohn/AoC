using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2018._22;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var depth = int.Parse(Input[0][7..]);

        var parts = Input[1][8..].Split(',', StringSplitOptions.TrimEntries);

        var targetX = int.Parse(parts[0]);

        var targetY = int.Parse(parts[1]);

        var map = GenerateMap(depth, targetX, targetY);

        //Dump(map, targetX, targetY);

        var result = GetRiskLevel(map, targetX, targetY);

        return result.ToString();
    }

    private static object GetRiskLevel(char[,] map, int targetX, int targetY)
    {
        var riskLevel = 0;
        
        for (var y = 0; y < targetY + 1; y++)
        {
            for (var x = 0; x < targetX + 1; x++)
            {
                riskLevel += map[x, y] is '.' or 'M' or 'T' ? 0 : map[x, y] == '=' ? 1 : 2;
            }
        }

        return riskLevel;
    }

    private static char[,] GenerateMap(int depth, int targetX, int targetY)
    {
        var indexes = new int[targetX + 1, targetY + 1];

        var map = new char[targetX + 1, targetY + 1];

        map[0, 0] = 'M';

        map[targetX, targetY] = 'T';

        for (var y = 0; y < targetY + 1; y++)
        {
            for (var x = 0; x < targetX + 1; x++)
            {
                if (x == 0 && y == 0 || x == targetX && y == targetY)
                {
                    continue;
                }

                int index;

                if (y == 0)
                {
                    index = x * 16807;
                }
                else if (x == 0)
                {
                    index = y * 48271;
                }
                else
                {
                    index = indexes[x - 1, y] * indexes[x, y - 1];
                }

                var erosion = (index + depth) % 20183;

                indexes[x, y] = erosion;

                var type = erosion % 3;

                map[x, y] = type == 0 ? '.' : type == 1 ? '=' : '|';
            }
        }

        return map;
    }

    private void Dump(char[,] map, int targetX, int targetY)
    {
        for (var y = 0; y < targetY + 1; y++)
        {
            for (var x = 0; x < targetX + 1; x++)
            {
                Console.Write(map[x, y]);
            }

            Console.WriteLine();
        }
    }
}