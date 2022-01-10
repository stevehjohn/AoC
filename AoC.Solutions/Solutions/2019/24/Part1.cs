using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2019._24;

[UsedImplicitly]
public class Part1 : Base
{
    private readonly HashSet<int> _previousStates = new();

    public override string GetAnswer()
    {
        ParseInput();

        _previousStates.Add(GetBiodiversity());

        while (true)
        {
            PlayRound();

            var bioDiversity = GetBiodiversity();

            if (_previousStates.Contains(bioDiversity))
            {
                return bioDiversity.ToString();
            }

            _previousStates.Add(bioDiversity);
        }
    }

    private int GetBiodiversity()
    {
        var i = 1;

        var diversity = 0;

        for (var y = 1; y < 6; y++)
        {
            for (var x = 1; x < 6; x++)
            {
                if (Grid[x, y])
                {
                    diversity += i;
                }

                i *= 2;
            }
        }

        return diversity;
    }

    private void ParseInput()
    {
        var y = 1;
        foreach (var line in Input)
        {
            for (var x = 1; x < 6; x++)
            {
                Grid[x, y] = line[x - 1] == '#';
            }

            y++;
        }
    }
}