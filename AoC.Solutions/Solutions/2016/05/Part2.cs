using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2016._05;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var password = new char[8];

        var found = 0;

        foreach (var match in GetMatches())
        {
            var position = match.Sixth;

            if (position > 7 || password[position] != '\0')
            {
                continue;
            }

            password[position] = ToHex(match.Seventh);

            found++;

            if (found == 8)
            {
                return new string(password);
            }
        }

        throw new InvalidOperationException();
    }
    
}