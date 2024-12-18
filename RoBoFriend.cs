namespace KitBuilderTask;

/// <summary>
/// Represents a component capable of moving within table boundaries.
/// </summary>
class RoBoFriend : ICommandExecuter
{
    private int posX;

    private int posY;

    private int speed = 1;

    private int currentDirection;

    private bool isPosXSet = false;

    private bool isPosYSet = false;

    private ICliParser cliParser;

    private IReportGenerator reportGenerator;

    /// <summary>
    /// <c>RoBoFriend</c> class constructor.
    /// </summary>
    /// <param name="cliParser">Parser component used to parse the CLI commands</param>
    /// <param name="reportGenerator">Report generator component used to write the report to the terminal</param>
    public RoBoFriend(ICliParser cliParser, IReportGenerator reportGenerator)
    {
        this.cliParser = cliParser;
        this.reportGenerator = reportGenerator;
    }

    /// <inheritdoc />    
    public void ExecuteCommands()
    {
        var isLive = true;
        while (isLive)
        {
            Console.WriteLine("Please enter a command to continue\n");

            string[] argsArray = cliParser.ParseArgs();

            if (argsArray.Length > 0)
            {
                foreach (string arg in argsArray)
                {

                    if (arg.StartsWith("place"))
                    {
                        Place(arg);
                    }
                    else if (arg == "end")
                    {
                        isLive = false;
                    }
                    else if (isPosXSet && isPosYSet)
                    {
                        switch (arg)
                        {
                            case "move":
                                Move();
                                break;
                            case "left":
                                Left();
                                break;
                            case "right":
                                Right();
                                break;
                            case "report":
                                Report();
                                break;
                            default:
                                //do nothing
                                break;
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// Sets the <c>x</c> or <c>y</c> coordinate as long as it is within the table boundaries.
    /// </summary>
    /// <param name="value">The value to set the coordinate to</param>
    /// <param name="isPosX">Whether the coodinate is for the <c>x</c> axis or not</param>
    private void SetPos(int value, bool isPosX)
    {
        if (isPosX && Table.xBoundaryMin <= value && value <= Table.xBoundaryMax)
        {
            posX = value;
            if (!isPosXSet)
            {
                isPosXSet = true;
            }
        }
        else if (!isPosX && Table.yBoundaryMin <= value && value <= Table.yBoundaryMax)
        {
            posY = value;
            if (!isPosYSet)
            {
                isPosYSet = true;
            }
        }
    }

    /// <summary>
    /// Places the <c>RoBoFriend</c> at the coordinates on the table facing the specified <c>direction</c>.
    /// </summary>
    /// <param name="arg">String containing the command details in the syntaxx of <c>PLACE X Y DIRECTION</c></param>
    private void Place(string arg)
    {
        string[] args = arg.Split(' ');

        int argPosX;
        int argPosY;
        Direction argDirection;

        if (args.Length == 4 && args[0] == "place" &&
            int.TryParse(args[1], out argPosX) &&
            int.TryParse(args[2], out argPosY) &&
            Enum.TryParse(args[3], true, out argDirection))
        {
            SetPos(argPosX, true);
            SetPos(argPosY, false);
            currentDirection = (int)argDirection;
        }
    }

    /// <summary>
    /// Moves the <c>RoBoFriend</c> along the table coordinates an amount equal to its <c>speed</c>
    /// </summary>
    private void Move()
    {
        switch (currentDirection)
        {
            case (int)Direction.North:
                SetPos(posY + speed, false);
                break;
            case (int)Direction.East:
                SetPos(posX + speed, true);
                break;
            case (int)Direction.South:
                SetPos(posY - speed, false);
                break;
            case (int)Direction.West:
                SetPos(posX - speed, true);
                break;
            default:
                //do nothing
                break;
        }
    }

    /// <summary>
    /// Rotates the facing direction of the <c>RoBoFriend</c> by 90 degrees counter-clockwise.
    /// </summary>
    private void Left()
    {
        currentDirection -= 1;
        if (currentDirection < (int)Direction.North)
        {
            currentDirection = (int)Direction.West;
        }
    }

    /// <summary>
    /// Rotates the facing direction of the <c>RoBoFriend</c> by 90 degrees clockwise.
    /// </summary>
    private void Right()
    {
        currentDirection += 1;
        if (currentDirection > (int)Direction.West)
        {
            currentDirection = (int)Direction.North;
        }
    }

    /// <summary>
    /// Requests a report of the current <c>x</c> coordinate, <c>y</c> coordinate, and
    /// the <c>Direction</c> the <c>RoBoFriend</c> is facing.
    /// </summary>
    private void Report()
    {
        if (isPosXSet && isPosYSet)
        {
            reportGenerator.GenerateReport(this.posX, this.posY, (Direction)currentDirection);
        }
    }
}