using System.Text.Json;

namespace AoC.Games.Games.Deflectors.Levels;

public class LevelDataProvider
{
    private Dictionary<int, Level> _levels = [];

    public void LoadLevels()
    {
        var data = File.ReadAllText("./Games/Deflectors/Levels/levels.json");

        var levels = JsonSerializer.Deserialize<Level[]>(data);
    }
}