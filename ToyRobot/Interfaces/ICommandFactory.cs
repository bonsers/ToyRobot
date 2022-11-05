namespace ToyRobot.Interfaces
{
    public interface ICommandFactory
    {
        ICommandService Create(string command);
    }
}