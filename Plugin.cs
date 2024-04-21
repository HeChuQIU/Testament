using System.Reflection;

namespace Testament;

public class Plugin(string location)
{
    public List<ICommand> Commands { get; set; } = CreateCommands(LoadPlugin(location)).ToList();

    private static Assembly LoadPlugin(string pluginLocation)
    {
        Console.WriteLine($"Loading commands from: {pluginLocation}");
        var loadContext = new PluginLoadContext(pluginLocation);
        return loadContext.LoadFromAssemblyName(new AssemblyName(Path.GetFileNameWithoutExtension(pluginLocation)));
    }

    private static IEnumerable<ICommand> CreateCommands(Assembly assembly)
    {
        var count = 0;

        var types = assembly.GetTypes();
        foreach (var type in types)
        {
            if (!typeof(ICommand).IsAssignableFrom(type)) continue;
            if (Activator.CreateInstance(type) is not ICommand result) continue;
            count++;
            yield return result;
        }

        if (count != 0) yield break;
        var availableTypes = string.Join(",", assembly.GetTypes().Select(t => t.FullName));
        throw new ApplicationException(
            $"Can't find any type which implements ICommand in {assembly} from {assembly.Location}.\n" +
            $"Available types: {availableTypes}");
    }
}