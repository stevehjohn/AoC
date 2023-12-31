using System.Text;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2016._04;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var rooms = new List<(string RoomId, int SectorId)>();

        foreach (var line in Input)
        {
            var data = ParseLine(line);

            if (IsRealRoom(data.RoomId, data.Checksum))
            {
                rooms.Add((data.RoomId, data.SectorId));
            }
        }

        var sectorId = 0;

        foreach (var room in rooms)
        {
            var decrypted = DecryptRoom(room.RoomId, room.SectorId);
            
            if (decrypted.Contains("north"))
            {
                sectorId = room.SectorId;

                break;
            }
        }

        return sectorId.ToString();
    }

    private static string DecryptRoom(string roomId, int sectorId)
    {
        var shift = sectorId % 26;

        var builder = new StringBuilder();

        foreach (var c in roomId)
        {
            var decrypted = c + shift;

            if (decrypted > 'z')
            {
                decrypted -= 26;
            }

            builder.Append((char) decrypted);
        }

        return builder.ToString();
    }
}