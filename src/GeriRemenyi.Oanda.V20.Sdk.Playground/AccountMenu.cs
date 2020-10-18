﻿namespace GeriRemenyi.Oanda.V20.Sdk.Playground
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
                Console.WriteLine("2) Specific account");
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
                        await ShowOneAccountSelector(connection);
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

        private static async Task ShowOneAccountSelector(ApiConnection connection)
        {
            var selection = -1;

            while (selection != 0)
            {
                // Print out menu header
                Console.Clear();
                Console.WriteLine("====================");
                Console.WriteLine("= Specific account =");
                Console.WriteLine("====================");

                // Print out accounts as menu points
                var accounts = await connection.GetAccounts();
                foreach (var account in accounts.Select((content, index) => new { index = index + 1, content }))
                {
                    Console.WriteLine($"{account.index}) {account.content.Id}");
                }

                // Add exit menu point
                Console.WriteLine("0) Exit");

                // Wait for user selection
                Console.WriteLine("");
                Console.Write("Please select an account: ");
                selection = Utilities.TryToParseNumericAnswer(Console.ReadLine(), 0, Convert.ToUInt32(accounts.Count));

                // Handle selection
                if (selection != 0) 
                {
                    await ShowOneAccountMenu(connection, accounts.ElementAt(selection - 1).Id);
                }
            }        
        }

        private static async Task ShowOneAccountMenu(ApiConnection connection, string selectedAccountId)
        {
            var selection = -1;

            while (selection != 0)
            {
                // Print out menu header
                Console.Clear();
                Console.WriteLine("==================================");
                Console.WriteLine($"= Account '{selectedAccountId}' =");
                Console.WriteLine("==================================");
                Console.WriteLine("1) Account details");
                Console.WriteLine("2) Trading");
                Console.WriteLine("0) Go back to the Main Menu");

                // Wait for the user selection
                Console.WriteLine("");
                Console.Write("Please input the menupoint: ");
                selection = Utilities.TryToParseNumericAnswer(Console.ReadLine(), 0, 2);

                // Show submenu details based on the selection
                switch (selection)
                {
                    case 1:
                        await ShowOneAccountDetails(connection, selectedAccountId);
                        break;
                    case 2:
                        await TradeMenu.InitializeTradeMenu(connection, selectedAccountId);
                        break;
                }
            }
        }

        private static async Task ShowOneAccountDetails(ApiConnection connection, string selectedAccountId)
        {
            Console.Clear();
            Console.WriteLine("==========================================");
            Console.WriteLine($"= Account '{selectedAccountId}' details =");
            Console.WriteLine("==========================================");
            Console.WriteLine("");

            // Load details for the selected accounts
            var accountDetails = await connection.GetAccount(selectedAccountId).GetDetails();
            Console.WriteLine(JToken.Parse(accountDetails.ToJson()));
            Console.WriteLine("");

            // Wait for a keypress to go back to menu selector
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }
    }
}
