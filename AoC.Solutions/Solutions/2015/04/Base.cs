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

        using var md5 = MD5.Create();
        
        while (true)
        {
            var hash = Convert.ToHexString(md5.ComputeHash(Encoding.ASCII.GetBytes($"{key}{i}")));

            if (hash.StartsWith(pattern))
            {
                break;
            }

            i++;
        }
            
        md5.Clear();

        return i;
    }
}