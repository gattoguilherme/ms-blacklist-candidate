using System.Linq;

namespace black_list_ms_candidate.Domain
{
    public class Candidate
    {

        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime BornDate { get; set; }
        public DateTime CreationDate { get; set; }
        public Guid IdMentor { get; set; }
        public string Email { get; set; }
        public IList<Skill> Skills 
        { 
            get { return this._skills?.ToArray();}
            set { this._skills = value; }
        }

        private IList<Skill> _skills;

        public Candidate()
        {
        }

        public Candidate(string name, DateTime bornDate, Guid idMentor, string email)
        {
            Id = Guid.NewGuid();
            Name = name;
            //BornDate = bornDate.GetDateTimeFormats()[5];
            IdMentor = idMentor;
            Email = email;
            //CreationDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
            _skills = new List<Skill>();
        }

        public void AddSkill(Skill skill)
        {
            if (this._skills == null)
                _skills = new List<Skill>();
            this._skills.Add(skill);
        }

        public void ClearSkills()
        {
            this._skills.Clear();
        }

    }
}
