using System.Security.Cryptography;
using System.Text;
using JetBrains.Annotations;
using Org.BouncyCastle.Crypto.Digests;

namespace AoC.Solutions.Solutions._2016._05;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var prefix = Input[0];

        var password = new StringBuilder();

        var suffix = 1;

        var bytes = new byte[prefix.Length + 10];

        Buffer.BlockCopy(Encoding.ASCII.GetBytes(prefix), 0, bytes, 0, prefix.Length);

        var length = prefix.Length;
        
        var md5Digest = new MD5Digest();

        var hash = new byte[md5Digest.GetDigestSize()];
        
        for (var i = 0; i < 8; i++)
        {
            while (true)
            {
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

                var hex = Convert.ToHexString(hash);

                password.Append(hex[5]);

                break;
            }
        }

        return password.ToString().ToLower();
    }
}