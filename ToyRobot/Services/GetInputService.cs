using System;
using ToyRobot.Interfaces;

namespace ToyRobot.Services
{
    internal class GetInputService : IGetInputService
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}
