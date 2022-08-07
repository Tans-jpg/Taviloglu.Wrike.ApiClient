# Taviloglu.Wrike.ApiClient [![Build Status](https://travis-ci.org/staviloglu/Taviloglu.Wrike.ApiClient.svg?branch=master)](https://travis-ci.org/staviloglu/Taviloglu.Wrike.ApiClient/) [![BCH compliance](https://bettercodehub.com/edge/badge/staviloglu/Taviloglu.Wrike.ApiClient?branch=master)](https://bettercodehub.com/)

.NET Client for Wrike API. Feel free to show your ❤️ by giving a ⭐ and / or &nbsp;  [![](https://img.shields.io/static/v1?label=sponsoring&message=%E2%9D%A4&logo=GitHub&color=%23fe8e86)](https://github.com/sponsors/staviloglu) &nbsp; my coffe expenses

## Client Usage [![NuGet](https://img.shields.io/nuget/v/Taviloglu.Wrike.ApiClient.svg)](https://www.nuget.org/packages/Taviloglu.Wrike.ApiClient/) [![NuGet Downloads](https://img.shields.io/nuget/dt/Taviloglu.Wrike.ApiClient.svg)](https://www.nuget.org/packages/Taviloglu.Wrike.ApiClient/)
Create your Wrike Client and just call the function you need.
### Create client with permanent token
```csharp
var client = new WrikeClient("permanent_token");
```
### Create client with ClientId, ClientSecret, AuthorizationCode (Use oAuth2.0)
```csharp
//create the authorization url
var authorizationUrl = WrikeClient.GetAuthorizationUrl(
                ClientId,
                redirectUri: "http://localhost",
                state: "myTestState",
                scope: new List<string> { WrikeScopes.Default, WrikeScopes.wsReadWrite });
//After the user authorizes your client using the authroization url, 
//wrike will redirect user to the provided redirect_uri with the authorization code
//See https://developers.wrike.com/documentation/oauth2 for more details

//create client
var client = new WrikeClient(new WrikeAccessTokenRequest(
                ClientId,
                ClientSecret,
                AuthorizationCode), "redirect_uri");

//refresh token if needed
client.RefreshToken();
```
### Create client with AccessToken and Host
```csharp
var accesTokenResponse = WrikeClient.GetAccesToken(new WrikeAccessTokenRequest(
                ClientId,
                ClientSecret,
                AuthorizationCode), "http://localhost");

var client = new WrikeClient(accesTokenResponse.AccessToken, accesTokenResponse.Host);

//you need new client when you need to refresh token
var refreshTokenResponse = WrikeClient.RefreshToken(ClientId, ClientSecret, 
                accesTokenResponse.RefreshToken, accesTokenResponse.Host);

client = new WrikeClient(refreshTokenResponse.AccessToken, accesTokenResponse.Host);
```

### CRUD operations
```csharp
//create client
var client = new WrikeClient("permanent_token");

//get the list of custom fields
//https://developers.wrike.com/documentation/api/methods/query-custom-fields
var customFields = await client.CustomFields.GetAsync();

//create new custom field
//https://developers.wrike.com/documentation/api/methods/create-custom-field
var newCustomField = new WrikeCustomField("Title", WrikeCustomFieldType.Text);
newCustomField = await client.CustomFields.CreateAsync(newCustomField);

//delete a task
await client.Tasks.DeleteAsync("taskId");
```
### Provide custom HttpClient instance
By default, we use the classic `new HttpClient()` way of instantiating our own instance of `HttpClient`. We do, however, provide a way for you to pass in your own customized implementation of it so that you can use whatever custom Policies, Throttling, etc. that you require.
The recommended way of creating `HttpClients` is using `IHttpClientFactory` as specified by the official Microsoft docs [here](https://docs.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests)

In order to provide your own instance of HttpClient for this library to use, you simply need to provide it in the constructor for `WrikeHttpClient`:
```c#
var client = new WrikeClient("permanent_token", customHttpClient: customHttpClientImplementation);
```

For more details on usage checkout the [Taviloglu.Wrike.ApiClient.Samples](Taviloglu.Wrike.ApiClient.Samples) project
