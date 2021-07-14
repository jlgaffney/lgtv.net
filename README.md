# lgtv.net
LG TV WebOS API for .NET

Forked from https://github.com/gr4b4z/lgtv.net and updated to target .NET Standard 2.0.

## inspiration: 
* https://github.com/msloth/lgtv.js/blob/master/index.js

## references:
* https://github.com/ConnectSDK/Connect-SDK-Android-Core
* https://github.com/CODeRUS/harbour-lgremote-webos
* https://github.com/openwebos
* https://github.com/supersaiyanmode/PyWebOSTV
* https://github.com/klattimer/LGWebOSRemote
* https://mym.hackpad.com/ep/pad/static/rLlshKkzdNj

## Usage
```C#
// Initialization
var client = new LgTvClient(
    () => new LgTvConnection(),
    new JsonFileClientKeyStore(ClientKeyStoreFilePath),
    SecureConnection, TvHost, TvPort);

await client.Connect();
await client.MakeHandShake();


// Volume control
await client.Audio.VolumeDown();
await client.Audio.VolumeUp();


// Playback control
await client.Playback.Pause();
await client.Playback.Play();


var channels = await client.Channels.GetChannels();

var inputs = await client.Inputs.GetInputs();

var apps = await client.Apps.GetApps();


using (var mouse = await client.GetMouse())
{
    await mouse.SendButton(ButtonType.UP);
    await mouse.SendButton(ButtonType.LEFT);
    await mouse.SendButton(ButtonType.RIGHT);
    await mouse.SendButton(ButtonType.DOWN);
}


await client.Power.TurnOff();
```
