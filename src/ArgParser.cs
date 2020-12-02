using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace img_tool.src
{
    public class ArgParser
    {

        static List<String> Commands = new List<String> { "rd", "ds", "da"};

        static TaskTypes GetTaskType( string arg )
        {
            if ( arg == "rd")
            {
                return TaskTypes.RenameByDate;
            }
            else if ( arg == "ds")
            {
                return TaskTypes.DeleteBySize;
            }
            else if ( arg == "da")
            {
                return TaskTypes.DeleteByAttribute;
            }
            throw new Exception("Wrong command type");
        }

        public static ITask ParseCommandLine( string[] args )
        {
            // parse commands
            if (args.Length == 0)
            {
                ConsoleLog.WriteError($">> No arguments.");
                return null;
            }
                        
            //var arguments = args.Split(' ');
            var tasks = new List<TaskTypes>();
            var options = new List<IOption>();

            foreach( var arg in args)
            {
                if ( Commands.Contains(arg) )
                {
                    tasks.Add( GetTaskType( arg ));
                }
            }

            if (tasks.Count == 0)
            {
                ConsoleLog.WriteError($">> No commands found in arguments.");
                return null;
            }            

            if (tasks.Count > 1)
            {
                ConsoleLog.WriteError($">> This version can onply process one task at a time.");
                return null;
            }            

            // parse options
            foreach( var arg in args)
            {
                var option = OptionFactory.Find(arg);
                if (option is null)
                {
                    continue;
                }
                options.Add(option);
            }

            return ( TaskFactory.CreateTask(tasks[0], options) );
        }
    }
}
