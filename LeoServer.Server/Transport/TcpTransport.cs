using System.Net;
using System.Net.Sockets;
using LeoServer.Client;
using LeoServer.Core;
using LeoServer.Tool;

namespace LeoServer.Transport
{
    public class TcpTransport : ITransport
    {
        public event Action<IClientConnection> ClientConnected;
        public event Action<IClientConnection> ClientDisconnected;
        public event Action<IClientConnection, string> MessageReceived;

        /// <summary>
        /// Tcp 서버를 돌리기 위한 리스너
        /// </summary>
        private TcpListener _listener;
        private bool _isRunning = false;

        public void Start()
        {
            // Tcp 리스너 초기화
            _listener = new TcpListener(IPAddress.Any, 7777);
            _listener.Start();
            _isRunning = true;
            AcceptLoop();
            Logger.Log("Tcp Transport Started!");
        }

        public void Stop()
        {
            _isRunning = false;
            _listener.Stop();
        }

        private async void AcceptLoop()
        {
            while (_isRunning)
            {
                try
                {
                    // 비동기적으로 서버 접속을 받는 중
                    var tcpClient = await _listener.AcceptTcpClientAsync();
                    var connection = new TcpClientConnection(tcpClient);

                    // 연결되면 이벤트 Invoke 
                    ClientConnected?.Invoke(connection);

                    // 메시지 받기, 접속 종료 이벤트
                    connection.StartReceiving(MessageReceived, () =>
                    {
                        ClientDisconnected?.Invoke(connection);
                    });
                }
                catch (ObjectDisposedException)
                {
                    // Stop() 호출로 listener가 닫혔을 때 예외 무시
                }
            }
        }
    }
}