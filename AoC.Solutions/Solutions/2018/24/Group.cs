namespace AoC.Solutions.Solutions._2018._24;

public class Group
{
    public Type Type { get; }

    public int Units { get; set; }

    public int HitPoints { get; }

    public List<string> WeakTo { get; } = [];

    public List<string> ImmuneTo { get; } = [];

    public int DamagePoints { get; set; }

    public string DamageType { get; }

    public int Initiative { get; }

    public int EffectivePower => Units * DamagePoints;

    public Group(string input, Type type)
    {
        // ReSharper disable StringIndexOfIsCultureSpecific.1
        // ReSharper disable StringLastIndexOfIsCultureSpecific.1

        Type = type;

        Units = int.Parse(input[..input.IndexOf(' ')]);

        input = input[(input.IndexOf("with") + 5)..];

        HitPoints = int.Parse(input[..input.IndexOf(' ')]);

        if (input.IndexOf('(') > -1)
        {
            input = input[(input.IndexOf('(') + 1)..];

            var specialProperties = input[..input.IndexOf(')')];

            var propertyType = specialProperties.Split(';', StringSplitOptions.TrimEntries);

            foreach (var property in propertyType)
            {
                if (property.StartsWith("weak"))
                {
                    WeakTo = property[8..].Split(',', StringSplitOptions.TrimEntries).ToList();
                }
                else
                {
                    ImmuneTo = property[10..].Split(',', StringSplitOptions.TrimEntries).ToList();
                }
            }
        }

        input = input[(input.IndexOf("does") + 5)..];

        DamagePoints = int.Parse(input[..input.IndexOf(' ')]);

        input = input[(input.IndexOf(' ') + 1)..];

        DamageType = input[..input.IndexOf(' ')];

        input = input[(input.LastIndexOf(' ') + 1)..];

        Initiative = int.Parse(input);

        // ReSharper restore StringIndexOfIsCultureSpecific.1
        // ReSharper restore StringLastIndexOfIsCultureSpecific.1
    }
}