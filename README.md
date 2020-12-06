# OANDA .NET SDK
A fully typed .NET SDK for [OANDA's REST V20 API](https://developer.oanda.com/rest-live-v20/introduction/). The SDK uses the [oanda-dotnet-client](https://github.com/geriremenyi/oanda-dotnet-client) which is generated from the [oanda-openapi](https://github.com/geriremenyi/oanda-openapi) definition.

Based on your use case, you might consider using this simplified wrapper or the generated client library itself. You can also request a specific client generation under the openapi definition repo.

## Getting started

### Prerequisites

- [.NET Core 3.1 SDK](https://dotnet.microsoft.com/download) to build, run etc.
- [Visual Studio](https://visualstudio.microsoft.com) (optional) as the IDE.

### Local setup

1. Clone this repo
```bash
# HTTPS
https://github.com/geriremenyi/oanda-dotnet-sdk.git
# SSH
git@github.com:geriremenyi/oanda-dotnet-sdk.git
```

2. Navigate to the root of the project and restore nuget packages
```bash
dotnet restore src/GeriRemenyi.Oanda.V20.Sdk.sln
```

### Build

To build the SDK:
```bash
dotnet build src/GeriRemenyi.Oanda.V20.Sdk/GeriRemenyi.Oanda.V20.Sdk.csproj
```

### Playground

The easiest way to explore what the SDK offers if you open the [Playground project](https://github.com/geriremenyi/oanda-dotnet-sdk/tree/develop/src/GeriRemenyi.Oanda.V20.Sdk.Playground), start it and play around. You can try the SDK using the console app and you can also see some code examples there.

To run the playground:
```bash
dotnet run --project src/GeriRemenyi.Oanda.V20.Sdk.Playground/GeriRemenyi.Oanda.V20.Sdk.Playground.csproj
```

## How to use it

### Install nuget package
```bash
dotnet add package GeriRemenyi.Oanda.V20.Client
```

### Initialize a connection
```cs
var server = OandaConnectionType.FxPractice;
var token "<your_api_bearer_token>";
var connection = new OandaApiConnectionFactory().CreateConnection(server, token);
```

### Get accounts
```cs
connection.GetAccounts();
```

### Get account details
```cs
connection.GetAccount("<your_sub_account_id>").GetDetails();
// OR
await connection.GetAccount("<your_sub_account_id>").GetDetailsAsync();
```

### Get open trades
```cs
connection
    .GetAccount("<your_sub_account_id>")
    .GetOpenTrades();
// OR
await connection
    .GetAccount("<your_sub_account_id>")
    .GetOpenTradesAsync();
```

### Open a new trade
```cs
var instrument = InstrumentNames.EUR_USD;
var direction = TradeDirection.Long;
var units = 100000;
var trailingStopLossInPips = 100;
connection
    .GetAccount("<your_sub_account_id>")
    .OpenTrade(instrument, direction, units, trailingStopLossDistance);
// OR
await connection
    .GetAccount("<your_sub_account_id>")
    .OpenTradeAsync(instrument, direction, units, trailingStopLossDistance);
```

### Get candlesticks for an instruments between the given timefram
```cs
connection
    .GetInstrument(InstrumentName.EUR_USD)
    .GetCandlesByTime(CandlestickGranularity.H1, DateTime.Now.AddDays(-5), DateTime.Now);
// OR
await connection
    .GetInstrument(InstrumentName.EUR_USD)
    .GetCandlesByTimeAsync(CandlestickGranularity.H1, DateTime.Now.AddDays(-5), DateTime.Now);
```

### Get last N candlesticks for an instruments
```cs
connection
    .GetInstrument(InstrumentName.EUR_USD)
    .GetLastNCandles(CandlestickGranularity.H1, 100);
// OR
await connection
    .GetInstrument(InstrumentName.EUR_USD)
    .GetLastNCandlesAsync(CandlestickGranularity.H1, 100);
```

## Contributing
Pull requests and any kind of contribution are welcome. For major changes, please open an issue first to discuss what you would like to change.

## License
[MIT](https://choosealicense.com/licenses/mit/)

## :warning: Liability
CFDs are complex instruments and come with a high risk of losing money rapidly due to leverage.
Around 70% of retail investor accounts lose money when trading CFDs with OANDA.
You should consider whether you understand how CFDs work and whether you can afford to take the high risk of losing your money.

This is a project built for fun so by no means I'm held responsible for any of your financial losses even if it was caused by the software
malfunctioning. Please use this library with caution and ALWAYS test your algo trading strategies on a practice/demo account. Only use it on a live
account if you have learnt that there are nog bugs present at all.