namespace KitBuilderTask;

/// <summary>
/// Represent a component that generates a report containing coordinates and a direction.
/// </summary>
public interface IReportGenerator
{
    /// <summary>
    /// Writes a report to the terminal containing a <c>x</c> coordinate, <c>y</c> coordinate, and
    /// <c>Direction</c>
    /// </summary>
    /// <param name="x"><c>x</c> axis coordinate</param>
    /// <param name="y"><c>y</c> axis coordinate</param>
    /// <param name="direction">A cardinal direction</param>
    void GenerateReport(int x, int y, Direction direction);
}
