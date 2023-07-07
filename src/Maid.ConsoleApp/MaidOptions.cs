namespace Maid.ConsoleApp;

public sealed class MaidOptions
{
    public string Path { get; set; } = string.Empty;

    public string Command { get; set; } = string.Empty;

    public bool Recursive { get; set; }

    public bool MoveDontCopy { get; set; }

    public bool PathToName { get; set; }
}
