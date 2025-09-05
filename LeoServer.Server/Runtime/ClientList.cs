using System.Text;
using LeoServer.Core;
using LeoServer.Tool;

namespace LeoServer.Runtime
{
    public class ClientList : IDisposable
    {
        private List<IClientConnection> _currentClient;

        public ClientList()
        {
            _currentClient = new();
        }

        public void AddClient(IClientConnection client)
        {
            if (_currentClient.Contains(client))
            {
                Logger.LogWarning($"Client {client.Id} is Already Added");
                return;
            }

            _currentClient.Add(client);
        }

        public void RemoveClient(IClientConnection client)
        {
            if (!_currentClient.Contains(client))
            {
                Logger.LogWarning($"Client {client.Id} Not Found");
                return;
            }

            _currentClient.Remove(client);
        }

        public void Dispose()
        {
            _currentClient.Clear();
        }

        public string GetClients()
        {
            StringBuilder sb = new();
            for (int i = 0; i < _currentClient.Count; i++)
            {
                var curClient = _currentClient[i];
                sb.AppendLine($"[{i}] Client {curClient.Id}");
            }

            return sb.ToString();
        }
    }
}