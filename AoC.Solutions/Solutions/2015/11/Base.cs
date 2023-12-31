using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2015._11;

public abstract class Base : Solution
{
    public override string Description => "Password policy";

    protected static string GetNextPassword(string input)
    {
        // ReSharper disable once StringLiteralTypo
        var characters = "abcdefghjkmnpqrstuvwxyz!";

        var password = input.ToCharArray();

        while (true)
        {
            var digit = password.Length - 1;

            while (digit >= 0)
            {
                password[digit] = characters[characters.IndexOf(password[digit]) + 1];

                if (password[digit] != '!')
                {
                    break;
                }

                password[digit] = characters[0];

                digit--;
            }

            if (! ContainsSequence(password))
            {
                continue;
            }

            if (! ContainsPairs(password))
            {
                continue;
            }

            return new string(password);
        }
    }

    private static bool ContainsSequence(char[] password)
    {
        for (var i = 0; i < password.Length - 2; i++)
        {
            if (password[i] == (char) (password[i + 1] - 1) && password[i] == (char) (password[i + 2] - 2))
            {
                return true;
            }
        }

        return false;
    }

    private static bool ContainsPairs(char[] password)
    {
        var pairs = 0;

        for (var i = 0; i < password.Length - 1; i++)
        {
            if (password[i] == password[i + 1])
            {
                pairs++;

                if (pairs > 1)
                {
                    return true;
                }

                i++;
            }
        }

        return false;
    }
}