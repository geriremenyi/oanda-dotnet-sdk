namespace GeriRemenyi.Oanda.V20.Sdk.Account
{
    using GeriRemenyi.Oanda.V20.Sdk.Trade;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ClientModel = Client.Model;

    /// <summary>
    /// Interface for actions available on an account
    /// </summary>
    public interface IAccount
    {

        /// <summary>
        /// Get the full details of an account
        /// </summary>
        /// <returns>Full details of the account</returns>
        public ClientModel.Account GetDetails();

        /// <summary>
        /// Get the full details of an account asynchronously
        /// </summary>
        /// <returns>Full details of the account</returns>
        public Task<ClientModel.Account> GetDetailsAsync();

        /// <summary>
        /// Get summary of an account
        /// </summary>
        /// <returns>The summary of the account</returns>
        public ClientModel.AccountSummary GetSummary();

        /// <summary>
        /// Get summary of an account asynchronously
        /// </summary>
        /// <returns>The summary of the account</returns>
        public Task<ClientModel.AccountSummary> GetSummaryAsync();

        /// <summary>
        /// Get the tradeable instruments for the account
        /// </summary>
        /// <returns>The enumerable collection of tradeable instruments for the account</returns>
        public IEnumerable<ClientModel.Instrument> GetTradeableInstruments();

        /// <summary>
        /// Get the tradeable instruments for the account asynchronously
        /// </summary>
        /// <returns>The enumerable collection of tradeable instruments for the account</returns>
        public Task<IEnumerable<ClientModel.Instrument>> GetTradeableInstrumentsAsync();

        /// <summary>
        /// Get changes for the account
        /// </summary>
        /// <returns>The different enumerable collections for the changes done in the account</returns>
        public ClientModel.AccountChanges GetChanges();

        /// <summary>
        /// Get changes for the account asynchronously
        /// </summary>
        /// <returns>The different enumerable collections for the changes done in the account</returns>
        public Task<ClientModel.AccountChanges> GetChangesAsync();

        /// <summary>
        /// Trade actions asssociated with this account
        /// </summary>
        public abstract ITrades Trades { get; }
    }
}
