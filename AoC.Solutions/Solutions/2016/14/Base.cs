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
        
        var saltBytes = Encoding.ASCII.GetBytes(salt);

        var i = 0;
        
        var found = 0;

        var queued = new Dictionary<char, List<int>>(16);
        
        const string hexChars = "0123456789abcdef";

        foreach (var character in hexChars)
        {
            queued[character] = [];
        }

        var matches = new List<int>();

        Span<byte> baseBuffer = stackalloc byte[saltBytes.Length + 11];
        
        saltBytes.CopyTo(baseBuffer);

        Span<byte> hashBytes = stackalloc byte[16];
        
        Span<byte> hexBytes  = stackalloc byte[32];

        while (true)
        {
            var numberSpan = baseBuffer[saltBytes.Length..];

            var written = Encoding.ASCII.GetBytes(i.ToString(), numberSpan);
            
            var toHash = baseBuffer[..(saltBytes.Length + written)];

            MD5.TryHashData(toHash, hashBytes, out _);

            for (var j = 0; j < additionalHashes; j++)
            {
                BytesToLowerHex(hashBytes, hexBytes);

                MD5.TryHashData(hexBytes, hashBytes, out _);
            }

            BytesToLowerHex(hashBytes, hexBytes);

            var triple = GetTripleRepeatedCharacter(hexBytes);

            if (triple != '\0')
            {
                queued[triple].Add(i);
            }

            var quint = GetQuintupleRepeatedCharacter(hexBytes);

            if (quint != '\0')
            {
                matches.Clear();

                var list = queued[quint];

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

                if (matches.Count > 0)
                {
                    var previousFound = found;
                    
                    found += matches.Count;

                    if (found > 64)
                    {
                        return matches[^(64 - previousFound)];
                    }
                }
            }

            i++;
        }
    }

    private static void BytesToLowerHex(ReadOnlySpan<byte> src, Span<byte> dest)
    {
        const string hex = "0123456789abcdef";

        var j = 0;
        
        for (var i = 0; i < src.Length; i++)
        {
            var b = src[i];
            
            dest[j++] = (byte)hex[(b >> 4) & 0xF];
            
            dest[j++] = (byte)hex[b & 0xF];
        }
    }

    private static char GetTripleRepeatedCharacter(ReadOnlySpan<byte> hex)
    {
        for (var i = 0; i < hex.Length - 2; i++)
        {
            var c = hex[i];
            
            if (c == hex[i + 1] && c == hex[i + 2])
            {
                return (char)c;
            }
        }

        return '\0';
    }

    private static char GetQuintupleRepeatedCharacter(ReadOnlySpan<byte> hex)
    {
        for (var i = 0; i < hex.Length - 4; i++)
        {
            var c = hex[i];
            
            if (c == hex[i + 1] && c == hex[i + 2] && c == hex[i + 3] && c == hex[i + 4])
            {
                return (char)c;
            }
        }

        return '\0';
    }
}
