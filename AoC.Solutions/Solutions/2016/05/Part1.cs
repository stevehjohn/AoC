using System.Security.Cryptography;
using System.Text;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2016._05;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var prefix = Input[0];

        var password = new StringBuilder();

        var suffix = 0;

        for (var i = 0; i < 8; i++)
        {
            while (true)
            {
                var hash = MD5.HashData(Encoding.ASCII.GetBytes($"{prefix}{suffix}"));

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