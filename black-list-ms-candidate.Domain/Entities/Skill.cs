namespace black_list_ms_candidate.Domain
{
    public class Skill
    {

        public Skill()
        {

        }

        public Skill(string name)
        {
            Id_Skill = Guid.NewGuid();
            Name = name;
        }

        public Skill(Guid id, string name)
        {
            Id_Skill = id;
            Name = name;
        }

        public Skill(string id, string name)
        {
            Id_Skill = new Guid(id);
            Name = name;
        }

        public Guid Id_Skill { get; set; }
        public string Name { get; set; }
    }
}
