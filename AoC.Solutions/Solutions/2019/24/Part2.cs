//using AoC.Solutions.Common;
//using JetBrains.Annotations;

//namespace AoC.Solutions.Solutions._2019._24;

//[UsedImplicitly]
//public class Part2 : Base
//{
//    private readonly List<bool[,]> _grids = new();

//    private readonly List<Point> _dies = new();

//    private readonly List<Point> _infests = new();

//    public override string GetAnswer()
//    {
//        _grids.Add(ParseInput());

//        for (var i = 0; i < 10; i++)
//        {
//            PlayRound();
//        }

//        Dump();

//        return "TESTING";
//    }

//    private void Dump()
//    {
//        foreach (var grid in _grids)
//        {
//            for (var y = 1; y < 6; y++)
//            {
//                for (var x = 1; x < 6; x++)
//                {
//                    Console.Write(grid[x, y] ? '#' : ' ');
//                }

//                Console.WriteLine();
//            }

//            Console.WriteLine();
//        }
//    }

//    private void PlayRound()
//    {
//        for (var level = 0; level < _grids.Count; level++)
//        {
//            PlayGrid(level);
//        }

//        foreach (var item in _dies)
//        {
//            _grids[item.Z][item.X, item.Y] = false;
//        }

//        _dies.Clear();

//        if (_infests.Max(i => i.Z) >= _grids.Count)
//        {
//            _grids.Add(new bool[7, 7]);
//        }

//        var zOffset = 0;

//        if (_infests.Min(i => i.Z) < 0)
//        {
//            _grids.Insert(0, new bool[7, 7]);

//            zOffset = 1;
//        }

//        foreach (var item in _infests)
//        {
//            _grids[item.Z + zOffset][item.X, item.Y] = true;
//        }

//        _infests.Clear();

//        var empty = new List<int>();

//        var i = 0;

//        foreach (var grid in _grids)
//        {
//            var isEmpty = true;

//            for (var y = 1; y < 6; y++)
//            {
//                for (var x = 1; x < 6; x++)
//                {
//                    if (grid[x, y])
//                    {
//                        isEmpty = false;
//                    }
//                }
//            }

//            if (isEmpty)
//            {
//                empty.Add(i);
//            }

//            i++;
//        }

//        foreach (var index in empty)
//        {
//            _grids.RemoveAt(index);
//        }
//    }

//    private void PlayGrid(int level)
//    {
//        var grid = _grids[level];

//        for (var y = 1; y < 6; y++)
//        {
//            for (var x = 1; x < 6; x++)
//            {
//                if (x == 3 && y == 3)
//                {
//                    continue;
//                }

//                var adjacent = GetAdjacentCount(level, x, y);

//                if (grid[x, y] && adjacent != 1)
//                {
//                    _dies.Add(new Point(x, y, level));
//                }

//                if (! grid[x, y] && (adjacent == 1 || adjacent == 2))
//                {
//                    _infests.Add(new Point(x, y, level));
//                }
//            }
//        }
//    }

//    private int GetAdjacentCount(int level, int x, int y)
//    {
//        var count = 0;

//        if (x == 1)
//        {
//            count += level < _grids.Count - 1 && _grids[level + 1][2, 3] ? 1 : 0;
//        }

//        if (x == 5)
//        {
//            count += level < _grids.Count - 1 && _grids[level + 1][4, 3] ? 1 : 0;
//        }

//        if (y == 1)
//        {
//            count += level < _grids.Count - 1 && _grids[level + 1][3, 2] ? 1 : 0;
//        }

//        if (y == 5)
//        {
//            count += level < _grids.Count - 1 && _grids[level + 1][3, 4] ? 1 : 0;
//        }

//        if (x > 1 && x < 5 && y > 1 && y < 5)
//        {
//            count += level > 0 ? 1 : 0;
//        }

//        count += _grids[level][x - 1, y] ? 1 : 0;

//        count += _grids[level][x + 1, y] ? 1 : 0;

//        count += _grids[level][x, y - 1] ? 1 : 0;

//        count += _grids[level][x, y + 1] ? 1 : 0;

//        return count;
//    }
//}