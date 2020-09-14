namespace GeriRemenyi.Oanda.V20.Sdk.Playground
{
    using GeriRemenyi.Oanda.V20.Client.Model;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public static class InstrumentMenu
    {
        public static async Task InitializeInstrumentMenu(ApiConnection connection)
        {
            var selection = -1;

            while (selection != 0)
            {
                // Print out menu header
                Console.Clear();
                Console.WriteLine("==============");
                Console.WriteLine("= Instrument =");
                Console.WriteLine("==============");
                Console.WriteLine("1) Instrument candles");
                Console.WriteLine("0) Go back to the Main Menu");

                // Wait for the user selection
                Console.WriteLine("");
                Console.Write("Please input the menupoint: ");
                selection = Utilities.TryToParseNumericAnswer(Console.ReadLine(), 0, 1);

                // Show submenu details based on the selection
                switch (selection)
                {
                    case 1:
                        await ShowInstrumentCandles(connection);
                        break;
                }
            }
        }

        private static async Task ShowInstrumentCandles(ApiConnection connection)
        {
            // Print out menu header
            Console.Clear();
            Console.WriteLine("======================");
            Console.WriteLine("= Instrument candles =");
            Console.WriteLine("======================");
            Console.WriteLine("");

            // Let the user select from accounts
            Console.WriteLine("Please select the instrument");
            Console.WriteLine("-----------------------------");
            var availableInstruments = Enum.GetValues(typeof(InstrumentName)).Cast<InstrumentName>().ToList();
            foreach (var instrument in availableInstruments.Select((name, index) => new { index = index + 1, name }))
            {
                Console.WriteLine($"{instrument.index}) {instrument.name}");
            }
            Console.WriteLine("");
            Console.Write("Selected instrument: ");
            var selectedInstrument = Utilities.TryToParseNumericAnswer(Console.ReadLine(), 1, Convert.ToUInt32(availableInstruments.Count));
            Console.WriteLine("");

            // Let the user select the candle granularity
            Console.WriteLine("Please select the candle granularity");
            Console.WriteLine("-------------------------------------");
            var availableGranularities = Enum.GetValues(typeof(CandlestickGranularity)).Cast<CandlestickGranularity>().ToList();
            foreach (var granularity in availableGranularities.Select((name, index) => new { index = index + 1, name }))
            {
                Console.WriteLine($"{granularity.index}) {granularity.name}");
            }
            Console.WriteLine("");
            Console.Write("Selected granularity: ");
            var selectedGranularity = Utilities.TryToParseNumericAnswer(Console.ReadLine(), 1, Convert.ToUInt32(availableGranularities.Count));
            Console.WriteLine("");

            // Let the user input how many days to show
            Console.WriteLine("Please input how many days to show");
            Console.WriteLine("-----------------------------------");
            Console.Write("Days (max 5000 candlestick will be shown): ");
            var selectedDays = Utilities.TryToParseNumericAnswer(Console.ReadLine(), 1);
            Console.WriteLine("");

            // Load details for the instrument
            var candles = await connection
                .GetInstrument(availableInstruments.ElementAt(selectedInstrument - 1))
                .GetCandles(availableGranularities.ElementAt(selectedGranularity - 1), DateTime.Now.AddDays(selectedDays * -2), DateTime.Now.AddDays(-1));
            Console.WriteLine("Candles");
            Console.WriteLine("---------------------------------");
            Console.WriteLine("");
            Console.WriteLine(JToken.Parse(candles.ToJson()));
            Console.WriteLine("");

            // Wait for a keypress to go back to menu selector
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }
    }
}
