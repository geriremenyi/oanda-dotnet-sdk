namespace GeriRemenyi.Oanda.V20.Sdk
{
    using GeriRemenyi.Oanda.V20.Client.Api;
    using GeriRemenyi.Oanda.V20.Client.Client;
    using ClientModel = GeriRemenyi.Oanda.V20.Client.Model;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using GeriRemenyi.Oanda.V20.Sdk.Account;
    using GeriRemenyi.Oanda.V20.Sdk.Instrument;
    using GeriRemenyi.Oanda.V20.Client.Model;

    /// <summary>
    /// Interface for Oanda API V20 connection
    /// </summary>
    public interface IOandaApiConnection
    {
        /// <summary>
        /// Configuration object for the connection
        /// </summary>
        public abstract Configuration Configuration { get; }

        /// <summary>
        /// Date time format to use for the connection
        /// </summary>
        public abstract ClientModel.DateTimeFormat DateTimeFormat { get; }

        /// <summary>
        /// Actual Oanda account API functions
        /// </summary>
        public abstract IAccountApi AccountApi { get; }

        /// <summary>
        /// Actual Oanda instrument API functions
        /// </summary>
        public abstract IInstrumentApi InstrumentApi { get; }

        /// <summary>
        /// Actual Oanda order API functions
        /// </summary>
        public abstract IOrderApi OrderApi { get; }

        /// <summary>
        /// Actual Oanda trade API functions
        /// </summary>
        public abstract ITradeApi TradeApi { get; }

        /// <summary>
        /// Actual Oanda position API functions
        /// </summary>
        public abstract IPositionApi PositionApi { get; }

        /// <summary>
        /// Actual Oanda transaction API functions
        /// </summary>
        public abstract ITransactionApi TransactionApi { get; }

        /// <summary>
        /// Actual Oanda pricing API functions
        /// </summary>
        public abstract IPricingApi PricingApi { get; }

        /// <summary>
        /// Public getter for the accounts associated with this connection
        /// </summary>
        public abstract IEnumerable<ClientModel.AccountProperties> Accounts { get; }

        /// <summary>
        /// Explicitly ask for the reload of available accounts in the connection
        /// This way a connection doesn't have to be disposed and recreated if there
        /// were changes in the available accounts for the connection
        /// </summary>
        public void ReloadAccounts();

        /// <summary>
        /// Async version of the reload accounts
        /// </summary>
        public Task ReloadAccountsAsync();

        /// <summary>
        /// Gett accounts associated with the connection
        /// </summary>
        /// <returns>The enumerable collection of accounts for the connection</returns>
        public IEnumerable<ClientModel.AccountProperties> GetAccounts();

        /// <summary>
        /// Get a specific account in the connection
        /// </summary>
        /// <param name="accountId">Id of the account to get</param>
        /// <returns>An account object</returns>
        public IAccount GetAccount(string accountId);

        /// <summary>
        /// Get a specific instrument of the connection
        /// </summary>
        /// <param name="instrument">The name of the instrument</param>
        /// <returns>An instrument object</returns>
        public IInstrument GetInstrument(InstrumentName instrument);
    }
}
