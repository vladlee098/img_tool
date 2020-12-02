using System;
using System.Linq;

namespace img_tool.src
{

    class Program
    {

        static void PrintUsage()
        {
            Console.WriteLine("------------------------------------------------------------------------");
            Console.WriteLine("Utility to rename, set creation date or delete files");
            Console.WriteLine("Usage:");
            Console.WriteLine("img_tool <CMD> <OPTIONS>");
            Console.WriteLine("Commands:");
            Console.WriteLine("  rd: rename file by date mask");
            Console.WriteLine("  sd: set file create date");
            Console.WriteLine("  ds: delete files smaller then size");
            Console.WriteLine("  da: delete files by attribute (H|R|S)");
            Console.WriteLine("Options:");
            Console.WriteLine(" -i: source directory");
            Console.WriteLine(" -t: destination directory");
            Console.WriteLine(" -fm: file search mask");
            Console.WriteLine(" -dm: date mask applied to file names");
            Console.WriteLine(" -s: include sub directories");
            Console.WriteLine(" -b: max file size in KB (1KB = 1024 bytes)");
            Console.WriteLine(" -c: set create file date");
            Console.WriteLine(" -w: set last write file date");
            Console.WriteLine("------------------------------------------------------------------------");
        }

        static void Main(string[] args)
        {
            Console.WriteLine("------------------------------------------------------------------------");
            Console.WriteLine("Utility to rename, set creation date or delete files");
            Console.WriteLine("------------------------------------------------------------------------");
                     
            //string[] testArgs = new string[] { "rd", "-d" , "-i"};
            string[] testArgs = new string[] { "da", @"-t:e:\@@@test\Liza's birthday" , "-i"};

            try
            {
                var task = ArgParser.ParseCommandLine(testArgs);
                if (task is null)
                {
                    PrintUsage();
                    return;
                }
                task.Run();
            }
            catch( Exception ex)
            {
                ConsoleLog.WriteError( ex.Message);
            }

            Console.WriteLine("------------------------------------------------------------------------");
            Console.WriteLine(">> Press ENTER to quit. ");
            Console.ReadLine();
        }
    }
}
