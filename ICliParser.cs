namespace KitBuilderTask;

/// <summary>
/// Represents a parser of raw command line arguments.
/// </summary>
public interface ICliParser
{
    /// <summary>
    /// Reads and parses CLI arguments, returning a formatted array ready for execution.
    /// </summary>
    /// <returns>
    /// A formatted array of arguments ready for execution.
    /// </returns>
    string[] ParseArgs();
}
