using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._23;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var count = 0;

        for (var a = 0; a < Size; a++)
        {
            var neighbours = Neighbours[a];

            for (var i = 0; i < neighbours.Length; i++)
            {
                var b = neighbours[i];

                if (b <= a)
                {
                    continue;
                }

                for (var j = i + 1; j < neighbours.Length; j++)
                {
                    var c = neighbours[j];

                    if (! Adjacent[b * Size + c])
                    {
                        continue;
                    }

                    if (a / 26 == 19 || b / 26 == 19 || c / 26 == 19)
                    {
                        count++;
                    }
                }
            }
        }

        return count.ToString();
    }
}