using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2016._04;

public abstract class Base : Solution
{
    public override string Description => "Security through obscurity";

    protected static bool IsRealRoom(string roomId, string checksum)
    {
        var letters = roomId.GroupBy(c => c).Select(c => new { Character = c.Key, Count = c.Count() }).OrderByDescending(c => c.Count).ThenBy(c => c.Character).Select(c => c.Character).Take(5).ToArray();

        return new string(letters) == checksum;
    }

    protected static (string RoomId, int SectorId, string Checksum) ParseLine(string line)
    {
        var split = line[..^1].Split('[', StringSplitOptions.TrimEntries);

        var roomId = split[0][..split[0].LastIndexOf('-')].Replace("-", string.Empty);

        var sectorId = int.Parse(split[0][(split[0].LastIndexOf('-') + 1)..]);

        var checksum = split[1];

        return (roomId, sectorId, checksum);
    }
}