using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2018._22;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        GenerateMap();

        var result = GetRiskLevel();

        return result.ToString();
    }

    private int GetRiskLevel()
    {
        var riskLevel = 0;
        
        for (var y = 0; y < TargetY + 1; y++)
        {
            for (var x = 0; x < TargetX + 1; x++)
            {
                riskLevel += Map[x, y] is '.' or 'M' or 'T' ? 0 : Map[x, y] == '=' ? 1 : 2;
            }
        }

        return riskLevel;
    }
}