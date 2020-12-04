using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace img_tool.src
{
    
    public class Command
    {
        public readonly string Name;
        public readonly int Priority;
        public readonly TaskTypes TaskType;


        public Command( string name, int priority, TaskTypes taskType)
        {
            Name = name;
            Priority = priority;
            TaskType = taskType;
        }
    }
    
    public class Commands
    {
        static List<Command> Current = new List<Command> 
        { 
            new Command( "cd", 1, TaskTypes.SetFileDate ), 
            new Command( "rd", 2, TaskTypes.RenameByDate ), 
            new Command( "ds", 3, TaskTypes.DeleteBySize ), 
            new Command( "da", 4, TaskTypes.DeleteByAttribute ), 
        };
        
        public static Command TryGetCommand( string arg )
        {
            return Current.SingleOrDefault( x => x.Name == arg );
        }

        public static int GetPriority( TaskTypes taskType )
        {
            return Current.Single( x => x.TaskType == taskType ).Priority;
        }

        public static Command GetByPriority( int priority )
        {
            return Current.Single( x => x.Priority == priority );
        }

        public static TaskTypes GetTaskType( string arg )
        {
            var cmd = Current.SingleOrDefault( x => x.Name == arg );
            if (cmd is null)
            {
                throw new ArgumentException("Wrong command type: {arg}");
            }
            return cmd.TaskType;            
        }

    }
}
