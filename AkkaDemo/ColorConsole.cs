using System;
//using System.Console;

namespace AkkaDemo
{
    public static class ColorConsole
    {
        private static readonly object LockObject = new object();

        private static void WriteLine(ConsoleColor color, string message)
        {
            lock (LockObject)
            {
                Console.ForegroundColor = color;
                Console.WriteLine(message);
            }
        }

        private static void Write(ConsoleColor color, string message)
        {
            lock (LockObject)
            {
                Console.ForegroundColor = color;
                Console.Write(message);
            }
        }

        public static void WriteLineGreen(string message, params object[] args)
        {
            WriteLine(ConsoleColor.Green, string.Format(message, args));            
        }

        public static void WriteLineYellow(string message, params object[] args)
        {
            WriteLine(ConsoleColor.Yellow, string.Format(message, args));
        }

        public static void WriteLineRed(string message, params object[] args)
        {
            WriteLine(ConsoleColor.Red, string.Format(message, args));
        }

        public static void WriteLineCyan(string message, params object[] args)
        {
            WriteLine(ConsoleColor.Cyan, string.Format(message, args));
        }

        public static void WriteLineGray(string message, params object[] args)
        {
            WriteLine(ConsoleColor.Gray, string.Format(message, args));
        }

        public static void WriteGray(string message, params object[] args)
        {
            Write(ConsoleColor.Gray, string.Format(message, args));
        }

        public static void WriteLineMagenta(string message, params object[] args)
        {
            WriteLine(ConsoleColor.Magenta, string.Format(message, args));
        }

        public static void WriteLineWhite(string message, params object[] args)
        {
            WriteLine(ConsoleColor.White, string.Format(message, args));
        }
    }
}
