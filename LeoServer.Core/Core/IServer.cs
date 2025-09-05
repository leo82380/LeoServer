namespace LeoServer.Core
{
    public interface IServer
    {
        bool IsRunning { get; }

        /// <summary>
        /// 서버 초기화 함수
        /// </summary>
        /// <param name="transport">서버와 클라이언트 간의 통신수단</param>
        void Initialize(ITransport transport);

        /// <summary>
        /// 서버 시작 함수
        /// </summary>
        void Start();

        /// <summary>
        /// 서버 정지 함수
        /// </summary>
        void Stop();
    }
}