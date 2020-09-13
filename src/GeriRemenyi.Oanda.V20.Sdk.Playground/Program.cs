namespace GeriRemenyi.Oanda.V20.Sdk.Playground
{
    using GeriRemenyi.Oanda.V20.Client.Model;
    using System;
    using System.Text.Json;

    class Program
    {
        static void Main(string[] args)
        {
            // Init connection
            var connection = ConnectionInitializer.InitializeApiConnection();

            // Get candles
            var candles = connection.GetInstrument(InstrumentName.EUR_USD).GetCandles(CandlestickGranularity.D, DateTime.Now.AddDays(-14), DateTime.Now).GetAwaiter().GetResult();
            Console.WriteLine(JsonSerializer.Serialize(candles.Candles));
        }
    }
}
