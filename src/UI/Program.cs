﻿using System;
using src.Core;
using src.Shared;

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
            Console.WriteLine("  rd: rename file");
            Console.WriteLine("  cd: set file create date");
            Console.WriteLine("  ds: delete files smaller than specified size (KB)");
            Console.WriteLine("  da: delete files by attribute (H|R|S)");
            Console.WriteLine("Options:");
            Console.WriteLine(" -i: source directory");
            Console.WriteLine(" -f: file search mask");
            Console.WriteLine(" -d: file name prefix applied to file names plus index");
            Console.WriteLine(" -c: create file date, must be a valid date, format: 'YYYY-MM-DD'");
            Console.WriteLine(" -z: max file size in KB (1KB = 1024 bytes)");
            Console.WriteLine(" -r: include sub directories");
            Console.WriteLine(" -a: file attribute (H|R|S) only");
            Console.WriteLine(" -force: forced action, no confirmation");
            Console.WriteLine(" -test: will not apply action, just output affected files");
            Console.WriteLine(" -verbose: forced action, no confirmation");
            Console.WriteLine("------------------------------------------------------------------------");
        }

        static void Main(string[] args)
        {
            Console.WriteLine("------------------------------------------------------------------------");
            Console.WriteLine("Utility to rename, set creation date or delete files");
            Console.WriteLine("------------------------------------------------------------------------");

            try
            {
                var (tasks, options) = ArgParser.Parse(args, new TaskValidator() );
                if (tasks is null || options is null)
                {
                    PrintUsage();
                    return; 
                }

                var processor = new TaskProcessor(options, new FileProcessorFactory() );
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
