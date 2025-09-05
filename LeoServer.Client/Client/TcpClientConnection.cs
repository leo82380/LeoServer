using System;
using System.Net.Sockets;
using System.Text;
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

        /// <summary>
        /// 클라이언트의 메시지 전송 및 연결 해제 관리 함수
        /// </summary>
        /// <param name="onMessage">메시지 전송 이벤트</param>
        /// <param name="onDisconnect">연결 해제 이벤트</param>
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
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
            }
            finally
            {
                IsConnected = false;
                onDisconnect?.Invoke();
            }
        }

        /// <summary>
        /// 메시지 전송 이벤트
        /// </summary>
        /// <param name="message"></param>
        public async void SendMessage(string message)
        {
            if (!IsConnected) return;
            var data = Encoding.UTF8.GetBytes(message);
            await _stream.WriteAsync(data, 0, data.Length);
        }

        /// <summary>
        /// 서버에 연결되었을 때 호출되는 함수
        /// </summary>
        public void Connected()
        {
            Logger.Log($"Client Connected");
        }

        /// <summary>
        /// 서버에 연결이 해제되었을 때 호출되는 함수
        /// </summary>
        public void Disconnect()
        {
            Logger.Log($"Client Disconnected");
        }
    }
}
