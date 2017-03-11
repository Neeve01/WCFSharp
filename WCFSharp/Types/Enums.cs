namespace WCFSharp.Types
{
    public enum PluginSide
    {
        Server = 0,
        Client = 1
    }

    public enum EventMessageStyle
    {
        Default = 0,
        System = 1,
        Error = 2
    }

    public enum UserIcon
    {
        Male = 0,
        Female = 1,
        Unknown = 2
    }

    public enum MessageType
    {
        Default = 0,
        Status = 1,
        Picture = 2,
        PersonalMessage = 3
    }

    public enum CommfortPictureFormat
    {
        Bitmap = 0,
        Jpeg = 1,
        PNG = 2
    }

    public enum ConnectionState
    {
        Disconnect = 0,
        Connect = 1,
        Auth = 2
    }
}
