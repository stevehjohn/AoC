using System.Security.Cryptography;
using System.Text;
using JetBrains.Annotations;
using Org.BouncyCastle.Crypto.Digests;

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

        var length = prefix.Length;
        
        var md5Digest = new MD5Digest();

        var hash = new byte[md5Digest.GetDigestSize()];

        while (true)
        {
            retry:
            var suffixLength = 1 + (int) Math.Log10(suffix);

            var workingSuffix = suffix;

            var index = suffixLength - 1;
                
            while (workingSuffix > 0)
            {
                bytes[length + index] = (byte) ('0' + (byte) (workingSuffix % 10));

                workingSuffix /= 10;

                index--;
            }
                
            md5Digest.BlockUpdate(bytes, 0, length + suffixLength);

            md5Digest.DoFinal(hash, 0);

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