using black_list_ms_candidate.Domain;
using black_list_ms_candidate.Domain.Commands;
using black_list_ms_candidate.Domain.Handlers;
using black_list_ms_candidate.Domain.Interfaces;
using black_list_ms_candidate.Infrastructure.Interfaces;
using black_list_ms_candidate.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace black_list_ms_candidate.Controllers
{
    [Route("api/v1/[controller]")]
    public class CandidateController : ControllerBase
    {
        private readonly IRepository<Candidate> _candidateRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CandidateController(IRepository<Candidate> candidateRepository, IUnitOfWork unitOfWork)
        {
            _candidateRepository = candidateRepository;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCandidateAsync([FromBody] Candidate candidate)
        {
            // validate infos
            if (candidate == null) 
                return NotFound(new CommandResult(false, "candidate cannot be null"));
            // create command
            var command = new CreateCandidateCommand();
            command.Name = candidate.Name;
            command.IdMentor = candidate.IdMentor;
            command.BornDate = candidate.BornDate;
            command.Email = candidate.Email;
            command.Skills = candidate.Skills;

            //handle
            //var mockRepo = new MockRepository();
            var handler = new CandidateHandler(_candidateRepository);
            ICommandResult result = handler.Handle(command);
            // _unitOfWork.Commit();

            if (result != null && !((CommandResult)result).Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult> GetAllCandidateAsync()
        {

            // create command
            var command = new GetCandidatesCommand();

            //handle
            //var mockRepo = new MockRepository();
            var handler = new CandidateHandler(_candidateRepository);
            ICommandResult result = handler.Handle(command);

            if (result != null && !((CommandResult)result).Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("id")]
        public async Task<ActionResult> GetCandidateAsync([FromHeader] Guid id)
        {
            // create command
            var command = new GetCandidateByIdCommand();
            command.Id = id;

            //handle
            var handler = new CandidateHandler(_candidateRepository);
            ICommandResult result = handler.Handle(command);

            if (result != null && !((CommandResult)result).Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpDelete("id")]
        public async Task<ActionResult> DeleteCandidateAsync([FromHeader] Guid id)
        {
            // create command
            var command = new DeleteCandidateCommand();
            command.Id = id;

            //handle
            //var mockRepo = new MockRepository();
            var handler = new CandidateHandler(_candidateRepository);
            ICommandResult result = handler.Handle(command);

            if (result != null && !((CommandResult)result).Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateCandidateAsync([FromBody] Candidate candidate)
        {
            // validate infos
            if (candidate == null)
                return NotFound(new CommandResult(false, "candidate cannot be null"));
            
            var command = new UpdateCandidateCommand();
            command.Id = candidate.Id;
            command.Name = candidate.Name;
            command.IdMentor = candidate.IdMentor;
            command.Email = candidate.Email;
            command.Skills = candidate.Skills;

            var handler = new CandidateHandler(_candidateRepository);
            ICommandResult result = handler.Handle(command);

            if (result != null && !((CommandResult)result).Success)
                return BadRequest(result);

            return Ok(result);

        }
    }
}
