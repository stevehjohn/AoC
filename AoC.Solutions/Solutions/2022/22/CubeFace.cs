namespace AoC.Solutions.Solutions._2022._22;

public class CubeFace
{
    public char[,] Face { get; }

    public CubeFace Right { get; set; }

    public CubeFace Down { get; set; }

    public CubeFace Left { get; set; }

    public CubeFace Up { get; set; }

    public CubeFace(char[,] face)
    {
        Face = face;
    }
}