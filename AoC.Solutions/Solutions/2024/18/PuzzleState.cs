namespace AoC.Solutions.Solutions._2024._18;

public class PuzzleState
{
    public const int Size = 73;
    
    private static char[,] _map;

    public PuzzleState(string[] input)
    {
        if (_map == null)
        {
            _map = new char[Size, Size];
            
            for (var y = 0; y < Size; y++)
            {
                for (var x = 0; x < Size; x++)
                {
                    if (x is 0 or Size - 1 || y is 0 or Size - 1)
                    {
                        _map[x, y] = '#';
                        
                        continue;
                    }

                    _map[x, y] = '.';
                }
            }
        }
    }
}