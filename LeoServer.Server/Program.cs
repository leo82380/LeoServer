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

            Console.WriteLine("Server Running. Type 'CheckClient' to see connected clients.");

            while (true)
            {
                string input = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(input))
                    continue;

                if (input.Equals("exit", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }
                else if (input.Equals("CheckClient", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine(server.GetJoinedClients());
                }
                else
                {
                    Console.WriteLine("Unknown command");
                }
            }

            server.Stop();
        }
    }
}
