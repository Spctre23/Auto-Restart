using System.Reflection;

namespace PluginTemplate.Commands.Core;

public class CommandManager
{
    private readonly string _globalPermission = "templateplugin.commands";

    public void RegisterCommands()
    {
        Type baseType = typeof(Command);
        Assembly assembly = Assembly.GetExecutingAssembly();
        foreach (Type type in assembly.GetTypes())
        {
            if (!type.IsSubclassOf(baseType) || type == baseType || type.IsAbstract) continue;
            if (Activator.CreateInstance(type) is not Command cmd) continue;

            cmd.Permissions.Add($"{_globalPermission}.{cmd.Name}");

            TShockAPI.Command tsCmd = new(cmd.Permissions, cmd.Execute, cmd.Name)
            {
                HelpText = cmd.Description
            };
            TShockAPI.Commands.ChatCommands.Add(tsCmd);
        }
    }
}
