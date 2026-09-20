using Microsoft.Xna.Framework;
using TShockAPI;
using Command = PluginTemplate.Commands.Core.Command;

namespace PluginTemplate.Commands;

public class ExampleCommand : Command
{
    public override string Name => "example";

    public override string Description => "An example command.";

    public override void Execute(CommandArgs args)
    {
        List<string> parameters = args.Parameters;
        if (parameters.Count == 0)
        {
            args.Player.SendMessage("This is an example command.", Color.Lime);
        }
        else if (parameters[0] == "test")
        {
            args.Player.SendMessage("Test message", Color.Magenta);
        }
        else
        {
            args.Player.SendMessage("Hello world!", Color.SkyBlue);
        }
    }
}
