using black_list_ms_candidate.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace black_list_ms_candidate.Domain.Commands
{
    public class GetCandidateByIdCommand : ICommand
    {
        public Guid Id { get; set; }
    }
}
