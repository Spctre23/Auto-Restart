using Microsoft.Xna.Framework;
using Terraria;
using TerrariaApi.Server;
using TShockAPI;
using TShockAPI.Hooks;

namespace AutoRestart;

[ApiVersion(2, 1)]
public class AutoRestart(Main game) : TerrariaPlugin(game)
{
    public override string Name => "AutoRestart";
    public override Version Version => new(1, 0, 0);
    public override string Author => "Spctre";
    public override string Description => "A simple TShock auto restart plugin for terraria servers.";
    private readonly string _configPath = Path.Combine(TShock.SavePath, "AutoRestart.json");
    private CancellationTokenSource cts = new();

    public override void Initialize()
    {
        Config.Load(_configPath);
        GeneralHooks.ReloadEvent += Reload;

        StartRestartScheduler();
    }

    private void Reload(ReloadEventArgs args)
    {
        args.Player.SendMessage($"[{Name}] reloaded config.", Color.Green);
        Config.Load(_configPath);

        cts.Cancel();
        cts = new();
        StartRestartScheduler();
    }

    private void StartRestartScheduler()
    {
        RestartSchedule rs = new();
        Task.Run(() => rs.Schedule(cts.Token), cts.Token);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            GeneralHooks.ReloadEvent -= Reload;
        }
        base.Dispose(disposing);
    }
}
