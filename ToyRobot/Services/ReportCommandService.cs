using System;
using ToyRobot.Interfaces;
using ToyRobot.Models;

namespace ToyRobot.Services
{
    internal class ReportCommandService : ICommandService
    {
        private readonly ILogService _logService;

        public ReportCommandService(ILogService logService)
        {
            _logService = logService;
        }

        public int Process(ref Robot robot, int commandCount)
        {
            if (commandCount == 1)
            {
                string message = "REPORT command ignore. Cannot do this command on first go";
                _logService.WriteLine(message);
                return (int)ErrorMessage.COMMAND_IGNORED;
            }

            _logService.WriteLine($"Output: {robot.x},{robot.y},{robot.f}");
            
            return (int)ErrorMessage.EXIT;
        }
    }
}
