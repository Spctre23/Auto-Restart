using TShockAPI;

namespace PluginTemplate.Commands.Core;

public abstract class Command
{
    public abstract string Name { get; }

    public virtual string Description { get; } = string.Empty;

    /// <summary>
    /// Extra permissions specific to this command, additional to the standard "[plugin].commands.[command]" permission.
    /// </summary>
    public virtual List<string> Permissions { get; } = [];

    public abstract void Execute(CommandArgs args);
}