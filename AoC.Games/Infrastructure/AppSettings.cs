using System.Text.Json;

namespace AoC.Games.Infrastructure;

public class AppSettings
{
    public float ScaleFactor { get; init; }
    
    public bool AllowCheat { get; init; }
    
    private static readonly Lazy<AppSettings> Lazy = new(GetAppSettings);

    public static AppSettings Instance => Lazy.Value;
    
    private static AppSettings GetAppSettings()
    {
        var data = File.ReadAllText("app-settings.json");

        return JsonSerializer.Deserialize<AppSettings>(data);
    }
}