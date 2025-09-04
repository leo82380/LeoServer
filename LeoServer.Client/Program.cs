using System.Net.Sockets;
using System.Text;
using LeoServer.Tool;

namespace LeoServer.Client
{

    class Program
    {
        static void Main()
        {
            try
            {
                using var tcpClient = new TcpClient("127.0.0.1", 7777);
                using var stream = tcpClient.GetStream();

                Logger.Log("Connected to server!");

                // 서버에서 보낸 메시지 받기
                Task.Run(async () =>
                {
                    var buffer = new byte[1024];
                    while (true)
                    {
                        int length = await stream.ReadAsync(buffer, 0, buffer.Length);
                        if (length == 0) break;
                        string msg = Encoding.UTF8.GetString(buffer, 0, length);
                        Logger.Log($"Server → {msg}");
                    }
                });

                // 서버로 메시지 보내기
                while (true)
                {
                    string line = Console.ReadLine();
                    if (string.IsNullOrEmpty(line)) break;
                    byte[] data = Encoding.UTF8.GetBytes(line);
                    stream.Write(data, 0, data.Length);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Client error: {ex.Message}");
            }

            Logger.Log("Disconnected.");
        }
    }
}