namespace AoC.Solutions.Solutions._2021._04;

public class Board
{
    private readonly int[] _board;
    private readonly bool[] _hits;

    public Board(int[] data)
    {
        _board = data;

        _hits = new bool[25];
    }

    public bool CheckDraw(int draw)
    {
        for (var i = 0; i < _board.Length; i++)
        {
            if (_board[i] == draw)
            {
                _hits[i] = true;
                
                break;
            }
        }

        for (var y = 0; y < 5; y++)
        {
            if (_hits.Skip(5 * y).Take(5).All(h => h))
            {
                return true;
            }
        }

        for (var x = 0; x < 5; x++)
        {
            if (_hits[x] && _hits[5 + x] && _hits[10 + x] && _hits[15 + x] && _hits[20 + x])
            {
                return true;
            }
        }

        return false;
    }

    public List<int> GetUnmarked()
    {
        var result = new List<int>();

        for (var i = 0; i < _hits.Length; i++)
        {
            if (! _hits[i])
            {
                result.Add(_board[i]);
            }
        }

        return result;
    }
}