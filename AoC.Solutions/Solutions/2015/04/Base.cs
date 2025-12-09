using System.Buffers.Text;
using System.Security.Cryptography;
using System.Text;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2015._04;

public abstract class Base : Solution
{
    public override string Description => "Crypto stocking stuffer";

    protected int GetAnswer(string pattern)
    {
        var key = Input[0];

        var i = 1;

        var keyBytes = Encoding.ASCII.GetBytes(key);

        var buffer = new byte[keyBytes.Length + 11];

        Array.Copy(keyBytes, buffer, keyBytes.Length);

        var requireSixZeroes = pattern.Length == 6;
        
        using var md5 = MD5.Create();
        
        while (true)
        {
            Utf8Formatter.TryFormat(i, buffer.AsSpan(keyBytes.Length), out var written);

            var hash = md5.ComputeHash(buffer, 0, keyBytes.Length + written);

            var isMatch = hash[0] == 0 && hash[1] == 0 && (requireSixZeroes ? hash[2] == 0 : (hash[2] & 0xF0) == 0);

            if (isMatch)
            {
                break;
            }

            i++;
        }
            
        md5.Clear();

        return i;
    }
}