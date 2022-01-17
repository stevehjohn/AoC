using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2019._18;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        ModifyMap();

        InterrogateMap();

        var result = FindShortestPath();

        return result.ToString();
    }

    private void ModifyMap()
    {
        Map[39, 39] = '1';
        Map[40, 39] = '#';
        Map[41, 39] = '2';
        Map[39, 40] = '#';
        Map[40, 40] = '#';
        Map[41, 40] = '#';
        Map[39, 41] = '3';
        Map[40, 41] = '#';
        Map[41, 41] = '4';
    }
}