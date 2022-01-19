using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2020._13;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var busTimes = Input[1].Split(',').Select((b, i) => (Id: b == "x" ? -1 : long.Parse(b), TimeOffset: (long) i)).Where(b => b.Id != -1).ToList();
        
        var time = 0L;

        var step = 1L;

        foreach (var bus in busTimes)
        {
            while ((time + bus.TimeOffset) % bus.Id != 0)
            {
                time += step;
            }

            step *= bus.Id;
        }

        return time.ToString();
    }
}