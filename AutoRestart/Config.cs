using Newtonsoft.Json;

namespace AutoRestart;

public class Config
{
    public static Config Instance { get; private set; } = new();
    public int TargetHour { get; set; } = 0;
    public int TargetMinute { get; set; } = 0;
    public int RestartIntervalDays { get; set; } = 1;
    public bool BroadcastRestartWarning { get; set; } = true;

    public static void Load(string configPath)
    {
        if (!File.Exists(configPath))
        {
            Instance = new();
            Instance.Save(configPath);
            return;
        }
        try
        {
            Instance = JsonConvert.DeserializeObject<Config>(File.ReadAllText(configPath)) ?? new();
        }
        catch 
        { 
            Instance = new(); 
        };
    }

    public void Save(string configPath)
    {
        File.WriteAllText(configPath, JsonConvert.SerializeObject(this, Formatting.Indented));
    }
}
