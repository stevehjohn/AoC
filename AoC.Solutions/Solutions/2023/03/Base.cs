using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2023._03;

public abstract class Base : Solution
{
    public override string Description => "Gear Ratios";

    protected int Width;

    protected int Height;
    
    protected char GetChar(int x, int y)
    {
        if (x < 0 || x >= Width)
        {
            return '.';
        }

        if (y < 0 || y >= Height)
        {
            return '.';
        }

        if (char.IsNumber(Input[y][x]))
        {
            return '.';
        }

        return Input[y][x];
    }
}