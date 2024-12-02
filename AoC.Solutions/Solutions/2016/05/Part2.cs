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

            suffix++;

            if (hash[0] != 0 || hash[1] != 0 || (hash[2] & 0b1111_0000) != 0)
            {
                continue;
            }

            var hex = Convert.ToHexString(hash);

            var position = (hash[2] & 0b1111_0000) >> 4;

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

        return new string(password).ToLower();
    }
}