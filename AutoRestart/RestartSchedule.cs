using Microsoft.Xna.Framework;
using TShockAPI;

namespace AutoRestart;

public class RestartSchedule
{
    private DateTime _now;
    private DateTime _restartTime;
    private TimeSpan _delay;

    public RestartSchedule()
    {
        _now = DateTime.Now;
        _restartTime = new(_now.Year, _now.Month, _now.Day, Config.Instance.TargetHour, Config.Instance.TargetMinute, 0);
    }

    public async Task Schedule(CancellationToken token)
    {
        if (_now >= _restartTime)
        {
            _restartTime = _restartTime.AddDays(Config.Instance.RestartIntervalDays);
        }

        if (Config.Instance.BroadcastRestartWarning)
            AnnounceRestart(token);

        try
        {
            await Task.Delay(_delay, token);
            RestartServer();
        }
        catch (TaskCanceledException) { return; }
    }

    private async void AnnounceRestart(CancellationToken token)
    {
        while (true)
        {
            _now = DateTime.Now;
            _delay =  _restartTime - DateTime.Now;

            string hourWarning = $"Server will perform a quick scheduled restart in {_delay.TotalMinutes} minutes.";
            string fiveSecWarning = $"Restarting in {_delay.TotalSeconds}...";

            switch ((int)_delay.TotalMinutes)
            {
                case int n when (n == 60 || n == 30 || n == 15 || n == 10 || n == 5 || n == 2):
                    Broadcast(hourWarning);
                    break;
            }
            switch ((int)_delay.TotalSeconds)
            {
                case <= 10:
                    Broadcast(fiveSecWarning);
                    break;
            }

            try
            {
                await Task.Delay(1000, token);
            }
            catch (TaskCanceledException) { return; }
        }
    }

    private void RestartServer()
    {
        Broadcast("Restarting server...");
        TShock.Utils.StopServer();
    }

    private void Broadcast(string msg)
    {
        Utils.Instance.Broadcast(msg, Color.Orange);
    }
}
