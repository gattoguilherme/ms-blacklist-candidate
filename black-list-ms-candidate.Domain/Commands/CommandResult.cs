using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using black_list_ms_candidate.Domain.Interfaces;

namespace black_list_ms_candidate.Domain.Commands
{
    public class CommandResult : ICommandResult
    {
        public CommandResult(bool success, object message)
        {
            Success = success;
            Message = message;
        }

        public bool Success { get; set; }
        public object Message { get; set; }
    }
}
