using LeoServer.Core;
using LeoServer.Tool;

namespace LeoServer.Runtime
{
    public class MainServer : IServer, IDisposable
    {
        public bool IsRunning { get; private set; }

        private ServerEventHandler _eventHandler;
        private ITransport _transport;

        public void Initialize(ITransport transport)
        {
            IsRunning = false;
            _transport = transport;
            _eventHandler = new(_transport);
            _eventHandler.AddEvents();
            AddEvents();
        }

        public void Start()
        {
            if (IsRunning) return;

            IsRunning = true;
            Logger.Log("Server Started!");
            _transport.Start();
        }

        public void Stop()
        {
            if (!IsRunning) return;

            IsRunning = false;
            Logger.Log("Server Stopped!");
            _transport.Stop();
        }

        public void Dispose()
        {
            Stop();
            _eventHandler.Dispose(); // 중복 제거됨
        }

        private void AddEvents()
        {
            _eventHandler.OnClientConnection += HandleClientConnection;
            _eventHandler.OnMessageReceived += HandleMessageReceived;
        }

        private void HandleMessageReceived(IClientConnection connection, string message)
        {
            Logger.Log($"[Client {connection.Id}] {message}");
        }

        private void HandleClientConnection(IClientConnection client, bool isConnected)
        {
            if (isConnected)
                Logger.Log($"Client {client.Id} Connected!");
            else
                Logger.Log($"Client {client.Id} Disconnected!");
        }
    }
}
