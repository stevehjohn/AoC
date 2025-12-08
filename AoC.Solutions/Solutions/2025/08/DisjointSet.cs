namespace AoC.Solutions.Solutions._2025._08;

public class DisjointSet<T> where T : notnull
{
    private readonly Dictionary<T, T> _parents = new();

    private readonly Dictionary<T, int> _sizes = new();

    public void Add(T item)
    {
        _parents[item] = item;

        _sizes[item] = 1;
    }

    public bool Union(T a, T b)
    {
        var ra = Find(a);
        
        var rb = Find(b);

        if (EqualityComparer<T>.Default.Equals(ra, rb))
        {
            return false;
        }

        if (_sizes[ra] < _sizes[rb])
        {
            (ra, rb) = (rb, ra);
        }

        _parents[rb] = ra;
        
        _sizes[ra] += _sizes[rb];

        return true;
    }
    
    public IEnumerable<int> GetSizes()
    {
        var map = new Dictionary<T, int>();

        foreach (var item in _parents.Keys)
        {
            var root = Find(item);

            if (! map.TryAdd(root, 1))
            {
                map[root]++;
            }
        }

        return map.Values;
    }

    private T Find(T x)
    {
        if (! _parents.TryGetValue(x, out var value))
        {
            Add(x);

            return x;
        }

        if (! EqualityComparer<T>.Default.Equals(value, x))
        {
            _parents[x] = Find(value);
        }

        return _parents[x];
    }
}