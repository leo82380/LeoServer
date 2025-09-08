using LeoServer.Tool;

namespace LeoServer.Runtime
{
    public class CommandHandler : IDisposable
    {
        private Dictionary<string, Action> _commands = new(StringComparer.OrdinalIgnoreCase);

        public void AddCommand(string command, Action action)
        {
            if (_commands.ContainsKey(command))
            {
                Logger.LogWarning($"{command} is already added");
                return;
            }

            _commands.Add(command, action);
        }

        public void InvokeCommand(string command)
        {
            if (string.IsNullOrWhiteSpace(command)) return;

            if (_commands.TryGetValue(command, out var action))
            {
                action?.Invoke();
            }
            else
            {
                Logger.LogWarning("Unknown Commad!");
            }
        }


        public void Dispose()
        {
            _commands.Clear();
            _commands = null;
        }
    }
}