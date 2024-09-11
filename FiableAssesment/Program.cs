// See https://aka.ms/new-console-template for more information
using System.Security.Cryptography.X509Certificates;

namespace FiableAssessment;

internal class ToyRobot
{
    enum RobotFacing
    {
        NORTH, EAST, SOUTH, WEST
    }
    static void Main(string[] args)
    {

        bool isTerminate = false;
        string command;
        string robotFacing = string.Empty;

        bool isInitialized = false;

        int x = 0;
        int y = 0;

        while (!isTerminate) {

            Console.Write("Enter Command: ");
            command = Console.ReadLine() ?? string.Empty ;

            if (IsCommandValid(command))
            {
                if (command.StartsWith("PLACE"))
                {
                    string[] placeParamters = command.Split(' ')[1].Split(',');
                    x = int.Parse(placeParamters[0]);
                    y = int.Parse(placeParamters[1]);
                    robotFacing = placeParamters[2];

                    isInitialized = true;
                }
                else
                {
                    if (isInitialized)
                    {
                        if (command == "REPORT")
                        {
                            Console.WriteLine(x.ToString() + " " + y.ToString() + " " + robotFacing);
                        }
                        else if (command == "LEFT" | command == "RIGHT")
                        {
                            robotFacing = ChangeDirection(robotFacing, command);
                        }
                        else if (command == "MOVE")
                        {
                            MoveRobot(ref x, ref y, robotFacing);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Application should be initialised first, please execute 'PLACE X,Y,F' Command first");
                    }
                }
                
            }
            else
            {
                Console.WriteLine("Invalid command!");
            };
        }

    }

    private static bool IsCommandValid(string command)
    {
        string[] validCommands = [ "MOVE", "LEFT", "RIGHT", "REPORT"];

        if (string.IsNullOrEmpty(command))
        {
            Console.WriteLine("Validation Error: Command should not be empty");
            return false;
        }

        if (command.Any(c => char.IsLower(c)))
        {
            Console.WriteLine("Validation Error: Command should not contain lowercase characters");
            return false;
        }

        if (command.StartsWith("PLACE"))
        {
            string[] placeCommandParams = command.Split(' ');
            if (placeCommandParams.Length > 2 | (placeCommandParams.Length <= 1))
            {
                Console.WriteLine("Validation Error: Invalid 'PLACE' Command, should have one space");
                return false;
            }
            if (placeCommandParams[0] != "PLACE")
            {
                Console.WriteLine("Validation Error: Invalid 'PLACE' Command");
                return false;
            }
        }
        else
        {
            if (!validCommands.Any(x => x.Contains(command)))
            {
                Console.WriteLine("Validation Error: Invalid Command");
                return false;
            }
        }
       
        return true;
    }

    private static void MoveRobot(ref int x, ref int y, string robotFacing)
    {
        if (robotFacing == "NORTH")
        {
            if (y >= 4)
            {
                Console.WriteLine("Robot is about to fall off");
            }
            else
            {
                y += 1;
            }
        }
        else if (robotFacing == "EAST")
        {
            if (x >= 4)
            {
                Console.WriteLine("Robot is about to fall off");
            }
            else
            {
                x += 1;
            }
        }
        else if (robotFacing == "WEST")
        {
            if (x <= 0)
            {
                Console.WriteLine("Robot is about to fall off");
            }
            else
            {
                x -= 1;
            }
        }
        else if (robotFacing == "SOUTH")
        {
            if (y <= 0)
            {
                Console.WriteLine("Robot is about to fall off");
            }
            else
            {
                y -= 1;
            }
        }
    }

    private static string ChangeDirection(string robotFacing, string direction)
    {
        if (robotFacing == "NORTH")
        {
            return direction == "LEFT" ? "WEST" : "EAST";
        }
        else if (robotFacing == "EAST")
        {
            return direction == "LEFT" ? "NORTH" : "SOUTH";
        }
        else if (robotFacing == "WEST")
        {
            return direction == "LEFT" ? "SOUTH" : "NORTH";
        }
        else if (robotFacing == "SOUTH")
        {
            return direction == "LEFT" ? "EAST" : "WEST";
        }
        return "";
    }
}



