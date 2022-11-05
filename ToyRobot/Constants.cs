namespace ToyRobot
{
    internal class Constants
    {
        public const string PLACE = "PLACE";
        public const string MOVE = "MOVE";
        public const string REPORT = "REPORT";
        public const string LEFT = "LEFT";
        public const string RIGHT = "RIGHT";

        public const int MIN_Y = 0;
        public const int MAX_Y = 5;
        public const int MIN_X = 0;
        public const int MAX_X = 5;
    }

    internal enum ErrorMessage
    {
        EXIT = -1,
        COMMAND_COMPLETED = 0,
        COMMAND_IGNORED = 1
    }
}
