using black_list_ms_candidate.Domain;

namespace black_list_ms_candidate.Infrastructure.DTO
{
    internal class CandidateDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime BornDate { get; set; }
        public DateTime CreationDate { get; set; }
        public Guid IdMentor { get; set; }
        public string Email { get; set; }
        public IList<Skill> Skills { get; set; }
    }
}
