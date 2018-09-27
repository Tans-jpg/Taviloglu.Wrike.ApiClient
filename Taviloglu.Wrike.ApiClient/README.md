# Taviloglu.Wrike.ApiClient [![Build Status](https://travis-ci.org/staviloglu/Taviloglu.Wrike.ApiClient.svg?branch=master)](https://travis-ci.org/staviloglu/Taviloglu.Wrike.ApiClient/)
.NET Client for Wrike API 

:boom: Latest working client (v0.89.0-alpha) works with Wrike API v3 and is not uploaded to NuGet. You can download the binaries and the source code from [Releases](https://github.com/staviloglu/Taviloglu.Wrike.ApiClient/releases/tag/v0.89.0-alpha) on github.

:boom: master branch is now under development for supporting Wrike API v4

## Donate
If you find this library useful and if it saved you time please think about supporting my work, I will appreciate that.
<a href="https://www.buymeacoffee.com/staviloglu" target="_blank"><img src="https://www.buymeacoffee.com/assets/img/custom_images/black_img.png" alt="Buy Me A Coffee" style="height: auto !important;width: auto !important;" ></a>

## Client Usage [![NuGet](https://img.shields.io/nuget/v/Taviloglu.Wrike.ApiClient.svg)](https://www.nuget.org/packages/Taviloglu.Wrike.ApiClient/)[![NuGet Downloads](https://img.shields.io/nuget/dt/Taviloglu.Wrike.ApiClient.svg)](https://www.nuget.org/packages/Taviloglu.Wrike.ApiClient/)
Create your Wrike Client with your permanent token and just call the function you need.
```csharp
//create client
var bearerToken = "your_permanent_token";
var wrikeClient = new WrikeClient(bearerToken);

//get the list of custom fields
//https://developers.wrike.com/documentation/api/methods/query-custom-fields
var customFields = await wrikeClient.CustomFields.GetAsync();

//create new custom field
//https://developers.wrike.com/documentation/api/methods/create-custom-field
var newCustomField = new WrikeCustomField
{
    AccountId = "IEABX2HE",
    Title = "Sinan's custom field",
    Type = WrikeCustomFieldType.Duration
};
newCustomField = await wrikeClient.CustomFields.CreateAsync(newCustomField);
```
For more details on usage checkout the [Taviloglu.Wrike.ApiClient.Samples](Taviloglu.Wrike.ApiClient.Samples) project