using System.Linq;

namespace black_list_ms_candidate.Domain
{
    public class Candidate
    {
        private string _phoneNumber;
        private Guid _id;
        private Guid _idMentor;

        public Guid Id
        {
            get
            {
                if (this._id == new Guid("00000000-0000-0000-0000-000000000000"))
                    this._id = Guid.NewGuid();
                return this._id;
            }
            set
            {
                if (value == new Guid("00000000-0000-0000-0000-000000000000"))
                    this._id = Guid.NewGuid();
                else
                    this._id = value;
            }
        }
        public Guid IdMentor { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string Uf { get; set; }
        public string Condit { get; set; }
        public bool Status { get; set; }
        public int Score { get; set; }
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { _phoneNumber = new string(value.Where(char.IsDigit).ToArray()); }
        }

        public IList<Skill> Skills
        {
            get { return this._skills?.ToArray(); }
            set { this._skills = value; }
        }

        private IList<Skill> _skills;

        public Candidate()
        {
        }

        public Candidate(string name, Guid idMentor, string email, string city, string uf, string condition, bool status, int score, string phoneNumber)
        {
            Id = Guid.NewGuid();
            Name = name;
            IdMentor = idMentor;
            Email = email;
            City = city;
            Uf = uf;
            Condit = condition;
            Score = score;
            Status = status;
            PhoneNumber = phoneNumber;
            _skills = new List<Skill>();

        }

        public Candidate(Guid guid, string name, Guid idMentor, string email, string city, string uf, string condition, bool status, int score, string phoneNumber)
        {
            Id = guid;
            Name = name;
            IdMentor = idMentor;
            Email = email;
            _skills = new List<Skill>();
            City = city;
            Uf = uf;
            Score = score;
            Condit = condition;
            Status = status;
            PhoneNumber = phoneNumber;
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
