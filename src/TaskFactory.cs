using System;
using System.Collections.Generic;

namespace img_tool.src
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
            // else if ( taskType == TaskTypes.RenameByDateMask)
            // {
            //     return new RenameByMask(_parser);
            // }
            // else if ( taskType == TaskTypes.SetFileDate)
            // {
            //     throw new NotImplementedException();
            // }
            throw new NotImplementedException();
        }        
    }

}
