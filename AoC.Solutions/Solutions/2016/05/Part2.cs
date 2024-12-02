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

        var password = new char[8];

        var suffix = 1;

        var bytes = new byte[prefix.Length + 10];

        Buffer.BlockCopy(Encoding.ASCII.GetBytes(prefix), 0, bytes, 0, prefix.Length);
        
        var span = new Span<byte>(bytes);

        var length = prefix.Length;

        while (true)
        {
            retry:
            var suffixLength = 1 + (int) Math.Log10(suffix);

            var workingSuffix = suffix;

            var index = suffixLength - 1;
                
            while (workingSuffix > 0)
            {
                span[length + index] = (byte) ('0' + (byte) (workingSuffix % 10));

                workingSuffix /= 10;

                index--;
            }
                
            var hash = MD5.HashData(span[..(length + suffixLength)]);
            suffix++;

            if (hash[0] != 0 || hash[1] != 0 || (hash[2] & 0b1111_0000) != 0)
            {
                continue;
            }

            var position = hash[2] & 0b0000_1111;

            if (position > 7 || password[position] != '\0')
            {
                continue;
            }

            var hex = Convert.ToHexString(new Span<byte>(hash).Slice(3, 1));

            password[position] = hex[0];

            for (var i = 0; i < 8; i++)
            {
                if (password[i] == 0)
                {
                    goto retry;
                }
            }
            
            break;
        }
        return new string(password).ToLower();
    }
}