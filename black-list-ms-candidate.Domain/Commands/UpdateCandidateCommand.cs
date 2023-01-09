using black_list_ms_candidate.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace black_list_ms_candidate.Domain.Commands
{
    public class UpdateCandidateCommand : ICommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime BornDate { get; set; }
        public Guid IdMentor { get; set; }
        public string Email { get; set; }
        public IList<Skill> Skills { get; set; }
    }
}
