using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2020._13;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var departTime = int.Parse(Input[0]);

        var busTimes = Input[1].Split(',').Where(s => s != "x").Select(b => (Id: int.Parse(b), Time: int.Parse(b) )).ToList();

        for (var i = 0; i < busTimes.Count; i++)
        {
            var id = busTimes[i].Time;

            var time = busTimes[i].Time;

            busTimes[i] = (Id: id, Time: departTime / time * time + time);
        }

        var soonest = busTimes.MinBy(b => b.Time);

        var delay = soonest.Time - departTime;

        return (delay * soonest.Id).ToString();
    }
}