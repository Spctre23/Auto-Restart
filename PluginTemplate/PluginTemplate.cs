using PluginTemplate.Commands.Core;
using Terraria;
using TerrariaApi.Server;
using TShockAPI;
using TShockAPI.Hooks;

namespace PluginTemplate;

/// <summary>
/// The main plugin class should always be decorated with an ApiVersion attribute. The current API Version is 2.1
/// </summary>
[ApiVersion(2, 1)]
public class PluginTemplate(Main game) : TerrariaPlugin(game)
{
    /// <summary>
    /// The name of the plugin.
    /// </summary>
    public override string Name => "TemplatePlugin";

    /// <summary>
    /// The version of the plugin in its current state.
    /// </summary>
    public override Version Version => new(1, 0, 0);

    /// <summary>
    /// The author(s) of the plugin.
    /// </summary>
    public override string Author => "Ijwu, Spctre";

    /// <summary>
    /// A short, one-line, description of the plugin's purpose.
    /// </summary>
    public override string Description => "A simple demonstration of the TShock and TS-API code structure, with additional examples for command and config management.";

    private readonly string _configPath = Path.Combine(TShock.SavePath, "TemplatePlugin.json");

    /// <summary>
    /// Performs plugin initialization logic.
    /// Add your hooks etc here.
    /// </summary>
    public override void Initialize()
    {
        Config.Load(_configPath);

        CommandManager cmdManager = new();
        cmdManager.RegisterCommands();

        GeneralHooks.ReloadEvent += Reload;
    }

    /// <summary>
    /// Performs plugin reload logic.
    /// Add want you want to be executed when the plugin reloads here.
    /// </summary>
    private void Reload(ReloadEventArgs args)
    {
        Config.Load(_configPath);
    }

    /// <summary>
    /// Performs plugin cleanup logic.
    /// Remove your hooks and perform general cleanup here.
    /// </summary>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            //unhook
            //dispose child objects
            //set large objects to null

            GeneralHooks.ReloadEvent -= Reload;
        }
        base.Dispose(disposing);
    }
}
