using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using LeoServer.Core;
using LeoServer.Tool;

namespace LeoServer.Client
{
    public class TcpClientConnection : IClientConnection
    {
        public string Id { get; }
        public bool IsConnected { get; private set; }

        private readonly TcpClient _client;
        private NetworkStream _stream;

        public TcpClientConnection(TcpClient client)
        {
            _client = client;
            _stream = _client.GetStream();
            Id = Guid.NewGuid().ToString();
            IsConnected = true;
        }

        public async void StartReceiving(Action<IClientConnection, string> onMessage, Action onDisconnect)
        {
            var buffer = new byte[1024];
            try
            {
                while (IsConnected)
                {
                    int length = await _stream.ReadAsync(buffer, 0, buffer.Length);
                    if (length == 0) break; // 연결 끊김
                    string message = Encoding.UTF8.GetString(buffer, 0, length);
                    onMessage?.Invoke(this, message);
                }
            }
            catch
            {
                // 연결 오류 처리
            }
            finally
            {
                IsConnected = false;
                onDisconnect?.Invoke();
            }
        }

        public async void SendMessage(string message)
        {
            if (!IsConnected) return;
            var data = Encoding.UTF8.GetBytes(message);
            await _stream.WriteAsync(data, 0, data.Length);
        }

        public void Connected()
        {
            Logger.Log($"Client Connected");
        }

        public void Disconnect()
        {
            Logger.Log($"Client Disconnected");
        }
    }
}
