using System.Text.Json.Nodes;
using Testament;

namespace Plugin;

public class HelloCommand : ICommand
{
    public string Name { get; } = "hello";
    public string Description { get; } = "Hello World!";

    public JsonObject Execute(JsonObject data)
    {
        return new JsonObject
        {
            ["message"] = "Hello, World!"
        };
    }
}