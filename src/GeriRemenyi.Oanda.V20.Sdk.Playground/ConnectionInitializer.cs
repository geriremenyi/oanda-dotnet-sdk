
namespace GeriRemenyi.Oanda.V20.Sdk.Playground
{
    using GeriRemenyi.Oanda.V20.Sdk.Exceptions;
    using GeriRemenyi.Oanda.V20.Sdk.Utilities;
    using System;
    using System.Linq;

    public static class ConnectionInitializer
    {
        public static ApiConnection InitializeApiConnection()
        {
            ApiConnection connection = null;

            while (connection == null)
            {
                try
                {
                    var server = ServerSelector();
                    var token = InputToken();
                    connection = new ApiConnection(server, token);
                }
                catch (ApiConnectionException ace)
                {
                    Console.WriteLine($"Failed to initialize connection. Exception message: {ace.Message}. Please double check that the server and the access token are correct.");
                    Console.WriteLine("");
                    Console.WriteLine("");
                }
            }

            return connection;
        }

        public static OandaServer ServerSelector()
        {
            // Print out available servers
            Console.WriteLine("Please select the OANDA server you want to connect to");
            Console.WriteLine("======================================================");
            var availableServers = Enum.GetValues(typeof(OandaServer)).Cast<OandaServer>().ToList();
            foreach (var server in availableServers.Select((name, index) => new { index = index + 1, name }))
            {
                Console.WriteLine($"{server.index}) {server.name}");
            }
            Console.WriteLine("");
            Console.Write("Please input the number of the desired server: ");

            // Let the user select
            var selectedServer = TryToParseNumericAnswer(Console.ReadLine(), 1, Convert.ToUInt32(availableServers.Count));
            Console.WriteLine("");
            Console.WriteLine("");
            return availableServers.ElementAt(selectedServer - 1);
        }

        public static string InputToken()
        {
            // Print out help text
            Console.WriteLine("Please input your access token for the respective environment");
            Console.WriteLine("==============================================================");
            Console.WriteLine(
                "If you don't know what is it, please visit and gather more info on the following link: " +
                "https://developer.oanda.com/rest-live-v20/authentication/#obtaining-a-personal-access-token"
            );
            Console.WriteLine("");
            Console.Write("Please input the token: ");

            // Let the user input the token
            var token = Console.ReadLine();
            Console.WriteLine("");
            Console.WriteLine("");
            return token;
        }

        private static int TryToParseNumericAnswer(string answer, uint minValue, uint maxValue)
        {
            var numericAnswer = -1;

            while (numericAnswer < 0) 
            {
                try
                {
                    numericAnswer = int.Parse(answer);
                    if (numericAnswer < minValue || numericAnswer > maxValue)
                    {
                        throw new ArgumentException("The answer is out of range");
                    }
                }
                catch
                {
                    Console.Write("Invalid selection. Please try again: ");
                    answer = Console.ReadLine();
                    numericAnswer = -1;
                }
            }

            return numericAnswer;
        }
    }
}
