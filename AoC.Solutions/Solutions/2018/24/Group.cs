namespace AoC.Solutions.Solutions._2018._24;

public class Group
{
    public int Units { get; set; }

    public int HitPoints { get; }

    public List<string> WeakTo { get; } = new();

    public List<string> ImmuneTo { get; } = new();

    public int DamagePoints { get; }

    public string DamageType { get; }

    public int Initiative { get; }

    public Group(string input)
    {
        // ReSharper disable StringIndexOfIsCultureSpecific.1
        // ReSharper disable StringLastIndexOfIsCultureSpecific.1

        Units = int.Parse(input[..input.IndexOf(' ')]);

        input = input[(input.IndexOf("with") + 5)..];

        HitPoints = int.Parse(input[..input.IndexOf(' ')]);

        if (input.IndexOf('(') > -1)
        {
            input = input[(input.IndexOf("(") + 1)..];

            var specialProperties = input[..input.IndexOf(')')];
        }

        input = input[(input.IndexOf("does") + 5)..];

        DamagePoints = int.Parse(input[..input.IndexOf(' ')]);

        input = input[(input.IndexOf(" ") + 1)..];

        DamageType = input[..(input.IndexOf(" "))];

        input = input[(input.LastIndexOf(" ") + 1)..];

        Initiative = int.Parse(input);

        // ReSharper restore StringIndexOfIsCultureSpecific.1
        // ReSharper restore StringLastIndexOfIsCultureSpecific.1
    }
}