using AoC.Solutions.Extensions;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._23;

[UsedImplicitly]
public class Part2 : Base
{
    private char[,] _map;

    private int _width;

    private int _height;
    
    public override string GetAnswer()
    {
        ParseInput();

        BuildGraph();
        
        return "Unknown";
    }

    private void BuildGraph()
    {
        var queue = new Queue<(int X, int Y)>();

        queue.Enqueue((1, 0));
        
        while (queue.TryDequeue(out var position))
        {
        }
    }

    private void ParseInput()
    {
        _map = Input.To2DArray();

        _width = _map.GetLength(0);

        _height = _map.GetLength(1);
    }
}