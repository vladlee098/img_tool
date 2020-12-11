using System;
using System.Collections.Generic;
using img_tool.src.Interfaces;
using img_tool.src.Shared;

namespace img_tool.src.Core
{
    public class ArgParser
    {
        public static (SortedList<int, ITask>, List<IOption>) Parse( string[] args, ITaskValidator taskValidator )
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
                else
                {
                    var option = OptionFactory.Find(arg);
                    if (option is not null)
                    {
                        options.Add(option);
                    }
                }
            }

            if ( !taskValidator.ValidateTasks(taskTypes) )
            {
                return (null, null);
            }            

            return ( TaskFactory.CreateTasks(taskTypes, options), options );
        }

    }
}
