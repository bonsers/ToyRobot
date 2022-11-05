using System;
using ToyRobot.Interfaces;

namespace ToyRobot.Services
{
    public class LogService : ILogService
    {
        public void WriteLine(string line)
        {
            Console.WriteLine(line);
        }
    }
}
