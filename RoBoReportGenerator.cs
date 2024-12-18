namespace KitBuilderTask;

/// <summary>
/// The RoBoFriend report generator
/// </summary>
public sealed class RoBoReportGenerator : IReportGenerator
{
    /// <inheritdoc />
    public void GenerateReport(int x, int y, Direction direction)
    {
        Console.WriteLine($"{x}, {y}, {direction.ToString().ToUpper()}\n");
    }
}
