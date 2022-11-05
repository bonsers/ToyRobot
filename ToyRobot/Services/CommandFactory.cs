using System;
using System.Text.RegularExpressions;
using ToyRobot.Interfaces;
using ToyRobot.Models;

namespace ToyRobot.Services
{
    public class CommandFactory : ICommandFactory
    {
        private readonly ILogService _logService;
        
        public CommandFactory(ILogService logService)
        {
            _logService = logService;
        }
        
        public ICommandService Create(string command)
        {
            if (command == Constants.LEFT)
            {
                return new LeftCommandService(_logService);
            }
            else if (command == Constants.RIGHT)
            {
                return new RightCommandService(_logService);
            }
            else if (command == Constants.REPORT)
            {
                return new ReportCommandService(_logService);
            }
            else if (command == Constants.MOVE)
            {
                return new MoveCommandService(_logService);
            }
            else if (command.Contains(Constants.PLACE))
            {
                MatchCollection matches = TryToMatchUsingRegex(command);

                if (matches.Count == 1) // There should be only one match, otherwise it can't be a valid PLACE command
                {
                    Match match = matches[0];
                    int x = int.Parse(match.Groups[1].ToString());
                    int y = int.Parse(match.Groups[2].ToString());
                    Direction f = Enum.Parse<Direction>(match.Groups[3].ToString());
                    return new PlaceCommandService(x, y, f, _logService);
                }
            }

            throw new Exception($"Cannot recognise command: {command}");
        }

        private MatchCollection TryToMatchUsingRegex(string command)
        {
            const string pattern = @"PLACE ([0-9]),([0-9]),([a-zA-Z]+)";
            RegexOptions options = RegexOptions.Multiline;
            return Regex.Matches(command, pattern, options);
        }
    }
}
