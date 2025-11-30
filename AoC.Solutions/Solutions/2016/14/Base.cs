using System.Security.Cryptography;
using System.Text;
using AoC.Solutions.Extensions;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2016._14;

public abstract class Base : Solution
{
    public override string Description => "One time pad";

    protected int RunHashes(int additionalHashes = 0)
    {
        var salt = Input[0];

        var i = 0;

        var found = 0;

        var queued = new Dictionary<char, List<int>>();

        const string characters = "0123456789ABCDEF";

        for (var c = 0; c < characters.Length; c++)
        {
            queued.Add(characters[c], []);
        }

        while (true)
        {
            var hash = MD5.HashData(Encoding.ASCII.GetBytes($"{salt}{i}"));

            additionalHashes.Repetitions(() => hash = MD5.HashData(Encoding.ASCII.GetBytes(Convert.ToHexString(hash).ToLower())));

            var hex = Convert.ToHexString(hash);

            var character = GetTripleRepeatedCharacter(hex);

            if (character != '\0')
            {
                queued[character].Add(i);
            }

            character = GetQuintupleRepeatedCharacter(hex);

            if (character != '\0')
            {
                var matches = queued[character].Where(index => index >= i - 1_000 && index < i).ToList();

                if (matches.Count > 0)
                {
                    var previousFound = found;

                    found += matches.Count;

                    queued[character].RemoveAll(index => index >= i - 1_000 && index < i);

                    if (found > 64)
                    {
                        return matches[63 - previousFound];
                    }
                }
            }

            i++;
        }
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