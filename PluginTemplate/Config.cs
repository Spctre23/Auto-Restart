using Newtonsoft.Json;

namespace PluginTemplate;

public class Config
{
    public static Config Instance { get; private set; } = new();

    public bool SampleBooleanSetting { get; set; } = true;
    public int SampleIntSetting { get; set; } = 5;

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
