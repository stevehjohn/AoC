using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2023._12;

public abstract class Base : Solution
{
    public override string Description => "Hot springs";

    protected static (string Row, int[] Groups) ParseLine(string line)
    {
        var parts = line.Split(' ');

        var right = parts[1].Split(',');

        var groups = new int[right.Length];

        for (var i = 0; i < right.Length; i++)
        {
            var value = int.Parse(right[i]);

            groups[i] = value;
        }

        return (parts[0], groups);
    }

    protected long GetArrangements(string row, int[] groups)
    {
        row = $".{row}.";
        var damaged = DamagedList(groups).ToArray();

        var table = new long[row.Length + 1, damaged.Length + 1];
        table[table.GetUpperBound(0), table.GetUpperBound(1)] = 1;

        for (var c = row.Length - 1; c >= 0; c--)
        {
            for (var d = damaged.Length - 1; d >= 0; d--)
            {
                table[c, d] = (row[c] != '.', row[c] != '#', damaged[d]) switch
                {
                    (true, _, true) => table[c + 1, d + 1],
                    (_, true, false) => table[c + 1, d + 1] + table[c + 1, d],
                    _ => 0
                };
            }
        }

        return table[0, 0];
    }

    private IEnumerable<bool> DamagedList(int[] lengths)
    {
        yield return false;

        foreach (var length in lengths)
        {
            for (var i = 0; i < length; i++)
            {
                yield return true;
            }

            yield return false;
        }
    }
}