using System.Buffers.Text;
using System.Security.Cryptography;
using System.Text;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2016._14;

public abstract class Base : Solution
{
    public override string Description => "One time pad";

    protected int RunHashes(int additionalHashes = 0)
    {
        var salt = Input[0];

        var saltBytes = Encoding.ASCII.GetBytes(salt);

        var queued = new List<int>[16];

        for (var x = 0; x < 16; x++)
        {
            queued[x] = [];
        }

        var matches = new List<int>();

        var found = 0;

        var i = 0;

        if (additionalHashes == 0)
        {
            while (true)
            {
                var hash = CalculateHash(saltBytes, i, additionalHashes);

                var result = ProcessHash(hash, i, queued, matches, ref found);

                if (result >= 0)
                {
                    return result;
                }

                i++;
            }
        }

        const int batchSize = 1_024;

        var hashes = new HashInfo[batchSize];

        while (true)
        {
            var start = i;

            Parallel.For(0, batchSize, x =>
            {
                hashes[x] = CalculateHash(saltBytes, start + x, additionalHashes);
            });

            for (var x = 0; x < batchSize; x++)
            {
                i = start + x;

                var result = ProcessHash(hashes[x], i, queued, matches, ref found);

                if (result >= 0)
                {
                    return result;
                }
            }
        }
    }

    private static int ProcessHash(HashInfo hash, int i, List<int>[] queued, List<int> matches, ref int found)
    {
        if (hash.Triple >= 0)
        {
            queued[hash.Triple].Add(i);
        }

        if (hash.Quintuple < 0)
        {
            return -1;
        }

        matches.Clear();

        var list = queued[hash.Quintuple];

        for (var x = list.Count - 1; x >= 0; x--)
        {
            var idx = list[x];

            if (idx < i - 1_000)
            {
                list.RemoveAt(x);

                continue;
            }

            if (idx < i)
            {
                matches.Add(idx);

                list.RemoveAt(x);
            }
        }

        if (matches.Count == 0)
        {
            return -1;
        }

        var previousFound = found;

        found += matches.Count;

        if (found > 64)
        {
            return matches[^(64 - previousFound)];
        }

        return -1;
    }

    private static HashInfo CalculateHash(byte[] saltBytes, int i, int additionalHashes)
    {
        Span<byte> baseBuffer = stackalloc byte[saltBytes.Length + 11];

        saltBytes.CopyTo(baseBuffer);

        var numberSpan = baseBuffer[saltBytes.Length..];

        Utf8Formatter.TryFormat(i, numberSpan, out var written);

        var toHash = baseBuffer[..(saltBytes.Length + written)];

        Span<byte> hashBytes = stackalloc byte[16];

        MD5.TryHashData(toHash, hashBytes, out _);

        Span<byte> hexBytes = stackalloc byte[32];

        for (var j = 0; j < additionalHashes; j++)
        {
            BytesToLowerHex(hashBytes, hexBytes);

            MD5.TryHashData(hexBytes, hashBytes, out _);
        }

        BytesToLowerHex(hashBytes, hexBytes);

        return new HashInfo(GetTripleRepeatedCharacter(hexBytes), GetQuintupleRepeatedCharacter(hexBytes));
    }

    private static void BytesToLowerHex(ReadOnlySpan<byte> src, Span<byte> dest)
    {
        const string hex = "0123456789abcdef";

        var j = 0;

        for (var i = 0; i < src.Length; i++)
        {
            var b = src[i];

            dest[j++] = (byte) hex[(b >> 4) & 0xF];

            dest[j++] = (byte) hex[b & 0xF];
        }
    }

    private static int GetTripleRepeatedCharacter(ReadOnlySpan<byte> hex)
    {
        for (var i = 0; i < hex.Length - 2; i++)
        {
            var c = hex[i];

            if (c == hex[i + 1] && c == hex[i + 2])
            {
                return c <= (byte) '9' ? c - (byte) '0' : c - (byte) 'a' + 10;
            }
        }

        return -1;
    }

    private static int GetQuintupleRepeatedCharacter(ReadOnlySpan<byte> hex)
    {
        for (var i = 0; i < hex.Length - 4; i++)
        {
            var c = hex[i];

            if (c == hex[i + 1] && c == hex[i + 2] && c == hex[i + 3] && c == hex[i + 4])
            {
                return c <= (byte) '9' ? c - (byte) '0' : c - (byte) 'a' + 10;
            }
        }

        return -1;
    }

    private readonly record struct HashInfo(int Triple, int Quintuple);
}
