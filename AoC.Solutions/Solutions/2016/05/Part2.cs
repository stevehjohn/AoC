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

        var suffix = 0;

        while (true)
        {
            var hash = MD5.HashData(Encoding.ASCII.GetBytes($"{prefix}{suffix}"));

            var hex = Convert.ToHexString(hash);

            suffix++;

            if (hex.StartsWith("00000"))
            {
                var position = hex[5] - '0';

                if (position > 7 || password[position] != '\0')
                {
                    continue;
                }

                password[position] = hex[6];

                if (password.All(c => c != '\0'))
                {
                    break;
                }
            }
        }

        return new string(password).ToLower();
    }
}