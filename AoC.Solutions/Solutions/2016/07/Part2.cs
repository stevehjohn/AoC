using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2016._07;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var total = 0;

        foreach (var line in Input)
        {
            total += SupportsSsl(line) ? 1 : 0;
        }

        return total.ToString();
    }

    private static bool SupportsSsl(string line)
    {
        var data = ParseLine(line);

        var sequences = new List<string>();

        foreach (var address in data.SupernetAddresses)
        {
            for (var i = 0; i < address.Length - 2; i++)
            {
                if (address[i] == address[i + 2] && address[i] != address[i + 1])
                {
                    sequences.Add(new string($"{address[i + 1]}{address[i]}{address[i + 1]}"));
                }
            }
        }

        foreach (var sequence in sequences)
        {
            if (data.HypernetAddresses.Any(a => a.Contains(sequence)))
            {
                return true;
            }
        }

        return false;
    }

    private static (List<string> SupernetAddresses, List<string> HypernetAddresses) ParseLine(string line)
    {
        var parts = line.Split(new[] { '[', ']' }, StringSplitOptions.TrimEntries);

        var supernetAddresses = new List<string>();

        var hypernetAddresses = new List<string>();

        for (var i = 0; i < parts.Length; i++)
        {
            if (i % 2 == 0)
            {
                supernetAddresses.Add(parts[i]);
            }
            else
            {
                hypernetAddresses.Add(parts[i]);
            }
        }

        return (supernetAddresses, hypernetAddresses);
    }
}