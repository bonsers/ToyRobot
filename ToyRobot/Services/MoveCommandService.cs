using System;
using ToyRobot.Interfaces;
using ToyRobot.Models;

namespace ToyRobot.Services
{
    internal class MoveCommandService : ICommandService
    {

        private readonly ILogService _logService;

        public MoveCommandService(ILogService logService)
        {
            _logService = logService;
        }

        public int Process(ref Robot robot, int commandCount)
        {
            if (commandCount == 1)
            {
                string message = "MOVE command ignore. Cannot do this command on first go";
                _logService.WriteLine(message);
                return (int)ErrorMessage.COMMAND_IGNORED;
            }
            
            if (robot.f == Direction.NORTH)
            {
                int temp = robot.y;
                temp++;
                bool isStillOnTable = temp <= Constants.MAX_Y && temp >= Constants.MIN_Y;
                if (isStillOnTable)
                {
                    robot.y++;
                }
                else
                {
                    //ignore
                    _logService.WriteLine(Constants.MOVE + " Command Ignored");
                }
            }
            else if (robot.f == Direction.EAST)
            {
                int temp = robot.x;
                temp++;
                bool isStillOnTable = temp <= Constants.MAX_X && temp >= Constants.MIN_X;
                if (isStillOnTable)
                {
                    robot.x++;
                }
                else
                {
                    _logService.WriteLine(Constants.MOVE + " Command Ignored");
                }
            }
            else if (robot.f == Direction.SOUTH)
            {
                int temp = robot.y;
                temp--;
                bool isStillOnTable = temp <= Constants.MAX_Y && temp >= Constants.MIN_Y;
                if (isStillOnTable)
                {
                    robot.y--;
                }
                else
                {
                    _logService.WriteLine(Constants.MOVE + " Command Ignored");
                }
            }
            else if (robot.f == Direction.WEST)
            {
                int temp = robot.x;
                temp--;
                bool isStillOnTable = temp <= Constants.MAX_X && temp >= Constants.MIN_X;
                if (isStillOnTable)
                {
                    robot.x--;
                }
                else
                {
                    _logService.WriteLine(Constants.MOVE + " Command Ignored");
                }
            }

            return (int)ErrorMessage.COMMAND_COMPLETED;
        }
    }
}
