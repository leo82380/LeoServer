namespace LeoServer.Core
{
    public interface ITransport
    {
        event Action<IClientConnection> ClientConnected;
        event Action<IClientConnection> ClientDisconnected;
        event Action<IClientConnection, string> MessageReceived;

        void Start();
        void Stop();
    }
}