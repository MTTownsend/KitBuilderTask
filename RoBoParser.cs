namespace KitBuilderTask;

/// <summary>
/// The RoBoFriend CLI parser.
/// </summary>
public sealed class RoBoParser : ICliParser
{
    /// <inheritdoc />
    public string[] ParseArgs()
    {
        var input = Console.ReadLine();
        string[] argsArray = (input != null) ? input.ToLower().Split(',', StringSplitOptions.TrimEntries): Array.Empty<string>();

        return argsArray;
    }
}
