# lgtv.net
LG TV WebOS API for .NET

Forked from https://github.com/gr4b4z/lgtv.net and updated to target .NET Standard 2.0.

## inspiration: 
* https://github.com/msloth/lgtv.js/blob/master/index.js

## references:
* https://github.com/ConnectSDK/Connect-SDK-Android-Core
* https://github.com/CODeRUS/harbour-lgremote-webos
* https://github.com/openwebos
* https://mym.hackpad.com/ep/pad/static/rLlshKkzdNj

## Usage
```C#
// Initalization
var client = new LgTvClient(new LgTvConnection(), new JsonFileClientKeyStore(ClientKeyStoreFilePath, TvHostname), TvHostname, TvPort);

await client.Connect();
await client.MakeHandShake();


// Volume control
await client.VolumeDown();
await client.VolumeUp();


// Playback control
await client.Pause();
await client.Play();


var apps = await client.GetApps();


using (var mouse = await client.GetMouse())
{
    mouse.SendButton(ButtonType.UP);
    mouse.SendButton(ButtonType.LEFT);
    mouse.SendButton(ButtonType.RIGHT);
    mouse.SendButton(ButtonType.DOWN);
}


await client.TurnOff();
```
