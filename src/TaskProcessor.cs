using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace img_tool.src
{
    public class TaskProcessor
    {
        string _sourceDir;
        string _fileMask;
        bool _recursive;

        readonly List<IOption> _globalOptions;

        public TaskProcessor( List<IOption> options)
        {
            if ( !Validate(options))
            {
                throw new ArgumentException( $"Provided options are invalid, check command line");
            }
            
            _globalOptions = options;
        }

        private string TasksToString( SortedList<int, ITask> tasks )
        {
            var sb = new StringBuilder();

            for( int priority = 1; priority <= tasks.Count(); priority++ ) 
            {
                sb.Append( Commands.GetByPriority( priority ).TaskType.ToString() + ", " );
            }

            sb.Length --;
            sb.Length --;

            return sb.ToString();
        }

        public string ToString( SortedList<int, ITask> tasks )
        {
            var sb = new StringBuilder();
            sb.AppendLine(">> Processing tasks:");
            sb.AppendLine($"   - {TasksToString(tasks)}");
            sb.AppendLine(">> Paramerters:");
            sb.AppendLine($"   - target directory: {this._sourceDir}");
            sb.AppendLine($"   - file mask: {this._fileMask}");
            sb.AppendLine($"   - include sub directories: {this._recursive}");
            return sb.ToString();
        }
        
        public bool Validate(List<IOption> options)
        {
            var sourceDir = options.SingleOrDefault( x => x.OptionType == OptionTypes.SourceDirectory);
            if (sourceDir is null)
            {
                ConsoleLog.WriteError($">> Invalid SourceDirectory option.");
                return false;
            }
            _sourceDir = (sourceDir as PathOption).Data;

            var mask = options.SingleOrDefault( x => x.OptionType == OptionTypes.FileMask);
            _fileMask = mask is null ? "*.*" : (mask as TextOption).Data;

            var recursive = options.SingleOrDefault( x => x.OptionType == OptionTypes.IncludeSubDirectories);
            _recursive = recursive is null ? false : true;

            return true;        
        }

        public void Execute( SortedList<int, ITask> tasks)
        {
            ConsoleLog.WriteTask( this.ToString(tasks));

            var goAhead = ConsoleLog.AskToApprove($">> Confirm delete by attribute task (Y/N): ");
            if (!goAhead)
            {
                return;
            }
                        
            var files = (from file in Directory.EnumerateFiles(
                            _sourceDir,
                            _fileMask,
                            _recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly)
                        select file).ToList();

            if (files == null || files.Count() == 0)
            {
                ConsoleLog.WriteInfo($"No files found in '{_sourceDir}' with mask: '{_fileMask}'");
                return;
            }
            ConsoleLog.WriteInfo($">>Found {files.Count()} files inside {_sourceDir}, with mask: '{_fileMask}'");

            Parallel.For(0, files.Count, index => 
            { 
                var processor = new FileProcessor( tasks, files[index], index);
                processor.ApplyTasks();
            } );           
        }        
    }
}
