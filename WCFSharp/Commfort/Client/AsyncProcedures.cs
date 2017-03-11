using System.Drawing;
using System.Threading.Tasks;
using WCFSharp.Types;

namespace WCFSharp
{
    public static partial class Commfort
    {
        public static partial class Client
        {
            /// <summary>
            /// Prints message to events.
            /// </summary>
            /// <param name="Message">Message to print.</param>
            /// <param name="Style">Message style.</param>
            /// <param name="WithTime">Append time to message?</param>
            /// <param name="WriteToHistory">Write the message to history?</param>
            public static async Task PrintMessageToEventsAsync(string Message, EventMessageStyle Style = EventMessageStyle.Default, bool WithTime = true, bool WriteToHistory = true)
            {
                await Task.Factory.StartNew(() =>
                {
                    PrintMessageToEvents(Message, Style, WithTime, WriteToHistory);
                }, Commfort.TokenSource.Token, TaskCreationOptions.None, Commfort.Scheduler);
            }

            /// <summary>
            /// Sends message to channel. Note that you can't send MessageType.Picture.
            /// </summary>
            /// <param name="Channel">Channel to send to.</param>
            /// <param name="Message">Message to send.</param>
            /// <param name="Type"></param>
            /// <returns></returns>
            public static async Task SendMessageAsync(string Channel, string Message, MessageType Type = MessageType.Default)
            {
                await Task.Factory.StartNew(() =>
                {
                    SendMessage(Channel, Message, Type);
                }, Commfort.TokenSource.Token, TaskCreationOptions.None, Commfort.Scheduler);
            }

            /// <summary>
            /// Joins the channel. If channel doesn't exist, creates it with given flags.
            /// </summary>
            /// <param name="Channel">Channel to join/create.</param>
            /// <param name="ShowInChannelList">Should channel be visible in channel list?</param>
            /// <param name="InviteOnly">Should channel be only accessible via invites?</param>
            /// <returns></returns>
            public static async Task JoinChannelAsync(string Channel, bool ShowInChannelList = true, bool InviteOnly = true)
            {
                await Task.Factory.StartNew(() =>
                {
                    JoinChannel(Channel, ShowInChannelList, InviteOnly);
                }, Commfort.TokenSource.Token, TaskCreationOptions.None, Commfort.Scheduler);
            }

            /// <summary>
            /// Leaves the channel.
            /// </summary>
            /// <param name="Channel">Channel to leave.</param>
            /// <returns></returns>
            public static async Task LeaveChannelAsync(string Channel)
            {
                await Task.Factory.StartNew(() =>
                {
                    LeaveChannel(Channel);
                }, Commfort.TokenSource.Token, TaskCreationOptions.None, Commfort.Scheduler);
            }

            /// <summary>
            /// Leaves the private channel.
            /// </summary>
            /// <param name="Username">Channel to leave.</param>
            /// <returns></returns>
            public static async Task LeavePrivateChannelAsync(string Username)
            {
                await Task.Factory.StartNew(() =>
                {
                    LeavePrivateChannel(Username);
                }, Commfort.TokenSource.Token, TaskCreationOptions.None, Commfort.Scheduler);
            }

            /// <summary>
            /// Sends message to channel. Note that you can't send MessageType.Picture.
            /// </summary>
            /// <param name="Channel">Channel to send to.</param>
            /// <param name="Message">Message to send.</param>
            /// <param name="Type"></param>
            /// <returns></returns>
            public static async Task SendMessageAsync(Channel Channel, string Message, MessageType Type = MessageType.Default)
            {
                await SendMessageAsync(Channel.Name, Message, Type);
            }

            /// <summary>
            /// Sends private message to user.
            /// </summary>
            /// <param name="Username">User to send the message to.</param>
            /// <param name="Message">Actually, the message to send.</param>
            /// <param name="Type">Type of the message.</param>
            /// <returns></returns>
            public static async Task SendPrivateMessageAsync(string Username, string Message, MessageType Type = MessageType.Default)
            {
                await Task.Factory.StartNew(() =>
                {
                    SendPrivateMessage(Username, Message, Type);
                }, Commfort.TokenSource.Token, TaskCreationOptions.None, Commfort.Scheduler);
            }

            /// <summary>
            /// Sends private message to user.
            /// </summary>
            /// <param name="Username">User to send the message to.</param>
            /// <param name="Message">Actually, the message to send.</param>
            /// <param name="Type">Type of the message.</param>
            /// <returns></returns>
            public static async Task SendPrivateMessageAsync(User User, string Message, MessageType Type = MessageType.Default)
            {
                await SendPrivateMessageAsync(User.Name, Message, Type);
            }

            /// <summary>
            /// Sends picture to the channel. While you should use matching image format, I don't really know what does it do.
            /// </summary>
            /// <param name="Channel">The channel to send picture to.</param>
            /// <param name="Picture">Picture to send.</param>
            /// <param name="Format">Image format.</param>
            /// <returns></returns>
            public static async Task SendPictureAsync(string Channel, Image Picture, CommfortPictureFormat Format = CommfortPictureFormat.Jpeg)
            {
                await Task.Factory.StartNew(() =>
                {
                    SendPicture(Channel, Picture, Format);
                }, Commfort.TokenSource.Token, TaskCreationOptions.None, Commfort.Scheduler);
            }

            /// <summary>
            /// Sends picture to the channel. While you should use matching image format, I don't really know what does it do.
            /// </summary>
            /// <param name="Channel">The channel to send picture to.</param>
            /// <param name="Picture">Picture to send.</param>
            /// <param name="Format">Image format.</param>
            /// <returns></returns>
            public static async Task SendPrivatePictureAsync(string Username, Image Picture, CommfortPictureFormat Format = CommfortPictureFormat.Jpeg)
            {
                await Task.Factory.StartNew(() =>
                {
                    SendPrivatePicture(Username, Picture, Format);
                }, Commfort.TokenSource.Token, TaskCreationOptions.None, Commfort.Scheduler);
            }

            /// <summary>
            /// Changes the status of client. Also changes flags.
            /// </summary>
            /// <param name="NewStatus">The new status.</param>
            /// <param name="IgnoreImportantFlag">Should important message flag be ignored?</param>
            /// <param name="TurnOffSounds">Should sounds be muted?</param>
            /// <param name="TurnOffNotifications">Should notifications be muted?</param>
            /// <returns></returns>
            public static async Task ChangeStatusAsync(string NewStatus, bool IgnoreImportantFlag = false, bool TurnOffSounds = false, bool TurnOffNotifications = false)
            {
                await Task.Factory.StartNew(() =>
                {
                    ChangeStatus(NewStatus, IgnoreImportantFlag, TurnOffSounds, TurnOffNotifications);
                }, Commfort.TokenSource.Token, TaskCreationOptions.None, Commfort.Scheduler);
            }

            /// <summary>
            /// Changes channel topic. Obviously, you must be in channel.
            /// </summary>
            /// <param name="Channel">The channel to change topic of.</param>
            /// <param name="NewTopic">New topic.</param>
            /// <returns></returns>
            public static async Task ChangeChannelTopicAsync(string Channel, string NewTopic)
            {
                await Task.Factory.StartNew(() =>
                {
                    ChangeChannelTopic(Channel, NewTopic);
                }, Commfort.TokenSource.Token, TaskCreationOptions.None, Commfort.Scheduler);
            }

            /// <summary>
            /// Changes channel topic. Obviously, you must be in channel.
            /// </summary>
            /// <param name="Channel">The channel to change topic of.</param>
            /// <param name="NewTopic">New topic.</param>
            /// <returns></returns>
            public static async Task ChangeChannelTopicAsync(Channel Channel, string NewTopic)
            {
                await Task.Factory.StartNew(() =>
                {
                    ChangeChannelTopic(Channel.Name, NewTopic);
                }, Commfort.TokenSource.Token, TaskCreationOptions.None, Commfort.Scheduler);
            }

            /// <summary>
            /// Changes channel greeting. Obviously, you must be in channel.
            /// </summary>
            /// <param name="Channel">The channel to change topic of.</param>
            /// <param name="Greeting">New greeting.</param>
            /// <returns></returns>
            public static async Task ChangeChannelGreetingAsync(string Channel, string NewGreeting)
            {
                await Task.Factory.StartNew(() =>
                {
                    ChangeChannelGreeting(Channel, NewGreeting);
                }, Commfort.TokenSource.Token, TaskCreationOptions.None, Commfort.Scheduler);
            }

            /// <summary>
            /// Changes channel greeting. Obviously, you must be in channel.
            /// </summary>
            /// <param name="Channel">The channel to change topic of.</param>
            /// <param name="Greeting">New greeting.</param>
            /// <returns></returns>
            public static async Task ChangeChannelGreetingAsync(Channel Channel, string NewTopic)
            {
                await Task.Factory.StartNew(() =>
                {
                    ChangeChannelGreeting(Channel.Name, NewTopic);
                }, Commfort.TokenSource.Token, TaskCreationOptions.None, Commfort.Scheduler);
            }

            /// <summary>
            /// Clears the channel. Obviously, only for you.
            /// </summary>
            /// <param name="Channel">The channel to clear/</param>
            /// <returns></returns>
            public static async Task ClearChannelAsync(string Channel)
            {
                await Task.Factory.StartNew(() =>
                {
                    ClearChannel(Channel);
                }, Commfort.TokenSource.Token, TaskCreationOptions.None, Commfort.Scheduler);
            }

            /// <summary>
            /// Clears the channel. Obviously, only for you.
            /// </summary>
            /// <param name="Channel">The channel to clear/</param>
            /// <returns></returns>
            public static async Task ClearChannelAsync(Channel Channel)
            {
                await Task.Factory.StartNew(() =>
                {
                    ClearChannel(Channel.Name);
                }, Commfort.TokenSource.Token, TaskCreationOptions.None, Commfort.Scheduler);
            }

            /// <summary>
            /// Sends data to server's virtual user.
            /// </summary>
            /// <param name="Receiver">Name of virtual user.</param>
            /// <param name="Data">Data to send.</param>
            /// <returns></returns>
            public static async Task SendDataToServerAsync(string Receiver, byte[] Data)
            {
                await Task.Factory.StartNew(() =>
                {
                    SendDataToServer(Receiver, Data);
                }, Commfort.TokenSource.Token, TaskCreationOptions.None, Commfort.Scheduler);
            }
        }
    }
}