using System;
using System.Collections.Generic;
using src.Interfaces;
using src.Shared;

namespace src.Core
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
                if ( command != null )
                {
                    taskTypes.Add( command.TaskType );
                }
                else
                {
                    var option = OptionFactory.Find(arg);
                    if (option != null)
                    {
                        options.Add(option);
                    }
                }
            }

            if ( !taskValidator.ValidateTasks(taskTypes) )
            {
                throw new ArgumentException( $"Provided commands are invalid, check command line");
            }            

            return ( TaskFactory.CreateTasks(taskTypes, options), options );
        }

    }
}
