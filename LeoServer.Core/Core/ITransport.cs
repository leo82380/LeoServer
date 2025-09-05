using System;

namespace LeoServer.Core
{
    public interface ITransport
    {
        /// <summary>
        /// 클라이언트 Connect 이벤트
        /// </summary>
        event Action<IClientConnection> ClientConnected;

        /// <summary>
        /// 클라이언트 Disconnected 이벤트
        /// </summary>
        event Action<IClientConnection> ClientDisconnected;

        /// <summary>
        /// 클라이언트 메시지 수신 이벤트
        /// </summary>
        event Action<IClientConnection, string> MessageReceived;


        /// <summary>
        /// Transport 시작 함수
        /// </summary>
        void Start();

        /// <summary>
        /// Transport 종료 함수
        /// </summary>
        void Stop();
    }
}