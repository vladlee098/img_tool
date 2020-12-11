using System;
using System.Collections.Generic;
using img_tool.src.Interfaces;

namespace img_tool.src.Core
{
    public class TaskFactory
    {

        public static SortedList<int, ITask> CreateTasks( List<TaskTypes> taskTypes, List<IOption> options )
        {
            var tasks = new SortedList<int, ITask>();
            foreach( var taskType in taskTypes)
            {
                tasks.Add( Commands.GetPriority(taskType), CreateTask(taskType, options) );
            }
            return tasks;
        }        

        public static ITask CreateTask( TaskTypes taskType, List<IOption> options )
        {
            if ( taskType == TaskTypes.DeleteByAttribute)
            {
                return new DeleteByAttribute(options);
            }
            else if ( taskType == TaskTypes.DeleteBySize)
            {
                return new DeleteBySize(options);
            }
            else if ( taskType == TaskTypes.RenameFile)
            {
                return new RenameFile(options);
            }
            else if ( taskType == TaskTypes.SetFileDate)
            {
                return new SetCreateDate(options);
            }
            throw new NotImplementedException();
        }        
    }

}
