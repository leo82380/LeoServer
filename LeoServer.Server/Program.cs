using LeoServer.Runtime;
using LeoServer.Tool;
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

            var commandHandler = new CommandHandler();
            commandHandler.AddCommand("exit", server.Stop);
            commandHandler.AddCommand("checkClient", () => Logger.Log(server.GetJoinedClients()));
            commandHandler.AddCommand("broadcast", () => server.Broadcast("Hello Clients"));

            Console.WriteLine("Server Running. Type 'CheckClient' to see connected clients.");

            while (true)
            {
                string input = Console.ReadLine()?.Trim().ToLower();

                commandHandler.InvokeCommand(input);
            }
        }
    }
}
