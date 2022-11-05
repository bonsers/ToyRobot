using ToyRobot.Models;

namespace ToyRobot.Interfaces
{
    public interface ICommandService
    {
        public int Process(ref Robot robot, int commandCount);
    }
}
