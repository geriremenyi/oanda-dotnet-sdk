namespace GeriRemenyi.Oanda.V20.Sdk.Playground
{
    using GeriRemenyi.Oanda.V20.Client.Model;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Linq;
    using System.Runtime.InteropServices.ComTypes;
    using System.Threading.Tasks;

    public static class TradeMenu
    {

        public static async Task InitializeTradeMenu(ApiConnection connection, string selectedAccountId)
        {
            var selection = -1;

            while (selection != 0)
            {
                // Print out menu header
                Console.Clear();
                Console.WriteLine("=========");
                Console.WriteLine("= Trade =");
                Console.WriteLine("=========");
                Console.WriteLine("1) Open trades");
                Console.WriteLine("2) Open new trade");
                Console.WriteLine("0) Go back to the Account Menu");

                // Wait for the user selection
                Console.WriteLine("");
                Console.Write("Please input the menupoint: ");
                selection = Utilities.TryParseIntegerValue(Console.ReadLine(), 0, 2);

                // Show submenu details based on the selection
                switch (selection)
                {
                    case 1:
                        await ShowOpenTrade(connection, selectedAccountId);
                        break;
                    case 2:
                        await ShowOpenNewTrade(connection, selectedAccountId);
                        break;
                }
            }
        }

        private static async Task ShowOpenTrade(ApiConnection connection, string selectedAccountId)
        {
            // Print out menu header
            Console.Clear();
            Console.WriteLine("===============");
            Console.WriteLine("= Open trades =");
            Console.WriteLine("===============");
            Console.WriteLine("");

            // Collect and print out open trades for the account
            var trades = await connection.GetAccount(selectedAccountId).GetOpenTrades();
            foreach (var trade in trades)
            {
                Console.WriteLine(JToken.Parse(trade.ToJson()));
                Console.WriteLine("");
            }

            // Wait for a keypress to go back to menu selector
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

        private static async Task ShowOpenNewTrade(ApiConnection connection, string selectedAccountId)
        {
            // Print out menu header
            Console.Clear();
            Console.WriteLine("==================");
            Console.WriteLine("= Open new trade =");
            Console.WriteLine("==================");
            Console.WriteLine("");

            // Let the user select from instruments
            Console.WriteLine("Please select the instrument");
            Console.WriteLine("-----------------------------");
            var availableInstruments = Enum.GetValues(typeof(InstrumentName)).Cast<InstrumentName>().ToList();
            foreach (var instrument in availableInstruments.Select((name, index) => new { index = index + 1, name }))
            {
                Console.WriteLine($"{instrument.index}) {instrument.name}");
            }
            Console.WriteLine("");
            Console.Write("Selected instrument: ");
            var selectedInstrument = Utilities.TryParseIntegerValue(Console.ReadLine(), 1, Convert.ToInt32(availableInstruments.Count));
            Console.WriteLine("");

            // Let the user input how many units to trade
            Console.WriteLine("Please input how many units to trade");
            Console.WriteLine("-------------------------------------");
            Console.Write("Number of units to trade (positive number long, negative number short): ");
            var unitsToTrade = Utilities.TryParseIntegerValue(Console.ReadLine());
            Console.WriteLine("");

            // Let the user input how many units to trade
            Console.WriteLine("Lease input how far the trailing stop loss should be");
            Console.WriteLine("-----------------------------------------------------");
            Console.Write("Trailing stop loss distance: ");
            var trailingStopLossDistance = Utilities.TryParseIntegerValue(Console.ReadLine(), 1);
            Console.WriteLine("");

            var tradeOpenResponse = await connection.GetAccount(selectedAccountId).OpenTrade(availableInstruments.ElementAt(selectedInstrument - 1), unitsToTrade, trailingStopLossDistance);
        }
    }
}
