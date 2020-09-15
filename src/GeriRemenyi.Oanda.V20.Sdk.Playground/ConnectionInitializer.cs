
namespace GeriRemenyi.Oanda.V20.Sdk.Playground
{
    using GeriRemenyi.Oanda.V20.Client.Client;
    using GeriRemenyi.Oanda.V20.Sdk.Utilities;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public static class ConnectionInitializer
    {
        public static async Task<ApiConnection> InitializeApiConnection()
        {
            ApiConnection connection = null;

            while (connection == null)
            {
                try
                {
                    Console.WriteLine("==========================");
                    Console.WriteLine("= Connect to OANDA's API =");
                    Console.WriteLine("==========================");
                    Console.WriteLine("");
                    var server = ServerSelector();
                    var token = InputToken();
                    connection = new ApiConnection(server, token);
                    await connection.Test();
                }
                catch (ApiException ae)
                {
                    Console.WriteLine($"Failed to initialize connection. Exception message: {ae.Message}. Please double check that the server and the access token are correct.");
                    Console.WriteLine("");
                }
            }

            return connection;
        }

        public static OandaServer ServerSelector()
        {
            // Print out available servers
            Console.WriteLine("Please select the OANDA server you want to connect to");
            Console.WriteLine("------------------------------------------------------");
            var availableServers = Enum.GetValues(typeof(OandaServer)).Cast<OandaServer>().ToList();
            foreach (var server in availableServers.Select((name, index) => new { index = index + 1, name }))
            {
                Console.WriteLine($"{server.index}) {server.name}");
            }
            Console.WriteLine("");
            Console.Write("Please input the number of the desired server: ");

            // Let the user select
            var selectedServer = Utilities.TryToParseNumericAnswer(Console.ReadLine(), 1, Convert.ToUInt32(availableServers.Count));
            Console.WriteLine("");
            return availableServers.ElementAt(selectedServer - 1);
        }

        public static string InputToken()
        {
            // Print out help text
            Console.WriteLine("Please input your access token for the respective environment");
            Console.WriteLine("--------------------------------------------------------------");
            Console.Write("Token: ");

            // Let the user input the token
            var token = Console.ReadLine();
            Console.WriteLine("");
            return token;
        }
    }
}
