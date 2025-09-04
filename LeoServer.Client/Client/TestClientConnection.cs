using LeoServer.Core;
using LeoServer.Tool;

namespace LeoServer.Test.Client
{
    public class TestClientConnection : IClientConnection
    {
        public string Id { get; }

        public bool IsConnected { get; private set; }

        public TestClientConnection(string id)
        {
            Id = id;
        }

        public void Connected()
        {
            if (IsConnected)
            {
                Logger.LogWarning($"Client {Id} is Already Connected!");
                return;
            }
            IsConnected = true;
        }

        public void Disconnect()
        {
            if (!IsConnected)
            {
                Logger.LogWarning($"Client {Id} is Already Disconnected!");
                return;
            }
            IsConnected = false;
        }

        public void SendMessage(string message)
        {
            Logger.Log($"[To Client {Id}] {message}");
        }
    }
}