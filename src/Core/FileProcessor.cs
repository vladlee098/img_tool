using System;
using System.Collections.Generic;
using System.IO;
using src.Shared;
using src.Interfaces;

namespace src.Core
{
    public class FileProcessor : IProcessor
    {
        readonly SortedList<int, ITask> _tasks;
        readonly string _file;
        readonly int _index;
        bool _verbose;
        bool _force;
        bool _test;
        
        public FileProcessor(SortedList<int, ITask> tasks, string file, int index, bool verbose, bool force, bool test)
        {
            _tasks = tasks;
            _file = file;
            _index = index;
            _verbose = verbose;
            _force = force;
            _test = test;
        }
        
        public virtual void ApplyTasks()
        {
            try
            {
                
                if (_verbose)
                {
                    //ConsoleLog.WriteInfo($"Task count: {_tasks.Count}");
                }
                var fileInfo = new FileInfo(_file);

                //ConsoleLog.WriteInfo($"Thread: {Thread.CurrentThread.GetHashCode()}");
                foreach( int priority in _tasks.Keys)
                {
                    //ConsoleLog.WriteInfo($"Thread: {Thread.CurrentThread.GetHashCode()}, priority: {priority} - STARTING...");
                    
                    if ( _tasks.ContainsKey(priority) )
                    {
                        _tasks[priority].Apply(fileInfo, _index);
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
