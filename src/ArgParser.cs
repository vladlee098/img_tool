using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace img_tool.src
{
    public class ArgParser
    {

        public static (SortedList<int, ITask>, List<IOption>) Parse( string[] args )
        {
            // parse commands
            if (args.Length == 0)
            {
                ConsoleLog.WriteError($">> No arguments.");
                return (null, null);
            }
                        
            var taskTypes = new List<TaskTypes>();
            var options = new List<IOption>();

            foreach( var arg in args)
            {
                var command = Commands.TryGetCommand( arg );
                if ( command is not null )
                {
                    taskTypes.Add( command.TaskType );
                }
            }

            if (taskTypes.Count == 0)
            {
                ConsoleLog.WriteError($">> No commands found in arguments.");
                return (null, null);
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

            return ( TaskFactory.CreateTasks(taskTypes, options), options );
        }
    }
}
