using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using src.Interfaces;
using src.Shared;

namespace img_tool.test
{
    
    public class MockFileProcessorFactory : IFileProcessorFactory
    {
        public virtual IProcessor Create(SortedList<int, ITask> tasks, string file, int index, bool verbose, bool force, bool test)
        {
            return new MockFileProcessor( tasks, file, index, verbose, force, test);
        }
    }


    public class MockFileProcessor : IProcessor
    {
        readonly SortedList<int, ITask> _tasks;
        readonly string _file;
        readonly int _index;
        bool _verbose;
        bool _force;
        bool _test;
        
        public MockFileProcessor(SortedList<int, ITask> tasks, string file, int index, bool verbose, bool force, bool test)
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
                foreach( int priority in _tasks.Keys)
                {
                    ConsoleLog.WriteInfo($"Thread: {Thread.CurrentThread.GetHashCode()}, priority: {priority} - WORKING...");
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
