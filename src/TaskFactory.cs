using System;
using System.Collections.Generic;

namespace img_tool.src
{
    public class TaskFactory
    {
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
            else if ( taskType == TaskTypes.SetFileDate)
            {
                throw new NotImplementedException();
            }
            throw new NotImplementedException();
        }        
    }
}
