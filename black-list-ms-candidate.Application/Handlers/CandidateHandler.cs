using black_list_ms_candidate.Domain.Commands;
using black_list_ms_candidate.Domain.Interfaces;
using black_list_ms_candidate.Infrastructure.Interfaces;
using black_list_ms_candidate.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace black_list_ms_candidate.Domain.Handlers
{
    public class CandidateHandler : IHandler<CreateCandidateCommand>
    {
        private readonly CandidateRepository _candidateRepository;
        public CandidateHandler(CandidateRepository candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }

        public ICommandResult Handle(CreateCandidateCommand command)
        {
            var name = command.Name;
            var bornDate = command.BornDate;
            var email = command.Email;
            var idMentor = command.IdMentor;

            try
            {
                var candidate = new Candidate(name, bornDate, idMentor, email);

                if (command.Skills != null)
                    command.Skills.ToList().ForEach(skill => candidate.AddSkill(skill));

                candidate.AddSkill(new Skill(Guid.NewGuid(), "Comedor"));
                this._candidateRepository.Add(candidate);

                return new CommandResult(true, "Candidate successfully created.");
            }
            catch (Exception e)
            {
                return new CommandResult(false, $"Error on creating new candidate: {e.ToString()}");
            }
            //var res = this._candidateRepository.Add(candidate);

            //if (res == null)
            //    return new CommandResult(false, "Persistence error on recording candidate.");
            //else
            //    this._candidateRepository.SaveChanges();
        }

        public ICommandResult Handle(GetCandidatesCommand command)
        {
            var res = this._candidateRepository.GetAll();

            if (res == null)
                return new CommandResult(false, "Cannot retrieve all candidates");

            return new CommandResult(true, res);
        }

        public ICommandResult Handle(GetCandidateByIdCommand command)
        {
            var res = this._candidateRepository.Get(command.Id);

            if (res == null)
                return new CommandResult(false, "Cannot retrieve candidate");

            return new CommandResult(true, res);
        }

        public ICommandResult Handle(DeleteCandidateCommand command)
        {
            var res = this._candidateRepository.Delete(command.Id);

            if (res == Guid.Empty)
                return new CommandResult(false, $"Cannot delete candidate {command.Id }");

            return new CommandResult(true, $"Customer {res} succesfully deleted");
        }
    }
}