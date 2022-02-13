using System.Security.Cryptography;
using System.Text;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2016._14;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var salt = Input[0];

        var i = 0;

        var found = 0;

        var queued = new List<(char Character, int Index)>();

        while (true)
        {
            var hash = MD5.HashData(Encoding.ASCII.GetBytes($"{salt}{i}"));

            var hex = Convert.ToHexString(hash);

            var character = GetTripleRepeatedCharacter(hex);

            if (character != '\0')
            {
                queued.Add((character, i));
            }

            character = GetQuintupleRepeatedCharacter(Convert.ToHexString(hash));

            if (character != '\0')
            {
                var matches = queued.Count(q => q.Character == character && q.Index >= i - 1_000);

                if (matches > 0)
                {
                    found += matches;

                    queued.RemoveAll(q => q.Character == character && q.Index >= i - 1_000);

                    if (found >= 64)
                    {
                        break;
                    }
                }
            }

            i++;
        }

        return i.ToString();
    }

    private static char GetTripleRepeatedCharacter(string hex)
    {
        for (var i = 0; i < hex.Length - 2; i++)
        {
            if (hex[i] == hex[i + 1] && hex[i] == hex[i + 2])
            {
                return hex[i];
            }
        }

        return '\0';
    }

    private static char GetQuintupleRepeatedCharacter(string hex)
    {
        for (var i = 0; i < hex.Length - 4; i++)
        {
            if (hex[i] == hex[i + 1] && hex[i] == hex[i + 2] && hex[i] == hex[i + 3] && hex[i] == hex[i + 4])
            {
                return hex[i];
            }
        }

        return '\0';
    }
}