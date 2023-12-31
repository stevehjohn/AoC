using AoC.Solutions.Exceptions;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2022._08;

public abstract class Base : Solution
{
    public override string Description => "Treetop tree house";

    protected char[,] Matrix;

    protected int Size;

    protected void ProcessInput()
    {
        if (Input.Length != Input[0].Length)
        {
            throw new PuzzleException("Input is not square.");
        }

        Size = Input.Length;

        Matrix = new char[Size, Size];

        for (var y = 0; y < Size; y++)
        {
            for (var x = 0; x < Size; x++)
            {
                Matrix[x, y] = Input[y][x];
            }
        }
    }
}