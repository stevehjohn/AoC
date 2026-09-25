using System.Buffers.Text;
using System.Security.Cryptography;
using System.Text;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2016._05;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var prefix = Input[0];

        var prefixBytes = Encoding.ASCII.GetBytes(prefix);

        var password = new char[8];

        var found = 0;

        var suffix = 1;

        const int batchSize = 4_096;

        var matches = new Match[batchSize];

        while (true)
        {
            var start = suffix;

            Parallel.For(0, batchSize, i =>
            {
                matches[i] = Calculate(prefixBytes, start + i);
            });

            for (var i = 0; i < batchSize; i++)
            {
                var match = matches[i];

                if (! match.IsMatch)
                {
                    continue;
                }

                if (match.Position > 7 || password[match.Position] != '\0')
                {
                    continue;
                }

                password[match.Position] = match.Character;

                found++;

                if (found == 8)
                {
                    return new string(password);
                }
            }

            suffix += batchSize;
        }
    }

    private static Match Calculate(byte[] prefixBytes, int suffix)
    {
        Span<byte> bytes = stackalloc byte[prefixBytes.Length + 10];

        prefixBytes.CopyTo(bytes);

        Utf8Formatter.TryFormat(suffix, bytes[prefixBytes.Length..], out var written);

        Span<byte> hash = stackalloc byte[16];

        MD5.TryHashData(bytes[..(prefixBytes.Length + written)], hash, out _);

        if (hash[0] != 0 || hash[1] != 0 || (hash[2] & 0b1111_0000) != 0)
        {
            return default;
        }

        var position = hash[2] & 0b0000_1111;

        var value = hash[3] >> 4;

        var character = value < 10 ? (char) ('0' + value) : (char) ('a' + value - 10);

        return new Match(true, position, character);
    }

    private readonly record struct Match(bool IsMatch, int Position, char Character);
}