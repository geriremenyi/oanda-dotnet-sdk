namespace GeriRemenyi.Oanda.V20.Sdk.Playground
{
    using Newtonsoft.Json.Linq;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public static class AccountMenu
    {
        public static async Task InitializeAccountMenu(ApiConnection connection)
        {
            var selection = -1;

            while (selection != 0)
            {
                // Print out menu header
                Console.Clear();
                Console.WriteLine("===========");
                Console.WriteLine("= Account =");
                Console.WriteLine("===========");
                Console.WriteLine("1) All accounts");
                Console.WriteLine("2) Account details");
                Console.WriteLine("0) Go back to the Main Menu");

                // Wait for the user selection
                Console.WriteLine("");
                Console.Write("Please input the menupoint: ");
                selection = Utilities.TryToParseNumericAnswer(Console.ReadLine(), 0, 2);

                // Show submenu details based on the selection
                switch (selection)
                {
                    case 1:
                        await ShowAllAccounts(connection);
                        break;
                    case 2:
                        await ShowOneAccount(connection);
                        break;
                }
            }
        }

        private static async Task ShowAllAccounts(ApiConnection connection)
        {
            // Print out menu header
            Console.Clear();
            Console.WriteLine("================");
            Console.WriteLine("= All accounts =");
            Console.WriteLine("================");
            Console.WriteLine("");

            // Collect and print out accounts
            var accounts = await connection.GetAccounts();
            accounts.ForEach(a =>
            {
                Console.WriteLine(JToken.Parse(a.ToJson()));
                Console.WriteLine("");
            });

            // Wait for a keypress to go back to menu selector
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

        private static async Task ShowOneAccount(ApiConnection connection)
        {
            // Print out menu header
            Console.Clear();
            Console.WriteLine("===================");
            Console.WriteLine("= Account details =");
            Console.WriteLine("===================");
            Console.WriteLine("");

            // Let the user select from accounts
            Console.WriteLine("Please select the account");
            Console.WriteLine("--------------------------");
            var accounts = await connection.GetAccounts();
            foreach (var account in accounts.Select((content, index) => new { index = index + 1, content }))
            {
                Console.WriteLine($"{account.index}) {account.content.Id}");
            }
            Console.WriteLine("");
            Console.Write("Selected account: ");
            var selectedAccount = Utilities.TryToParseNumericAnswer(Console.ReadLine(), 1, Convert.ToUInt32(accounts.Count));
            Console.WriteLine("");

            // Load details for the selected accounts
            var accountDetails = await connection.GetAccount(accounts.ElementAt(selectedAccount - 1).Id).GetDetails();
            Console.WriteLine("Details for the selected account");
            Console.WriteLine("---------------------------------");
            Console.WriteLine("");
            Console.WriteLine(JToken.Parse(accountDetails.ToJson()));
            Console.WriteLine("");

            // Wait for a keypress to go back to menu selector
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }
    }
}
