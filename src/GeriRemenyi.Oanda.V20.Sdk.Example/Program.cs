using GeriRemenyi.Oanda.V20.Sdk.Utilities;
using System;

namespace GeriRemenyi.Oanda.V20.Sdk.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            // Init connection
            var server = ConnectionInitializer.ServerSelector();
            var token = ConnectionInitializer.InputToken();
            var connection = new OandaConnection(server, token);

            // Get account details
        }
    }
}
