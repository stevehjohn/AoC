using System.Text.Json;

namespace AoC.Games.Games.Deflectors.Levels;

public class LevelDataProvider
{
    private readonly Dictionary<int, Level> _levels = [];

    public int LevelCount => _levels.Count;

    public Level GetLevel(int number)
    {
        LoadLevels();
        
        return _levels[number];
    }
    
    private void LoadLevels()
    {
        _levels.Clear();
        
        var data = File.ReadAllText("./Games/Deflectors/Levels/levels.json");

        var levels = JsonSerializer.Deserialize<Level[]>(data);

        foreach (var level in levels)
        {
            _levels.Add(level.Id, level);
        }
    }
}