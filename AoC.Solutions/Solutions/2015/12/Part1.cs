using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2015._12;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var json = Input[0];

        var sum = 0;

        for (var i = 0; i < json.Length - 1; i++)
        {
            if (json[i] is '[' or ',' or ':' && (char.IsNumber(json[i + 1]) || json[i + 1] == '-'))
            {
                int n;

                for (n = i + 1; i < json.Length; n++)
                {
                    if (! (char.IsNumber(json[n]) || json[n] == '-'))
                    {
                        break;
                    }
                }

                sum += int.Parse(json[(i + 1)..n]);
            }
        }

        return sum.ToString();
    }
}