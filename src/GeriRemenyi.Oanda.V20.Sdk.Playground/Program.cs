namespace GeriRemenyi.Oanda.V20.Sdk.Playground
{
    using GeriRemenyi.Oanda.V20.Client.Model;
    using System;
    using System.Text.Json;

    class Program
    {
        static void Main(string[] args)
        {
            // Initialize connection
            var connection = ConnectionInitializer.InitializeApiConnection().GetAwaiter().GetResult();

            // Let the user play around
            MainMenu.InitializeMainMenu(connection).GetAwaiter().GetResult();
        }
    }
}
