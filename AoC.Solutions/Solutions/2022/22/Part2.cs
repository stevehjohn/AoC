using AoC.Solutions.Common;

namespace AoC.Solutions.Solutions._2022._22;

public class Part2 : Base
{
    public override string GetAnswer()
    {
        GetFaces();

        return "";
    }

    private void GetFaces()
    {
        var faces = new List<(Point NetCoordinates, char[,] Face)>();

        var y = 0;

        while (y < Input.Length)
        {
            var line = Input[y];

            var x = 0;

            while (x < line.Length)
            {
                if (line[x] == ' ')
                {
                    x += Constants.FaceSize;

                    continue;
                }

                var face = GetFace(x, y);

                faces.Add((new Point(x / Constants.FaceSize, y / Constants.FaceSize), face));

                x += Constants.FaceSize;
            }

            y += Constants.FaceSize;
        }

        var cube = new Cube(faces);
    }

    private char[,] GetFace(int x, int y)
    {
        var face = new char[Constants.FaceSize, Constants.FaceSize];

        for (var iY = 0; iY < Constants.FaceSize; iY++)
        {
            var line = Input[y + iY];

            for (var iX = 0; iX < Constants.FaceSize; iX++)
            {
                face[iX, iY] = line[x + iX];
            }
        }

        return face;
    }
}