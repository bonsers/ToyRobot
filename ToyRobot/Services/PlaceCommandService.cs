using System;
using ToyRobot.Interfaces;
using ToyRobot.Models;

namespace ToyRobot.Services
{
    internal class PlaceCommandService : ICommandService
    {
        private readonly int _x;
        private readonly int _y;
        private readonly Direction _f;
        private readonly ILogService _logService;

        public PlaceCommandService(int x, 
            int y, 
            Direction f,
            ILogService logService)
        {
            _x = x;
            _y = y;
            _f = f;
            _logService = logService;
        }

        public int Process(ref Robot robot, int commandCount)
        {
            if (commandCount != 1)
            {
                string message = "`PLACE x y f` command can only be run on 1st command";
                _logService.WriteLine(message);
                throw new Exception(message);
            }

            if (isNotOnTable(_x, _y))
            {
                string message = "PLACE Command ignored. This would be off the table";
                _logService.WriteLine(message);
                throw new Exception(message);
            }

            robot.x = _x;
            robot.y = _y;
            robot.f = _f;

            return (int)ErrorMessage.COMMAND_COMPLETED;
        }

        private bool isNotOnTable(int x, int y)
        {
            if (x < Constants.MIN_X)
            {
                return true;
            }

            if (x > Constants.MAX_X)
            {
                return true;
            }

            if (y < Constants.MIN_Y)
            {
                return true;
            }

            if (y > Constants.MAX_Y)
            {
                return true;
            }

            return false;
        }
    }
}
