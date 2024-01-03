using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2016._03;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var count = 0;

        for (var i = 0; i < Input.Length; i += 3)
        {
            var triangles = new List<List<int>> { new(), new(), new() };

            for (var j = 0; j < 3; j++)
            {
                var sides = Input[i + j].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

                triangles[0].Add(sides[0]);

                triangles[1].Add(sides[1]);
                
                triangles[2].Add(sides[2]);
            }

            for (var j = 0; j < 3; j++)
            {
                var triangle = triangles[j].OrderBy(t => t).ToArray();

                if (triangle[0] + triangle[1] > triangle[2])
                {
                    count++;
                }
            }
        }

        return count.ToString();
    }
}