using LeoServer.Core;

namespace LeoServer.Runtime
{
    public delegate void ClientConnectionEvent(IClientConnection client, bool isConnected);

    public class ServerEventHandler : IDisposable
    {
        private readonly ITransport _transport;

        public event ClientConnectionEvent OnClientConnection;
        public event Action<IClientConnection, string> OnMessageReceived;

        public ServerEventHandler(ITransport transport)
        {
            _transport = transport;
        }

        public void AddEvents()
        {
            _transport.ClientConnected += HandleClientConnected;
            _transport.ClientDisconnected += HandleClientDisconnected;
            _transport.MessageReceived += HandleMessageReceived;
        }

        private void HandleMessageReceived(IClientConnection client, string message)
            => OnMessageReceived?.Invoke(client, message);

        private void HandleClientDisconnected(IClientConnection client)
            => OnClientConnection?.Invoke(client, false);

        private void HandleClientConnected(IClientConnection client)
            => OnClientConnection?.Invoke(client, true);

        public void Dispose()
        {
            _transport.ClientConnected -= HandleClientConnected;
            _transport.ClientDisconnected -= HandleClientDisconnected;
            _transport.MessageReceived -= HandleMessageReceived;
        }
    }
}
