using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2016._04;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var total = 0;

        foreach (var line in Input)
        {
            var data = ParseLine(line);

            if (IsRealRoom(data.RoomId, data.Checksum))
            {
                total += data.SectorId;
            }
        }

        return total.ToString();
    }
}