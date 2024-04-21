// See https://aka.ms/new-console-template for more information

using System.Text.Json.Nodes;
using Testament;

Console.WriteLine("Hello, Testament!");
Console.WriteLine("This is a test of the Testament plugin system.");
Console.WriteLine("Input plugin path: ");
var pluginPath = Console.ReadLine();
var plugin = new Plugin(pluginPath ?? throw new InvalidOperationException());
plugin.Commands.Select((c, i) => $"{i + 1}. {c.Name} - {c.Description}").ToList().ForEach(Console.WriteLine);
while (true)
{
    Console.WriteLine("Input command number: ");
    if (!int.TryParse(Console.ReadLine(), out var commandNumber) || commandNumber < 1 || commandNumber > plugin.Commands.Count)
    {
        Console.WriteLine("Invalid command number.");
        continue;
    }

    var command = plugin.Commands[commandNumber - 1];
    var data = new JsonObject();
    var result = command.Execute(data);
    Console.WriteLine(result);
}