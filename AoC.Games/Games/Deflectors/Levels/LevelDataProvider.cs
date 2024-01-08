using System.Text.Json;

namespace AoC.Games.Games.Deflectors.Levels;

public class LevelDataProvider
{
    private readonly Dictionary<int, Level> _levels = [];

    public int LevelCount => _levels.Count;
    
    public void LoadLevels()
    {
        _levels.Clear();
        
        var data = File.ReadAllText("./Games/Deflectors/Levels/levels.json");

        var levels = JsonSerializer.Deserialize<Level[]>(data);

        foreach (var level in levels)
        {
            _levels.Add(level.Id, level);
        }
    }

    public Level GetLevel(int number)
    {
        return _levels[number];
    }
}