using black_list_ms_candidate.Domain;
using black_list_ms_candidate.Infrastructure.DbConn;
using black_list_ms_candidate.Infrastructure.Interfaces;
using Dapper;

namespace black_list_ms_candidate.Infrastructure.Repositories
{
    public class CandidateRepository : IRepository<Candidate>// : GenericRepository<Candidate>
    {
        private DbSession _session;

        public CandidateRepository(DbSession session)
        {
            _session = session;
        }

        public void Add(Candidate entity)
        {
            var queryCandidate = $"INSERT INTO CANDIDATES VALUES (\"{entity.Id}\", \"{entity.Name}\", \"{entity.IdMentor}\", \"{entity.Email}\"); SELECT * FROM CANDIDATES WHERE ID=\"{entity.Id}\"";
            
            try
            {
                var res = _session.Connection.QuerySingle<Candidate>(queryCandidate, null, _session.Transaction);
                // checar se skills existem no db, se nao da insert e ja cria relação
                entity.Skills.ToList().ForEach(s =>
                {
                    var queryCheck = $"SELECT ID_SKILL FROM SKILLS WHERE ID_SKILL = \"{s.Id_Skill}\";";
                    var res = _session.Connection.ExecuteScalar(queryCheck);

                    var queryInsertSkill = $"INSERT INTO SKILLS VALUES (\"{s.Id_Skill}\", \"{s.Name}\");";
                    var queryInsertRelationship = $"INSERT INTO CANDIDATES_SKILLS VALUES (\"{Guid.NewGuid()}\", \"{entity.Id}\", \"{s.Id_Skill}\");";
                    var fullQuery = string.Concat(queryInsertSkill, queryInsertRelationship);
                    if (res == null)
                        _session.Connection.Execute(fullQuery);
                    else
                        _session.Connection.Execute(queryInsertRelationship);
                });
                //_session.Connection.Execute(query, null, _session.Transaction);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void Update(Candidate entity)
        {
            var queryRemoveRelationship = $"DELETE FROM CANDIDATES_SKILLS WHERE ID_CANDIDATE = \"{entity.Id}\";";
            var queryRemoveCustomer = $"DELETE FROM CANDIDATES WHERE ID = \"{entity.Id}\";";
            var queryFull = string.Concat(queryRemoveRelationship, queryRemoveCustomer);

            try
            {
                // remove all
                _session.Connection.Execute(queryFull, null, _session.Transaction);

                // add as new customer but with the same guid
                this.Add(entity);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public Candidate Get(Guid id)
        {
            var query = $"SELECT C.ID, C.NAME, C.IDMENTOR, C.EMAIL, S.ID_SKILL, S.NAME FROM CANDIDATES C LEFT JOIN CANDIDATES_SKILLS CS ON C.ID = CS.ID_CANDIDATE LEFT JOIN SKILLS S ON S.ID_SKILL = CS.ID_SKILL WHERE C.ID = \"{id}\"";
            try
            {
                var candidates = _session.Connection.Query<Candidate, Skill, Candidate>(query, (candidate, skill) =>
                {
                    candidate.AddSkill(skill);
                    return candidate;
                }, splitOn: "ID_SKILL");

                List<Skill> skillsList = new List<Skill>();
                candidates.ToList().ForEach(x =>
                {
                    if (x.Skills.First() != null)
                        skillsList.Add(x.Skills.First());
                });

                Candidate consolidatedCandidate = candidates
                    .GroupBy(c => c.Id)
                    .Select(g =>
                    {
                        var groupedCandidate = g.First();
                        groupedCandidate.ClearSkills();
                        skillsList.ForEach(skill => groupedCandidate.AddSkill(skill));
                        return groupedCandidate;
                    })
                    .First();

                return consolidatedCandidate;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return new Candidate();
            }

        }

        public IList<Candidate> GetAll()
        {
            var query = $"SELECT C.ID, C.NAME, C.IDMENTOR, C.EMAIL, S.ID_SKILL, S.NAME FROM CANDIDATES C LEFT JOIN CANDIDATES_SKILLS CS ON C.ID = CS.ID_CANDIDATE LEFT JOIN SKILLS S ON S.ID_SKILL = CS.ID_SKILL";
            try
            {
                var candidates = _session.Connection.Query<Candidate, Skill, Candidate>(query, (candidate, skill) =>
                {
                    candidate.AddSkill(skill);
                    return candidate;
                }, splitOn: "ID_SKILL");

                //List<Skill> skillsList = new List<Skill>();
                //candidates.ToList().ForEach(x =>
                //{
                //    if (x.Skills.First() != null)
                //        skillsList.Add(x.Skills.First());
                //});

                var dictSkills = new Dictionary<Guid, List<Skill>>();
                candidates.ToList().ForEach(c =>
                {
                    if (!dictSkills.ContainsKey(c.Id))
                    {
                        var listSkillsToBeAdded =
                            (c.Skills.First() != null) ? 
                                new List<Skill>() { c.Skills.First() } : new List<Skill>();

                        dictSkills.Add(c.Id, listSkillsToBeAdded);
                    }
                    else
                    {
                        var currentSkills = dictSkills[c.Id];
                        currentSkills.Add(c.Skills.First());
                        dictSkills[c.Id] = currentSkills;
                    }
                });

                List<Candidate> consolidatedCandidate = candidates
                    .GroupBy(c => c.Id)
                    .Select(g =>
                    {
                        var groupedCandidate = g.First();
                        groupedCandidate.ClearSkills();
                        //skillsList.ForEach(skill => groupedCandidate.AddSkill(skill));
                        dictSkills[groupedCandidate.Id].ForEach(skill =>
                        {
                            groupedCandidate.AddSkill(skill);
                        });

                        return groupedCandidate;
                    })
                    .ToList();

                return consolidatedCandidate;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return new List<Candidate>();
            }
        }

        public Guid Delete(Guid id)
        {
            var queryDeleteRelationship = $"DELETE FROM CANDIDATES_SKILLS WHERE ID_CANDIDATE = \"{id}\";";
            var queryCandidate = $"DELETE FROM CANDIDATES WHERE ID = \"{id}\";";
            var queryConsolidated = string.Concat(queryDeleteRelationship, queryCandidate);
            try
            {
                _session.Connection.Execute(queryConsolidated);
                return id;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return Guid.Empty;
        }
    }
}