using ToyRobot.Interfaces;
using ToyRobot.Models;

namespace ToyRobot.Services
{
    internal class LeftCommandService : ICommandService
    {
        private readonly ILogService _logService;

        public LeftCommandService(ILogService logService)
        {
            _logService = logService;
        }

        public int Process(ref Robot robot, int commandCount)
        {
            if (commandCount == 1)
            {
                string message = "LEFT command ignore. Cannot do this command on first go";
                _logService.WriteLine(message);
                return (int)ErrorMessage.COMMAND_IGNORED;
            }

            if (robot.f == Direction.NORTH)
            {
                robot.f = Direction.WEST;
            }
            else if (robot.f == Direction.WEST)
            {
                robot.f = Direction.SOUTH;
            }
            else if (robot.f == Direction.SOUTH)
            {
                robot.f = Direction.EAST;
            }
            else if (robot.f == Direction.EAST)
            {
                robot.f = Direction.NORTH;
            }

            return (int)ErrorMessage.COMMAND_COMPLETED;
        }
    }
}
