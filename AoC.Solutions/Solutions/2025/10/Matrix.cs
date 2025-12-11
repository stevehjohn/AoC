namespace AoC.Solutions.Solutions._2025._10;

public sealed class Matrix
{
    public int[] Rows { get; }
    
    public int[] Totals { get; }

    public Matrix(int[] rows, int[] totals)
    {
        Rows = rows;
        
        Totals = totals;
    }
}
