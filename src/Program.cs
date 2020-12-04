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
            Console.WriteLine("  cd: set file create date");
            Console.WriteLine("  ds: delete files smaller then specified size (KB)");
            Console.WriteLine("  da: delete files by attribute (H|R|S)");
            Console.WriteLine("Options:");
            Console.WriteLine(" -i: source directory");
            Console.WriteLine(" -t: destination directory");
            Console.WriteLine(" -fm: file search mask");
            Console.WriteLine(" -dm: date mask applied to file names");
            Console.WriteLine(" -r: include sub directories");
            Console.WriteLine(" -z: max file size in KB (1KB = 1024 bytes)");
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
            //string[] testArgs = new string[] { "da", @"-i:e:\@@@test\Liza's birthday" , "-w", ""};
            //string[] byAttrArgs = new string[] { "da", @"-i:e:\@@@test\Liza's birthday" , "-w", "-a:H", "-f:*Rot*"};
            //string[] bySizeArgs = new string[] { "ds", @"-i:e:\@@@test\Liza's birthday" , "-z:5", "-r", "-w", "-f:*Rot*"};

            string[] bySizeArgs = new string[] { "ds", @"-i:e:\@@@test\Liza's birthday" , "-z:5", "-r", "-w", "-f:*Rot*"};

            try
            {
                var (tasks, options) = ArgParser.Parse(bySizeArgs);
                if (tasks is null || options is null)
                {
                    PrintUsage();
                    return;
                }

                var processor = new TaskProcessor(options);
                processor.Execute( tasks);
            }
            catch( Exception ex)
            {
                ConsoleLog.WriteError( ex.Message);
            }

            Console.WriteLine();
            Console.WriteLine("------------------------------------------------------------------------");
            Console.WriteLine(">> Done. Press ENTER to quit. ");
            Console.ReadLine();
        }
    }
}
