using Microsoft.Extensions.DependencyInjection;
using Serilog;
using domain.game;
using domain.shared;

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
                    gameHandler.Handle(Console.ReadLine());
                }
                catch (Exception ex)
                {
                    Log.Error($"{ex.Message}");
                }
            }
        }
    }
}
