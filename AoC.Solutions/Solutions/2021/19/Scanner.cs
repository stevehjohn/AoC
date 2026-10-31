namespace AoC.Solutions.Solutions._2021._19;

public abstract partial class Base
{
    private sealed class Scan
    {
        public required Vector[] Local { get; init; }

        public required Vector[][] Rotated { get; init; }

        public required Dictionary<int, int> Distances { get; init; }

        public Vector[] Global { get; set; }

        public Vector Position { get; set; }
    }
}