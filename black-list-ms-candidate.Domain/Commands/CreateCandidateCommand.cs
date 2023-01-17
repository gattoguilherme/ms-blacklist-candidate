using black_list_ms_candidate.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace black_list_ms_candidate.Domain.Commands
{
    public class CreateCandidateCommand : ICommand
    {
        public Guid Id { get; set; }
        public Guid IdMentor { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string Uf { get; set; }
        public string Condition { get; set; }
        public bool Status { get; set; }
        public int Score { get; set; }
        public string PhoneNumber { get; set; }
        public IList<Skill> Skills { get; set; }
    }
}
