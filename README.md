# OANDA .NET SDK
A fully typed .NET SDK for [OANDA's REST V20 API](https://developer.oanda.com/rest-live-v20/introduction/). The SDK uses the [oanda-dotnet-client](https://github.com/geriremenyi/oanda-dotnet-client) which is generated from the [oanda-openapi](https://github.com/geriremenyi/oanda-openapi) definition.

Based on your use case, you might consider using this simplified wrapper or the generated client library itself. You can also request a specific client generation under the openapi definition repo.

## Playground
The easiest way to explore what the SDK offers if you open the [Playground project](https://github.com/geriremenyi/oanda-dotnet-sdk/tree/develop/src/GeriRemenyi.Oanda.V20.Sdk.Playground), start it and play around. You can try the SDK using the console app and you can also see some code examples there.

## How to use it

### Install nuget package
```bash
dotnet add package GeriRemenyi.Oanda.V20.Client --version 0.0.2
```

### Initialize a connection
```cs
var server = OandaServer.FxPractice // OandaServer.FxTrade
var token "<your_api_bearer_token>"
var connection = new ApiConnection(server, token);
```

### Get accounts
```cs
await connection.GetAccounts();
```

### Get account details
```cs
await connection.GetAccount("<your_sub_account_id>").GetDetails()
```

### Get candlesticks for an instruments
```cs
await connection
    .GetInstrument(InstrumentName.EUR_USD)
    .GetCandles(CandlestickGranularity.H1, DateTime.Now.AddDays(-5), DateTime.Now);
```

## Contributing
Pull requests and any kind of contribution are welcome. For major changes, please open an issue first to discuss what you would like to change.

## License
[MIT](https://choosealicense.com/licenses/mit/)

## Liability
CFDs are complex instruments and come with a high risk of losing money rapidly due to leverage.
Around 70% of retail investor accounts lose money when trading CFDs with OANDA.
You should consider whether you understand how CFDs work and whether you can afford to take the high risk of losing your money.

This is a project built for fun so by no means I'm held responsible for any of your financial losses even if it was caused by the software
malfunctioning. Please use this library with caution and ALWAYS test your algo trading strategies on a practice/demo account. Only use it on a live
account if you have learnt that there are nog bugs present at all.