//----------------------------------------------------------------------------------------
// <copyright file="ConnectionType.cs" company="geriremenyi.com">
//     Author: Gergely Reményi
//     Copyright (c) geriremenyi.com. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------------------

namespace GeriRemenyi.Oanda.V20.Sdk.Common.Types
{
    /// <summary>
    /// Different kinds of connections available
    /// </summary>
    public enum OandaConnectionType
    {
        /// <summary>
        /// Practice connection
        /// Connects to the practice server
        /// </summary>
        FxPractice,

        /// <summary>
        /// Trade connections
        /// Connects to the live trading server
        /// </summary>
        FxTrade
    }
}
