namespace black_list_ms_candidate.Domain
{
    public class Mentor
    {
        public Guid Id { get; set; }
        public DateOnly BornDate { get; set; }
        public DateTime CreationDate { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }        
    }
}
