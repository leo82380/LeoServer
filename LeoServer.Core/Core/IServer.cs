namespace LeoServer.Core
{
    public interface IServer
    {
        bool IsRunning { get; }
        void Initialize(ITransport transport);
        void Start();
        void Stop();
    }
}