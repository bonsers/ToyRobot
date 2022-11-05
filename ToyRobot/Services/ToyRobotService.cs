using ToyRobot.Interfaces;
using ToyRobot.Models;

namespace ToyRobot.Services
{
    public class ToyRobotService : IToyRobotService
    {
        private readonly ILogService _logService;
        private readonly IGetInputService _getInputService;
        private readonly ICommandFactory _commandFactory;

        public ToyRobotService(ILogService logService, 
            IGetInputService getInputService,
            ICommandFactory commandFactory)
        {
            _logService = logService;
            _getInputService = getInputService;
            _commandFactory = commandFactory;
        }

        public void Run()
        {
            int commandCount = 1;
            Robot robot = new Robot();
            _logService.WriteLine("->ToyRobot");
            _logService.WriteLine("->Commands are case sensitive");

            while (true)
            {
                _logService.WriteLine("->Enter a command:");
                string command = _getInputService.ReadLine();

                var processor = _commandFactory.Create(command);
                var result = processor.Process(ref robot, commandCount);
                
                if (result == (int)ErrorMessage.COMMAND_COMPLETED)
                {
                    commandCount++;
                }
                else if (result < 0)
                {
                    break;
                }
            }

            return;
        }
    }
}
