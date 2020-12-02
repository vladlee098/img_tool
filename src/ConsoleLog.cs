using System;

namespace img_tool.src
{
    internal class ConsoleLog
    {
        internal static void WriteError( string message )
        {
            var saved = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ForegroundColor = saved;
        }

        internal static void WriteTask( string message )
        {
            var saved = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(message);
            Console.ForegroundColor = saved;
        }

        internal static bool AskToApprove( string message )
        {
            //Console.Clear();
            var saved = Console.ForegroundColor;
            try
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(message);

                Console.ForegroundColor = ConsoleColor.Blue;
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.Escape || key.KeyChar == 'N' || key.KeyChar == 'n')
                {
                    return false;
                }
                else if (key.KeyChar == 'Y')
                {
                    return true;
                }
                return false;
            }
            finally
            {
                Console.ForegroundColor = saved;
            }
        }
    }
}
