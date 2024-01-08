using System.Text.Json.Serialization;
using JetBrains.Annotations;

namespace AoC.Games.Games.Deflectors.Levels;

[UsedImplicitly]
public class End : Point
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Direction Direction { get; set; }
}