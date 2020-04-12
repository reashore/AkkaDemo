using System;

namespace AkkaDemo
{
    public static class ColorConsole
    {
        private static readonly object LockObject = new object();

        private static void Write(ConsoleColor color, string message)
        {
            lock (LockObject)
            {
                Console.ForegroundColor = color;
                Console.WriteLine(message);
            }
        }

        public static void WriteGreen(string message, params object[] args)
        {
            Write(ConsoleColor.Green, string.Format(message, args));            
        }

        public static void WriteYellow(string message, params object[] args)
        {
            Write(ConsoleColor.Yellow, string.Format(message, args));
        }

        public static void WriteRed(string message, params object[] args)
        {
            Write(ConsoleColor.Red, string.Format(message, args));
        }

        public static void WriteCyan(string message, params object[] args)
        {
            Write(ConsoleColor.Cyan, string.Format(message, args));
        }

        public static void WriteGray(string message, params object[] args)
        {
            Write(ConsoleColor.Gray, string.Format(message, args));
        }

        //public static void WriteMagenta(string message, params object[] args)
        //{
        //    Write(ConsoleColor.Magenta, string.Format(message, args));
        //}

        public static void WriteWhite(string message, params object[] args)
        {
            Write(ConsoleColor.White, string.Format(message, args));
        }
    }
}
