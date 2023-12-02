namespace LgTv.Clients;

public static class LgTvCommands
{
    #region Power Commands

    public static readonly LgTvCommand PowerOff = new("ssap://system/turnOff");

    #endregion


    #region Audio Commands

    public static readonly LgTvCommand GetSoundOutput = new("status", "ssap://com.webos.service.apiadapter/audio/getSoundOutput");

    public static readonly LgTvCommand GetVolume = new("status", "ssap://audio/getVolume");

    public static readonly LgTvCommand GetVolumeIsMuted = new("status", "ssap://audio/getStatus");

    public static readonly LgTvCommand SetVolumeUp = new("volumeup", "ssap://audio/volumeUp");
    
    public static readonly LgTvCommand SetVolumeDown = new("volumedown", "ssap://audio/volumeDown");
    
    public static readonly LgTvCommand SetVolume = new("ssap://audio/setVolume");

    public static readonly LgTvCommand SetMute = new("ssap://audio/setMute");

    #endregion


    #region Display Commands

    public static readonly LgTvCommand Set3DOn = new("3d", "ssap://com.webos.service.tv.display/set3DOn");

    public static readonly LgTvCommand Set3DOff = new("3d", "ssap://com.webos.service.tv.display/set3DOff");

    public static readonly LgTvCommand Get3DStatus = new("status3D", "ssap://com.webos.service.tv.display/get3DStatus");

    #endregion


    #region Playback Commands

    public static readonly LgTvCommand MediaPlay = new("play", "ssap://media.controls/play");

    public static readonly LgTvCommand MediaPause = new("pause", "ssap://media.controls/pause");

    public static readonly LgTvCommand MediaStop = new("stop", "ssap://media.controls/stop");

    public static readonly LgTvCommand MediaFastForward = new("fastForward", "ssap://media.controls/fastForward");

    public static readonly LgTvCommand MediaRewind = new("rewind", "ssap://media.controls/rewind");

    #endregion


    #region Keyboard Commands

    public static readonly LgTvCommand InsertText = new("ssap://com.webos.service.ime/insertText");

    public static readonly LgTvCommand DeleteCharacters = new("ssap://com.webos.service.ime/deleteCharacters");

    public static readonly LgTvCommand SendEnterKey = new("ssap://com.webos.service.ime/sendEnterKey");

    #endregion


    #region Mouse Commands

    public static readonly LgTvCommand GetMouse = new("ssap://com.webos.service.networkinput/getPointerInputSocket");

    #endregion


    #region Channels Commands

    public static readonly LgTvCommand GetChannels = new("channels", "ssap://tv/getChannelList");

    public static readonly LgTvCommand GetCurrentChannel = new("channels", "ssap://tv/getCurrentChannel");

    public static readonly LgTvCommand GetCurrentChannelProgramInfo = new("programinfo", "ssap://tv/getChannelProgramInfo");

    public static readonly LgTvCommand SetChannelUp = new("channelUp", "ssap://tv/channelUp");

    public static readonly LgTvCommand SetChannelDown = new("channelDown", "ssap://tv/channelDown");

    public static readonly LgTvCommand SetChannel = new("ssap://tv/openChannel");

    #endregion


    #region Input Commands

    public static readonly LgTvCommand GetInputs = new("input", "ssap://tv/getExternalInputList");
    
    public static readonly LgTvCommand SetInput = new("ssap://tv/switchInput");

    #endregion


    #region App Commands

    public static readonly LgTvCommand GetForegroundAppInfo = new("ssap://com.webos.applicationManager/getForegroundAppInfo");

    public static readonly LgTvCommand GetApps = new("launcher", "ssap://com.webos.applicationManager/listLaunchPoints");

    public static readonly LgTvCommand LaunchApp = new("ssap://system.launcher/launch");

    public static readonly LgTvCommand OpenApp = new("ssap://system.launcher/open");

    public static readonly LgTvCommand CloseApp = new("ssap://system.launcher/close");

    #endregion


    #region Notification Commands

    public static readonly LgTvCommand ShowToast = new("ssap://system.notifications/createToast");

    public static readonly LgTvCommand CloseToast = new("ssap://system.notifications/closeToast");

    #endregion


    #region Info Commands

    public static readonly LgTvCommand GetCurrentTime = new("ssap://com.webos.service.tv.time/getCurrentTime");
    
    public static readonly LgTvCommand GetSystemInfo = new("ssap://system/getSystemInfo");
    
    public static readonly LgTvCommand GetSoftwareInfo = new("ssap://com.webos.service.update/getCurrentSWInformation");
    
    public static readonly LgTvCommand GetConnectionInfo = new("ssap://com.webos.service.connectionmanager/getinfo");
    
    public static readonly LgTvCommand GetServices = new("ssap://api/getServiceList");

    #endregion
}
