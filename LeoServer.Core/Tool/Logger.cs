using System;

namespace LeoServer.Tool
{
    public static class Logger
    {
        private const string Format = "[LeoServer][{0}][{1}] {2}";

        public static void Log(string message, ELogType logType = ELogType.LOG)
        {
            var prevColor = Console.ForegroundColor;

            switch (logType)
            {
                case ELogType.ERROR:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case ELogType.WARNING:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case ELogType.LOG:
                default:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
            }

            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            Console.WriteLine(Format, logType, timestamp, message);

            Console.ForegroundColor = prevColor;
        }

        public static void LogWarning(string message)
        {
            Log(message, ELogType.WARNING);
        }

        public static void LogError(string message)
        {
            Log(message, ELogType.ERROR);
        }
    }

    public enum ELogType
    {
        LOG,
        ERROR,
        WARNING
    }
}
