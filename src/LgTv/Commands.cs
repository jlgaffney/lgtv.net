namespace LgTv;

internal static class Commands
{
    #region Power Commands

    public static readonly Command PowerOff = new("ssap://system/turnOff");

    #endregion


    #region Audio Commands

    public static readonly Command GetSoundOutput = new("status", "ssap://com.webos.service.apiadapter/audio/getSoundOutput");

    // TODO ssap://com.webos.service.apiadapter/audio/changeSoundOutput

    public static readonly Command GetAudioStatus = new("status", "ssap://audio/getStatus");

    public static readonly Command SetVolumeUp = new("volumeup", "ssap://audio/volumeUp");

    public static readonly Command SetVolumeDown = new("volumedown", "ssap://audio/volumeDown");

    public static readonly Command SetVolume = new("ssap://audio/setVolume");

    public static readonly Command SetMute = new("ssap://audio/setMute");

    #endregion


    #region Playback Commands

    public static readonly Command MediaPlay = new("play", "ssap://media.controls/play");

    public static readonly Command MediaPause = new("pause", "ssap://media.controls/pause");

    public static readonly Command MediaStop = new("stop", "ssap://media.controls/stop");

    public static readonly Command MediaFastForward = new("fastForward", "ssap://media.controls/fastForward");

    public static readonly Command MediaRewind = new("rewind", "ssap://media.controls/rewind");

    #endregion


    #region Keyboard Commands

    public static readonly Command InsertText = new("ssap://com.webos.service.ime/insertText");

    public static readonly Command DeleteCharacters = new("ssap://com.webos.service.ime/deleteCharacters");

    public static readonly Command SendEnterKey = new("ssap://com.webos.service.ime/sendEnterKey");

    #endregion


    #region Mouse Commands

    public static readonly Command GetMouse = new("ssap://com.webos.service.networkinput/getPointerInputSocket");

    #endregion


    #region Channels Commands

    public static readonly Command GetChannels = new("channels", "ssap://tv/getChannelList");

    public static readonly Command GetCurrentChannel = new("channels", "ssap://tv/getCurrentChannel");

    public static readonly Command SetChannelUp = new("channelUp", "ssap://tv/channelUp");

    public static readonly Command SetChannelDown = new("channelDown", "ssap://tv/channelDown");

    public static readonly Command SetChannel = new("ssap://tv/openChannel");

    #endregion


    #region Input Commands

    public static readonly Command GetInputs = new("input", "ssap://tv/getExternalInputList");

    public static readonly Command SetInput = new("ssap://tv/switchInput");

    #endregion


    #region App Commands

    public static readonly Command GetForegroundAppInfo = new("ssap://com.webos.applicationManager/getForegroundAppInfo");

    public static readonly Command GetApps = new("launcher", "ssap://com.webos.applicationManager/listLaunchPoints");

    public static readonly Command LaunchApp = new("ssap://system.launcher/launch");

    public static readonly Command OpenApp = new("ssap://system.launcher/open");

    public static readonly Command CloseApp = new("ssap://system.launcher/close");

    // TODO ssap://com.webos.service.appstatus/getAppStatus // 404 no such service or method

    #endregion


    #region Notification Commands

    public static readonly Command ShowToast = new("ssap://system.notifications/createToast");

    public static readonly Command CloseToast = new("ssap://system.notifications/closeToast");

    #endregion


    #region Info Commands

    public static readonly Command GetCurrentTime = new("ssap://com.webos.service.tv.time/getCurrentTime");

    public static readonly Command GetSystemInfo = new("ssap://system/getSystemInfo");

    public static readonly Command GetSoftwareInfo = new("ssap://com.webos.service.update/getCurrentSWInformation");

    public static readonly Command GetConnectionInfo = new("ssap://com.webos.service.connectionmanager/getinfo");

    public static readonly Command GetServices = new("ssap://api/getServiceList");

    #endregion
}
