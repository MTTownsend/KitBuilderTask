namespace KitBuilderTask.Tests;

using Moq;

using NUnit.Framework;
using NUnit.Framework.Interfaces;

/// <summary>
/// Tests for the RoBoFriendDemo class.
/// </summary>
[TestFixture]
public class RoBoFriendTests
{
    private ICommandExecuter demo;

    private Mock<ICliParser> mockCliParser;

    private Mock<IReportGenerator> mockReportGenerator;

    /// <summary>
    /// Sets up the test fixture.
    /// </summary>
    [SetUp]
    public void SetUp()
    {
        mockCliParser = new Mock<ICliParser>();
        mockReportGenerator = new Mock<IReportGenerator>();

        demo = new RoBoFriend(mockCliParser.Object, mockReportGenerator.Object);
    }

    public static object[] itemData = {
        new object[] { "Test END command first", new string[] { "end" }, ( It.IsAny<int>(), It.IsAny<int>(), It.IsAny<Direction>() ), false },
        new object[] { "Test REPORT command first", new string[] { "report", "end" }, ( It.IsAny<int>(), It.IsAny<int>(), It.IsAny<Direction>() ), false },
        new object[] { "Test MOVE command first", new string[] { "move", "end" }, ( It.IsAny<int>(), It.IsAny<int>(), It.IsAny<Direction>() ), false },
        new object[] { "Test LEFT command first", new string[] { "left", "end" }, ( It.IsAny<int>(), It.IsAny<int>(), It.IsAny<Direction>() ), false },
        new object[] { "Test RIGHT command first", new string[] { "right", "end" }, ( It.IsAny<int>(), It.IsAny<int>(), It.IsAny<Direction>() ), false },
        new object[] { "Test invalid X coordinate, minimum boundary", new string[] { "place -1 0 north", "report", "end" }, ( It.IsAny<int>(), It.IsAny<int>(), It.IsAny<Direction>() ), false },
        new object[] { "Test invalid X coordinate, maximum boundary", new string[] { "place 6 0 north", "report", "end" }, ( It.IsAny<int>(), It.IsAny<int>(), It.IsAny<Direction>() ), false },
        new object[] { "Test invalid Y coordinate, minimum boundary", new string[] { "place 0 -1 north", "report", "end" }, ( It.IsAny<int>(), It.IsAny<int>(), It.IsAny<Direction>() ), false },
        new object[] { "Test invalid Y coordinate, maximum boundary", new string[] { "place 0 6 north", "report", "end" }, ( It.IsAny<int>(), It.IsAny<int>(), It.IsAny<Direction>() ), false },
        new object[] { "Test PLACE 0 0 North", new string[] { "place 0 0 north", "report", "end" }, ( 0, 0, Direction.North ), true },
        new object[] { "Test PLACE 5 5 West", new string[] { "place 5 5 west", "report", "end" }, ( 5, 5, Direction.West ), true },
        new object[] { "Test MOVE on X boundary, minimum boundary", new string[] { "place 0 0 west", "move", "report", "end" }, ( 0, 0, Direction.West ), true },
        new object[] { "Test MOVE on X boundary, maximum boundary", new string[] { "place 5 0 east", "move", "report", "end" }, ( 5, 0, Direction.East ), true },
        new object[] { "Test MOVE on Y boundary, minimum boundary", new string[] { "place 0 0 south", "move", "report", "end" }, ( 0, 0, Direction.South ), true },
        new object[] { "Test MOVE on Y boundary, maximum boundary", new string[] { "place 0 5 north", "move", "report", "end" }, ( 0, 5, Direction.North ), true },
        new object[] { "Test MOVE on X boundary, minimum boundary", new string[] { "place 0 0 west", "move", "report", "end" }, ( 0, 0, Direction.West ), true },
        new object[] { "Test MOVE on X boundary, maximum boundary", new string[] { "place 5 0 east", "move", "report", "end" }, ( 5, 0, Direction.East ), true },
        new object[] { "Test MOVE on Y boundary, minimum boundary", new string[] { "place 0 0 south", "move", "report", "end" }, ( 0, 0, Direction.South ), true },
        new object[] { "Test MOVE on Y boundary, maximum boundary", new string[] { "place 0 5 north", "move", "report", "end" }, ( 0, 5, Direction.North ), true },
        new object[] { "Test MOVE North", new string[] { "place 0 0 north", "move", "report", "end" }, ( 0, 1, Direction.North ), true },
        new object[] { "Test MOVE East", new string[] { "place 0 0 east", "move", "report", "end" }, ( 1, 0, Direction.East ), true },
        new object[] { "Test MOVE South", new string[] { "place 0 5 south", "move", "report", "end" }, ( 0, 4, Direction.South ), true },
        new object[] { "Test MOVE West", new string[] { "place 5 0 west", "move", "report", "end" }, ( 4, 0, Direction.West ), true },
        new object[] { "Test rotate Right from North", new string[] { "place 0 0 north", "right", "report", "end" }, ( 0, 0, Direction.East ), true },
        new object[] { "Test rotate Right from East", new string[] { "place 0 0 east", "right", "report", "end" }, ( 0, 0, Direction.South ), true },
        new object[] { "Test rotate Right from South", new string[] { "place 0 0 south", "right", "report", "end" }, ( 0, 0, Direction.West ), true },
        new object[] { "Test rotate Right from West", new string[] { "place 0 0 west", "right", "report", "end" }, ( 0, 0, Direction.North ), true },
        new object[] { "Test rotate LEFT from North", new string[] { "place 0 0 north", "left", "report", "end" }, ( 0, 0, Direction.West ), true },
        new object[] { "Test rotate LEFT from East", new string[] { "place 0 0 east", "left", "report", "end" }, ( 0, 0, Direction.North ), true },
        new object[] { "Test rotate LEFT from South", new string[] { "place 0 0 south", "left", "report", "end" }, ( 0, 0, Direction.East ), true },
        new object[] { "Test rotate LEFT from West", new string[] { "place 0 0 west", "left", "report", "end" }, ( 0, 0, Direction.South ), true },
        new object[] { "Test multiple commands", new string[] { "place 3 3 north", "move", "left", "move", "left", "move", "move", "report", "end" }, ( 2, 2, Direction.South ), true },
        new object[] { "Test multiple PLACE commands", new string[] { "place 0 0 north", "move", "right", "move", "place 0 0 west", "report", "end" }, ( 0, 0, Direction.West ), true },
        new object[] { "Test Invalid commands ignored", new string[] { "place 0 0 north", "new_command", "moves", "up", "report", "end" }, ( 0, 0, Direction.North ), true },
    };

    /// <summary>
    /// Test RoBoFriend commands.
    /// </summary>
    [Test]
    [TestCaseSource(nameof(itemData))]
    public void TestRoboFriendCommands(string description, string[] commands, ValueTuple<int, int, Direction> expectedReport, bool wasReportCalled)
    {
        this.mockCliParser.SetupSequence(p => p.ParseArgs()).Returns(commands);

        this.demo.ExecuteCommands();

        this.mockReportGenerator.Verify(rg => rg.GenerateReport(expectedReport.Item1, expectedReport.Item2, expectedReport.Item3), wasReportCalled ? Times.Once : Times.Never);
    }
}
