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

                var hex = Convert.ToHexString(hash);

                suffix++;

                if (hex.StartsWith("00000"))
                {
                    password.Append(hex[5]);

                    break;
                }
            }
        }

        return password.ToString().ToLower();
    }
}