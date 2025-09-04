using LeoServer.Core;
using LeoServer.Test.Client;

namespace LeoServer.Test.Transport
{
    public class TestTransport : ITransport
    {
        public event Action<IClientConnection> ClientConnected;
        public event Action<IClientConnection> ClientDisconnected;
        public event Action<IClientConnection, string> MessageReceived;

        private TestClientConnection _testClient;

        public void Start()
        {
            _testClient = new("0");
            _testClient.Connected();
            ClientConnected?.Invoke(_testClient);
            MessageReceived?.Invoke(_testClient, "Test Message");
        }

        public void Stop()
        {
            if (_testClient != null)
            {
                _testClient.Disconnect();
                ClientDisconnected?.Invoke(_testClient);
                _testClient = null;
            }
        }
    }
}