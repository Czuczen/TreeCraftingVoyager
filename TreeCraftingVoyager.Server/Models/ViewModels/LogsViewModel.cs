namespace TreeCraftingVoyager.Server.Models.ViewModels;

public class LogsViewModel
{
    public List<List<string>> Trace { get; } = new();

    public List<List<string>> Debug { get; } = new();

    public List<List<string>> Info { get; } = new();

    public List<List<string>> Warn { get; } = new();

    public List<List<string>> Error { get; } = new();

    public List<List<string>> Critical { get; } = new();

    public List<List<string>> None { get; } = new();
}
