namespace AoC.Solutions.Solutions._2022._08;

public class Part1 : Base
{
    public override string GetAnswer()
    {
        var rows = new List<List<char>>();
        
        var cols =new List<List<char>>();

        foreach (var line in Input)
        {
            var row = line.ToCharArray().ToList();

            rows.Add(row);

            for (var x = 0; x < line.Length; x++)
            {
                List<char> col;

                if (cols.Count < x + 1)
                {
                    col = new List<char>();

                    cols.Add(col);
                }
                else
                {
                    col = cols[x];
                }

                col.Add(line[x]);
            }
        }

        var visible = Input.Length * 2 + (Input[0].Length - 2) * 2;

        foreach (var row in rows.Skip(1).Take(rows.Count - 2))
        {
            visible += CountVisible(row);

            row.Reverse();

            visible += CountVisible(row);
        }

        foreach (var col in cols.Skip(1).Take(cols.Count - 2))
        {
            visible += CountVisible(col);

            col.Reverse();

            visible += CountVisible(col);
        }

        return visible.ToString();
    }

    private static int CountVisible(List<char> list)
    {
        var visibleCount = 0;

        var max = list[0];

        for (var i = 1; i < list.Count - 2; i++)
        {
            var tree = list[i];

            if (tree > max)
            {
                visibleCount++;

                max = tree;
            }
        }

        return visibleCount;
    }
}