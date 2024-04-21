using System.Text.Json;
using System.Text.Json.Nodes;

namespace Testament;

public interface ICommand
{
    public string Name { get; }
    public string Description { get; }
    public JsonObject Execute(JsonObject data);
}
