using LeoServer.Runtime;
using LeoServer.Transport;

namespace LeoServer.Main
{
    public class Program
    {
        static void Main(string[] args)
        {
            var server = new MainServer();
            server.Initialize(new TcpTransport());
            server.Start();

            Console.ReadLine();
            server.Stop();
        }
    }
}
