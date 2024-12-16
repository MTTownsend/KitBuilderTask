class RoBoFriend
{
    private int posX;

    private int posY;

    private int? currentDirection;

    private enum direction
    {
        north, east, south, west
    }

    private bool posXSet = false;

    private bool posYSet = false;

    private bool currentDirectionSet = false;

    public void ExecuteCommands(string args)
    {
        string[] argsArray = args.ToLower().Split(',', StringSplitOptions.TrimEntries);

        if (argsArray.Length > 0 && argsArray[0].StartsWith("place"))
        {
            foreach (string arg in argsArray)
            {

                if (arg.StartsWith("place"))
                {
                    Place(arg);
                }
                else
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

    private void setPos(int value, bool isPosX)
    {
        if (0 <= value && value <= 5)
        {
            if (isPosX)
            {
                posX = value;
                if (!posXSet)
                {
                    posXSet = true;
                }
            }
            else
            {
                posY = value;
                if (!posYSet)
                {
                    posYSet = true;
                }
            }
        }
    }

    private void Place(string arg)
    {
        string[] args = arg.Split(' ');

        int argPosX;
        int argPosY;
        direction argDirection;

        if (args.Length == 4 && args[0] == "place" &&
            int.TryParse(args[1], out argPosX) &&
            int.TryParse(args[2], out argPosY) &&
            Enum.TryParse(args[3], out argDirection))
        {
            setPos(argPosX, true);
            setPos(argPosY, false);
            currentDirection = (int)argDirection;
            currentDirectionSet = true;
        }
    }

    private void Move()
    {
        switch (currentDirection)
        {
            case (int)direction.north:
                setPos(posY + 1, false);
                break;
            case (int)direction.east:
                setPos(posX + 1, true);
                break;
            case (int)direction.south:
                setPos(posY - 1, false);
                break;
            case (int)direction.west:
                setPos(posX - 1, true);
                break;
            default:
                //do nothing
                break;
        }
    }

    private void Left()
    {
        currentDirection -= 1;
        if (currentDirection < (int)direction.north)
        {
            currentDirection = (int)direction.west;
        }
    }

    private void Right()
    {
        currentDirection += 1;
        if (currentDirection > (int)direction.west)
        {
            currentDirection = (int)direction.north;
        }
    }

    private void Report()
    {
        if (posXSet && posYSet && currentDirectionSet)
        {
            string currentDirectionString = ((direction)currentDirection).ToString().ToUpper();
            Console.WriteLine($"{posX}, {posY}, {currentDirectionString}");
        }
    }
}