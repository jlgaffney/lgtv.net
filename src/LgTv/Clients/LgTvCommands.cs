namespace LgTv.Clients
{
    public static class LgTvCommands
    {
        #region Power Commands

        public static readonly LgTvCommand PowerOff = new LgTvCommand("ssap://system/turnOff");

        #endregion


        #region Audio Commands

        public static readonly LgTvCommand GetSoundOutput = new LgTvCommand("status", "ssap://com.webos.service.apiadapter/audio/getSoundOutput");
        
        public static readonly LgTvCommand GetVolume = new LgTvCommand("status", "ssap://audio/getVolume");

        public static readonly LgTvCommand GetVolumeIsMuted = new LgTvCommand("status", "ssap://audio/getStatus");

        public static readonly LgTvCommand SetVolumeUp = new LgTvCommand("volumeup", "ssap://audio/volumeUp");
        
        public static readonly LgTvCommand SetVolumeDown = new LgTvCommand("volumedown", "ssap://audio/volumeDown");
        
        public static readonly LgTvCommand SetVolume = new LgTvCommand("ssap://audio/setVolume");

        public static readonly LgTvCommand SetMute = new LgTvCommand("ssap://audio/setMute");

        #endregion


        #region Display Commands

        public static readonly LgTvCommand Set3DOn = new LgTvCommand("3d", "ssap://com.webos.service.tv.display/set3DOn");

        public static readonly LgTvCommand Set3DOff = new LgTvCommand("3d", "ssap://com.webos.service.tv.display/set3DOff");

        public static readonly LgTvCommand Get3DStatus = new LgTvCommand("status3D", "ssap://com.webos.service.tv.display/get3DStatus");

        #endregion


        #region Playback Commands

        public static readonly LgTvCommand MediaPlay = new LgTvCommand("play", "ssap://media.controls/play");

        public static readonly LgTvCommand MediaPause = new LgTvCommand("pause", "ssap://media.controls/pause");

        public static readonly LgTvCommand MediaStop = new LgTvCommand("stop", "ssap://media.controls/stop");

        public static readonly LgTvCommand MediaFastForward = new LgTvCommand("fastForward", "ssap://media.controls/fastForward");

        public static readonly LgTvCommand MediaRewind = new LgTvCommand("rewind", "ssap://media.controls/rewind");

        #endregion


        #region Keyboard Commands

        public static readonly LgTvCommand InsertText = new LgTvCommand("ssap://com.webos.service.ime/insertText");

        public static readonly LgTvCommand DeleteCharacters = new LgTvCommand("ssap://com.webos.service.ime/deleteCharacters");

        public static readonly LgTvCommand SendEnterKey = new LgTvCommand("ssap://com.webos.service.ime/sendEnterKey");

        #endregion


        #region Mouse Commands

        public static readonly LgTvCommand GetMouse = new LgTvCommand("ssap://com.webos.service.networkinput/getPointerInputSocket");

        #endregion


        #region Channels Commands

        public static readonly LgTvCommand GetChannels = new LgTvCommand("channels", "ssap://tv/getChannelList");

        public static readonly LgTvCommand GetCurrentChannel = new LgTvCommand("channels", "ssap://tv/getCurrentChannel");

        public static readonly LgTvCommand GetCurrentChannelProgramInfo = new LgTvCommand("programinfo", "ssap://tv/getChannelProgramInfo");

        public static readonly LgTvCommand SetChannelUp = new LgTvCommand("channelUp", "ssap://tv/channelUp");

        public static readonly LgTvCommand SetChannelDown = new LgTvCommand("channelDown", "ssap://tv/channelDown");

        public static readonly LgTvCommand SetChannel = new LgTvCommand("ssap://tv/openChannel");

        #endregion


        #region Input Commands

        public static readonly LgTvCommand GetInputs = new LgTvCommand("input", "ssap://tv/getExternalInputList");
        
        public static readonly LgTvCommand SetInput = new LgTvCommand("ssap://tv/switchInput");

        #endregion


        #region App Commands

        public static readonly LgTvCommand GetForegroundAppInfo = new LgTvCommand("ssap://com.webos.applicationManager/getForegroundAppInfo");

        public static readonly LgTvCommand GetApps = new LgTvCommand("launcher", "ssap://com.webos.applicationManager/listLaunchPoints");

        public static readonly LgTvCommand LaunchApp = new LgTvCommand("ssap://system.launcher/launch");

        public static readonly LgTvCommand OpenApp = new LgTvCommand("ssap://system.launcher/open");

        public static readonly LgTvCommand CloseApp = new LgTvCommand("ssap://system.launcher/close");

        #endregion


        #region Notification Commands

        public static readonly LgTvCommand ShowToast = new LgTvCommand("ssap://system.notifications/createToast");

        public static readonly LgTvCommand CloseToast = new LgTvCommand("ssap://system.notifications/closeToast");

        #endregion


        #region Info Commands

        public static readonly LgTvCommand GetCurrentTime = new LgTvCommand("ssap://com.webos.service.tv.time/getCurrentTime");
        
        public static readonly LgTvCommand GetSystemInfo = new LgTvCommand("ssap://system/getSystemInfo");
        
        public static readonly LgTvCommand GetSoftwareInfo = new LgTvCommand("ssap://com.webos.service.update/getCurrentSWInformation");
        
        public static readonly LgTvCommand GetConnectionInfo = new LgTvCommand("ssap://com.webos.service.connectionmanager/getinfo");
        
        public static readonly LgTvCommand GetServices = new LgTvCommand("ssap://api/getServiceList");

        #endregion
    }
}
