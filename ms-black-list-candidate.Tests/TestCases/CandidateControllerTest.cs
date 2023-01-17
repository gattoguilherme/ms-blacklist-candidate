using black_list_ms_candidate.Controllers;
using black_list_ms_candidate.Domain;
using black_list_ms_candidate.Domain.Commands;
using black_list_ms_candidate.Infrastructure.Interfaces;
using black_list_ms_candidate.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using ms_black_list_candidate.Tests.Fakes;

namespace ms_black_list_candidate.Tests.TestCases
{
    public class CandidateControllerTest
    {
        private readonly CandidateController _controller;
        private readonly IRepository<Candidate> _repository;

        public CandidateControllerTest()
        {
            _repository = new FakeCandidateRepository();
            _controller = new CandidateController(_repository, null);
        }

        [Fact]
        public void GetAllCandidateAsync_Returns_3_Candidates()
        {
            // Arrange


            // Act
            var httpResult = _controller.GetAllCandidateAsync();
            CommandResult value = (httpResult.Result as OkObjectResult).Value as CommandResult;
            var candidates = (IList<Candidate>) value.Message;

            // Assert
            Assert.True(candidates.Count == 3);
        }

        [Fact]
        public void GetCandidateAsync_Returns_Right_candidates()
        {
            // Arrange
            Guid guidToBeSearched = new Guid("25398c72-e488-4139-89fd-1c1d30b42307");

            // Act
            var httpResult = _controller.GetCandidateAsync(guidToBeSearched);
            CommandResult value = (httpResult.Result as OkObjectResult).Value as CommandResult;
            var candidate = (Candidate)value.Message;

            // Assert
            Assert.True(candidate.Id == guidToBeSearched);
            Assert.True(candidate.Skills.Count == 3);
        }

        [Fact]
        public void DeleteCandidateAsync_Returns_Right_candidates()
        {
            // Arrange
            Guid guidToBeDeleted = new Guid("25398c72-e488-4139-89fd-1c1d30b42307");

            // Act
            var httpResult = _controller.DeleteCandidateAsync(guidToBeDeleted);

            httpResult = _controller.GetCandidateAsync(guidToBeDeleted);
            var value = (httpResult.Result as NotFoundObjectResult).Value as CommandResult;

            Assert.True(!value.Success);
        }

        [Fact]
        public void UpdateCandidateAsync_Returns_Right_candidates()
        {
            // Arrange
            Guid guidCandidateToBeUpdated = new Guid("25398c72-e488-4139-89fd-1c1d30b42307");
            Guid guidMentorToBeUpdated = new Guid("10364234-c868-4b25-b99c-dba0755ade69");
            Candidate candidateToBeUpdated = new Candidate(guidCandidateToBeUpdated, "c4", guidMentorToBeUpdated, "c4@email.com", "Santo André", "SP", "Estudante", true, 980, "12345678");
            Skill skillCloud = new Skill("Cloud");
            candidateToBeUpdated.AddSkill(skillCloud);

            // Act
            var httpResult = _controller.UpdateCandidateAsync(candidateToBeUpdated);
            var updateValue = (httpResult.Result as OkObjectResult).Value as CommandResult;

            httpResult = _controller.GetCandidateAsync(guidCandidateToBeUpdated);
            var getCandidateValue = (httpResult.Result as OkObjectResult).Value as CommandResult;
            var newCandidate = (Candidate)getCandidateValue.Message;

            Assert.True(updateValue.Success);
            Assert.True(newCandidate.Id == guidCandidateToBeUpdated);
            Assert.True(newCandidate.IdMentor == guidMentorToBeUpdated);
            Assert.Equal("c4", newCandidate.Name);
            Assert.Equal("c4@email.com", newCandidate.Email);
            Assert.True(newCandidate.Skills.Count == 1);
            Assert.True(newCandidate.Skills.Contains(skillCloud));
        }
    }
}
