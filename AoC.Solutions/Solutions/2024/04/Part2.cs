using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._04;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var result = ScanPuzzle();
        
        return result.ToString();
    }

    protected override int CheckCell(int x, int y)
    {
        return 0;
    }
}