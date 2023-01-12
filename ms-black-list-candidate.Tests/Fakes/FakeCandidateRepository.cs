using black_list_ms_candidate.Domain;
using black_list_ms_candidate.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ms_black_list_candidate.Tests.Fakes
{
    public class FakeCandidateRepository : IRepository<Candidate>
    {
        private readonly List<Candidate> _candidateList;
        public FakeCandidateRepository()
        {
            var c1 = new Candidate("c1", Guid.NewGuid(), "c1@email.com");
            var c2 = new Candidate("c2", Guid.NewGuid(), "c2@email.com");
            var c3 = new Candidate(new Guid("25398c72-e488-4139-89fd-1c1d30b42307"), "c3", new Guid("10364234-c868-4b25-b99c-dba0755ade69"), "c3@email.com");

            var s1 = new Skill("Backend");
            var s2 = new Skill("Frontend");
            var s3 = new Skill("DevOps");

            c2.AddSkill(s2);
            c3.AddSkill(s1);
            c3.AddSkill(s2);
            c3.AddSkill(s3);

            _candidateList = new List<Candidate>() { c1, c2, c3 };
        }

        public void Add(Candidate entity)
        {
            _candidateList.Add(entity);
        }

        public Guid Delete(Guid id)
        {
            var customerToBeRemoved = _candidateList.First(c => c.Id == id);
            _candidateList.Remove(customerToBeRemoved);

            return customerToBeRemoved.Id;
        }

        public Candidate Get(Guid id)
        {
            var customerToBeReturned = _candidateList.FirstOrDefault(c => c.Id == id);
            return customerToBeReturned;
        }

        public IList<Candidate> GetAll()
        {
            return _candidateList;
        }

        public void Update(Candidate entity)
        {
            Delete(entity.Id);
            Add(entity);
        }
    }
}
