namespace AoC.Solutions.Solutions._2022._15;

public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var position = GetDeadZone(4_000_000);
        // var position = GetDeadZone(20);
        
        return (position.X * 4_000_000L + position.Y).ToString();
    }
}