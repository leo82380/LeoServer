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

        private TcpListener _listener;
        private bool _isRunning = false;

        public void Start()
        {
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
                    var tcpClient = await _listener.AcceptTcpClientAsync();
                    var connection = new TcpClientConnection(tcpClient);
                    ClientConnected?.Invoke(connection);
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