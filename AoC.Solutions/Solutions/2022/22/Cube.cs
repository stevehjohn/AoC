namespace AoC.Solutions.Solutions._2022._22;

public class Cube
{
    private static char[,] Relationships = new char[4, 4];

    private readonly char[,,] _cube = new char[Constants.FaceSize, Constants.FaceSize, Constants.FaceSize];

    public static Cube BuildFromInput(string[] input)
    {
        InitialiseRelationships();

        GetArrangement(input);

        return null;
    }

    private static void GetArrangement(string[] input)
    {
        for (var y = 0; y < 4 * Constants.FaceSize; y += Constants.FaceSize)
        {
            if (y >= input.Length)
            {
                return;
            }

            for (var x = 0; x < 4 * Constants.FaceSize; x += Constants.FaceSize)
            {
                if (x >= input[y].Length)
                {
                    continue;
                }

                if (input[y][x] != ' ')
                {
                    Console.Write(Relationships[x / Constants.FaceSize, y / Constants.FaceSize]);
                }
                else
                {
                    Console.Write(' ');
                }
            }

            Console.WriteLine();
        }
    }

    private static void InitialiseRelationships()
    {
        var relationships = "URDLFRBLDRULBRFL";

        for (var i = 0; i < relationships.Length; i++)
        {
            Relationships[i % 4, i / 4] = relationships[i];
        }
    }
}