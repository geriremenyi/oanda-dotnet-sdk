namespace GeriRemenyi.Oanda.V20.Sdk.Playground
{
    using System;
    using System.Threading.Tasks;

    public static class MainMenu
    {
        public static async Task InitializeMainMenu(ApiConnection connection)
        {
            var selection = -1;

            while (selection != 0)
            {
                Console.Clear();
                Console.WriteLine("=============");
                Console.WriteLine("= Main menu =");
                Console.WriteLine("=============");
                Console.WriteLine("1) Account");
                Console.WriteLine("2) Instrument");
                Console.WriteLine("0) Exit");

                Console.WriteLine("");
                Console.Write("Please input the menupoint: ");

                selection = Utilities.TryParseIntegerValue(Console.ReadLine(), 0, 2);

                switch (selection)
                {
                    case 1:
                        await AccountMenu.InitializeAccountMenu(connection);
                        break;
                    case 2:
                        await InstrumentMenu.InitializeInstrumentMenu(connection);
                        break;
                }
            }
        }
    }
}
