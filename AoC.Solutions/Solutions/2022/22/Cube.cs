using AoC.Solutions.Common;

namespace AoC.Solutions.Solutions._2022._22;

public class Cube
{
    private static readonly char[,] Relationships = new char[4, 4];

    private readonly (char Element, Point MapPosition)[,,] _cube = new (char, Point)[Constants.FaceSize, Constants.FaceSize, Constants.FaceSize];

    public Cube BuildFromInput(string[] input)
    {
        InitialiseRelationships();

        var arrangement = GetArrangement(input);

        BuildCube(arrangement);

        return null;
    }
    
    private static void InitialiseRelationships()
    {
        const string relationships = "URDLFRBLDRULBRFL";

        for (var i = 0; i < relationships.Length; i++)
        {
            Relationships[i % 4, i / 4] = relationships[i];
        }
    }

    private static List<(Point MapStart, char Face)> GetArrangement(string[] input)
    {
        var arrangement = new List<(Point MapStart, char Face)>();

        for (var y = 0; y < 4 * Constants.FaceSize; y += Constants.FaceSize)
        {
            if (y >= input.Length)
            {
                return arrangement;
            }

            for (var x = 0; x < 4 * Constants.FaceSize; x += Constants.FaceSize)
            {
                if (x >= input[y].Length)
                {
                    continue;
                }

                if (input[y][x] != ' ')
                {
                    var position = new Point(x, y);

                    var face = Relationships[x / Constants.FaceSize, y / Constants.FaceSize];

                    arrangement.Add((position, face));
                    Console.Write(Relationships[x / Constants.FaceSize, y / Constants.FaceSize]);
                }
                else
                {
                    Console.Write(' ');
                }
            }

            Console.WriteLine();
        }

        return arrangement;
    }

    private void BuildCube(List<(Point MapStart, char Face)> arrangement)
    {
    }
}