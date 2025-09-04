namespace LeoServer.Core
{
    public interface IClientConnection
    {
        string Id { get; }
        bool IsConnected { get; }

        void SendMessage(string message);
        void Connected();
        void Disconnect();
    }
}