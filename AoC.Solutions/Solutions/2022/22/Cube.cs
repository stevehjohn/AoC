using AoC.Solutions.Common;

namespace AoC.Solutions.Solutions._2022._22;

public class Cube
{
    private readonly CubeFace[] _faces = new CubeFace[6];

    private int _currentFace;

    private Point _facePosition;

    public Cube(List<(Point NetCoordinates, char[,] Face)> faces)
    {
        for (var i = 0; i < 6; i++)
        {
            AddFace(faces, i);
        }
    }
    
    private void AddFace(List<(Point NetCoordinates, char[,] Face)> faces, int index)
    {
        var face = new CubeFace(faces[index].Face);

        _faces[index] = face;
    }
}