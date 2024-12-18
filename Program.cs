namespace KitBuilderTask;

/// <summary>
/// Main entry point into the application.
/// </summary>
class Program
{
    static void Main(string[] args)
    {
        ICliParser cliParser = new RoBoParser();
        IReportGenerator reportGenerator = new RoBoReportGenerator();

        ICommandExecuter demo = new RoBoFriend(cliParser, reportGenerator);
        demo.ExecuteCommands();
    }
}

