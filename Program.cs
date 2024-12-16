using System.CommandLine;
using System.CommandLine.Invocation;

class Program
{
    static void Main(string[] args)
    {
        var argument = new Argument<string>(name: "args");

        var rootCommand = new RootCommand();

        rootCommand.AddArgument(argument);

        rootCommand.SetHandler((args) =>
        {
            RoBoFriend roboFriend = new RoBoFriend();
            roboFriend.ExecuteCommands(args);
        }, argument);

        rootCommand.Invoke(args);
    }
}

