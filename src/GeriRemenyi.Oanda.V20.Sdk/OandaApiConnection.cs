//----------------------------------------------------------------------------------------
// <copyright file="OandaV20ApiConnection.cs" company="geriremenyi.com">
//     Author: Gergely Reményi
//     Copyright (c) geriremenyi.com. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------------------

namespace GeriRemenyi.Oanda.V20.Sdk
{
    using GeriRemenyi.Oanda.V20.Client.Api;
    using GeriRemenyi.Oanda.V20.Client.Client;
    using GeriRemenyi.Oanda.V20.Client.Model;
    using GeriRemenyi.Oanda.V20.Sdk.Account;
    using GeriRemenyi.Oanda.V20.Sdk.Common.Exceptions;
    using GeriRemenyi.Oanda.V20.Sdk.Common.Types;
    using GeriRemenyi.Oanda.V20.Sdk.Common.Extensions;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Data;
    using GeriRemenyi.Oanda.V20.Sdk.Instrument;
    using System.Reflection;

    /// <summary>
    /// Implementation of an Oanda API connection
    /// </summary>
    public class OandaApiConnection : IOandaApiConnection
    {
        /// <summary>
        /// Configuration object for the connection
        /// </summary>
        public Configuration Configuration { get; }

        /// <summary>
        /// Date time format to use for the connection
        /// </summary>
        public DateTimeFormat DateTimeFormat { get; }

        /// <summary>
        /// Actual Oanda account API functions
        /// </summary>
        public IAccountApi AccountApi { get; }

        /// <summary>
        /// Actual Oanda instrument API functions
        /// </summary>
        public IInstrumentApi InstrumentApi { get; }

        /// <summary>
        /// Actual Oanda order API functions
        /// </summary>
        public IOrderApi OrderApi { get; }

        /// <summary>
        /// Actual Oanda trade API functions
        /// </summary>
        public ITradeApi TradeApi { get; }

        /// <summary>
        /// Actual Oanda position API functions
        /// </summary>
        public IPositionApi PositionApi { get; }

        /// <summary>
        /// Actual Oanda transaction API functions
        /// </summary>
        public ITransactionApi TransactionApi { get; }

        /// <summary>
        /// Actual Oanda pricing API functions
        /// </summary>
        public IPricingApi PricingApi { get; }

        /// <summary>
        /// Public getter for the accounts associated with this connection
        /// </summary>
        public IEnumerable<AccountProperties> Accounts { get; private set; }

        /// <summary>
        /// Map of account ids and objects for internal caching
        /// while the connection object is alive
        /// </summary>
        public IDictionary<string, IAccount> _accountsCache;

        /// <summary>
        /// Map of instrument names and objects for internal caching
        /// while the connection object is alive
        /// </summary>
        public IDictionary<InstrumentName, IInstrument> _instrumentCache;

        /// <summary>
        /// OandaApiConnection constuctor
        /// Sets up the configuration and the datetime format
        /// </summary>
        /// <param name="connectionType">Type of the connection</param>
        /// <param name="accessToken">Access token for the connection</param>
        /// <param name="dateTimeFormat">Date time format to use for the connection</param>
        public OandaApiConnection(OandaConnectionType connectionType, string accessToken, DateTimeFormat dateTimeFormat = DateTimeFormat.RFC3339)
        {
            // Init configuration
            Configuration = new Configuration()
            {
                UserAgent = $"OandaDotnetSdk/{Assembly.GetExecutingAssembly().GetName().Version}",
                BasePath = connectionType.ToBasePath(),
                AccessToken = accessToken
            };

            // Init date time format
            DateTimeFormat = dateTimeFormat;

            // Init actual Oanda API connections from the GeriRemenyi.Oanda.V20.Client
            AccountApi = new AccountApi(Configuration);
            InstrumentApi = new InstrumentApi(Configuration);
            OrderApi = new OrderApi(Configuration);
            TradeApi = new TradeApi(Configuration);
            PositionApi = new PositionApi(Configuration);
            TransactionApi = new TransactionApi(Configuration);
            PricingApi = new PricingApi(Configuration);

            // Init object caches
            _accountsCache = new Dictionary<string, IAccount>();
            _instrumentCache = new Dictionary<InstrumentName, IInstrument>();

            // Fire a really small request synchronously to check that the inited connection is OK
            // Also fill the available accounts based on this
            try
            {
                Accounts = AccountApi.GetAccounts().Accounts;
            }
            catch
            {
                throw new ConnectionInitializationException($"Unable to connect to {Configuration.BasePath} with the given token");
            }
        }

        /// <summary>
        /// Explicitly ask for the reload of available accounts in the connection
        /// This way a connection doesn't have to be disposed and recreated if there
        /// were changes in the available accounts for the connection
        /// </summary>
        public void ReloadAccounts()
        {
            Accounts = AccountApi.GetAccounts().Accounts;
        }

        /// <summary>
        /// Async version of the reload accounts
        /// </summary>
        public async Task ReloadAccountsAsync()
        {
            var accountsResponse = await AccountApi.GetAccountsAsync();
            Accounts = accountsResponse.Accounts;
        }

        /// <summary>
        /// Gett accounts associated with the connection
        /// </summary>
        /// <returns>The enumerable collection of accounts for the connection</returns>
        public IEnumerable<AccountProperties> GetAccounts()
        {
            return Accounts;
        }

        /// <summary>
        /// Get a specific account in the connection
        /// </summary>
        /// <param name="accountId">Id of the account to get</param>
        /// <returns>An account object</returns>
        public IAccount GetAccount(string accountId)
        {
            var account = Accounts.Where(account => account.Id == accountId).SingleOrDefault();
            if (account == null)
            {
                throw new NoSuchAccountException($"The {accountId} doesn't exist in the connection");
            }

            _accountsCache.TryGetValue(accountId, out var accountInCache);
            if (accountInCache == null)
            {
                var accountToCache = new Account.Account(this, accountId);
                _accountsCache.Add(accountId, accountToCache);
                return accountToCache;
            }
            else
            {
                return accountInCache;
            }
        }

        /// <summary>
        /// Get a specific instrument of the connection
        /// </summary>
        /// <param name="instrument">The name of the instrument</param>
        /// <returns>An instrument object</returns>
        public IInstrument GetInstrument(InstrumentName instrument)
        {
            _instrumentCache.TryGetValue(instrument, out var instrumentInCache);
            if (instrumentInCache == null)
            {
                var instrumentToCache = new Instrument.Instrument(this, instrument);
                _instrumentCache.Add(instrument, instrumentToCache);
                return instrumentToCache;
            }
            else
            {
                return instrumentInCache;
            }
        }
    }
}
