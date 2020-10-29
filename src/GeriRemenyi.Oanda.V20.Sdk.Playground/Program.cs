namespace GeriRemenyi.Oanda.V20.Sdk.Playground
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initialize connection
            var connection = ConnectionInitializer.InitializeApiConnection();

            // Let the user play around
            MainMenu.InitializeMainMenu(connection).GetAwaiter().GetResult();
        }
    }
}
