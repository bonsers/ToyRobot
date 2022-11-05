using ToyRobot.Interfaces;
using ToyRobot.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ToyRobot
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddTransient<IToyRobotService, ToyRobotService>();
                    services.AddTransient<ILogService, LogService>();
                    services.AddTransient<IGetInputService, GetInputService>();
                    services.AddTransient<ICommandFactory, CommandFactory>();
                })
                .Build();

            var svc = ActivatorUtilities.CreateInstance<ToyRobotService>(host.Services);
            svc.Run();
        }
    }
}
