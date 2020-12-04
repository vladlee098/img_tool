using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace img_tool.src
{
    public class FileProcessor
    {
        readonly SortedList<int, ITask> _tasks;
        readonly string _file;
        
        public FileProcessor(SortedList<int, ITask> tasks, string file)
        {
            _tasks = tasks;
            _file = file;
        }

        public void ApplyTasks()
        {
            try
            {
                //ConsoleLog.WriteInfo($"Task count: {_tasks.Count}");
                var fileInfo = new FileInfo(_file);

                //ConsoleLog.WriteInfo($"Thread: {Thread.CurrentThread.GetHashCode()}");
                for( int priority = 1; priority <= _tasks.Count; priority++ ) 
                {
                    //ConsoleLog.WriteInfo($"Thread: {Thread.CurrentThread.GetHashCode()}, priority: {priority} - STARTING...");
                    
                    if ( _tasks.ContainsKey(priority) )
                    {
                        _tasks[priority].Apply(fileInfo);
                    }
                    else
                    {
                        //ConsoleLog.WriteInfo($"Thread: {Thread.CurrentThread.GetHashCode()}, priority: {priority} - KEY NOT FOUND...");
                        //ConsoleLog.WriteInfo($"Thread: {Thread.CurrentThread.GetHashCode()}, first task: {_tasks.Keys[0]}");
                    }
                    //ConsoleLog.WriteInfo($"Thread: {Thread.CurrentThread.GetHashCode()}, priority: {priority} - DONE");
                }            
            }
            catch (AggregateException aex)
            {
                ConsoleLog.WriteError($"Error while deleting '{_file}': {aex.Message}");
            }
            catch (IOException ex)
            {
                ConsoleLog.WriteError($"Error while deleting '{_file}': {ex.Message}");
            }                
        }        
    }
}
