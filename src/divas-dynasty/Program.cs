using Microsoft.Extensions.DependencyInjection;
using Serilog;
using domain.shared;
using domain.game.console;

namespace divas_dynasty
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

            var serviceCollection = new ServiceCollection();
            serviceCollection.ConfigureServices();
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var gameHandler = serviceProvider.GetRequiredService<GameHandler>();
            while (true)
            {
                try
                {
                    Console.WriteLine("Please, submit action:");
                    Log.Information(gameHandler.Handle(new GameCommand(Console.ReadLine())).Message);
                }
                catch (Exception ex)
                {
                    Log.Error($"{ex.Message}");
                }
            }
        }
    }
}
