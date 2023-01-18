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

        private Guid _id_Skill;
        public Guid Id_Skill
        {
            get {
                if (this._id_Skill == new Guid("00000000-0000-0000-0000-000000000000"))
                    this._id_Skill = Guid.NewGuid();
                return this._id_Skill;
            }
            set
            {
                if (value == new Guid("00000000-0000-0000-0000-000000000000"))
                    this._id_Skill = Guid.NewGuid();
                else
                    this._id_Skill = value;
            }
        }
        public string Name { get; set; }
    }
}
