using System;

namespace src.Shared
{
    public class ConsoleLog
    {
        public static void WriteError( string message )
        {
            var saved = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ForegroundColor = saved;
        }
        public static void WriteWarning( string message )
        {
            var saved = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(message);
            Console.ForegroundColor = saved;
        }

        public static void WriteInfo( string message )
        {
            Console.WriteLine(message);
        }

        public static void WriteTask( string message )
        {
            var saved = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(message);
            Console.ForegroundColor = saved;
        }

        public static bool AskToApprove( string message )
        {
            //Console.Clear();
            var saved = Console.ForegroundColor;
            try
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(message);

                Console.ForegroundColor = ConsoleColor.Blue;
                while(true)
                {
                    var key = Console.ReadKey();
                    if (key.Key == ConsoleKey.Escape || key.KeyChar == 'N' || key.KeyChar == 'n')
                    {
                        return false;
                    }
                    else if (key.KeyChar == 'Y' || key.KeyChar == 'y')
                    {
                        return true;
                    }
                }
            }
            finally
            {
                Console.WriteLine();
                Console.ForegroundColor = saved;
            }
        }
    }
}
