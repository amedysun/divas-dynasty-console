using Serilog;

class Program
{
    private static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
           .WriteTo.Console()
           .CreateLogger();
    }
}