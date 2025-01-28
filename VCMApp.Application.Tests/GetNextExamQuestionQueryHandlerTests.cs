using Moq;
using VCMApp.Application.Applicants.Vacancy;
using VCMApp.Application.Contracts;
using VCMApp.LightDomain.DTOs.ErrorTypes;

namespace VCMApp.Application.Tests
{
    [TestFixture]
    public class GetNextExamQuestionQueryHandlerTests
    {
        private Mock<IVacancyRepository> _vacancyRepositoryMock;
        private Mock<IApplicationRepository> _applicationRepositoryMock;
        private Mock<IApplicantRepository> _applicantRepositoryMock;
        private GetNextExamQuestionQueryHandler _handler;

        [SetUp]
        public void Setup()
        {
            _vacancyRepositoryMock = new Mock<IVacancyRepository>();
            _applicationRepositoryMock = new Mock<IApplicationRepository>();
            _applicantRepositoryMock = new Mock<IApplicantRepository>();

            _handler = new GetNextExamQuestionQueryHandler(
                _vacancyRepositoryMock.Object,
                _applicationRepositoryMock.Object,
                _applicantRepositoryMock.Object);
        }

        [Test]
        public async Task Handle_ShouldReturnExamIsExpired_WhenSessionIsInactiveOrExpired()
        {
            // Arrange
            var query = new GetNextExamQuestionQuery
            {
                SessionId = Guid.NewGuid(),
                QuestionIndex = 1
            };

            _applicationRepositoryMock
                .Setup(repo => repo.GetBySessionGuid(query.SessionId))
                .ReturnsAsync(new LightDomain.Entities.Application
                {
                    IsActive = false,
                    EndDate = DateTime.UtcNow.AddDays(-1)
                });

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsTrue(result.IsFailure);
            Assert.AreEqual(ExamErrorType.ExamIsExpired, result.Error);
        }

        [Test]
        public async Task Handle_ShouldReturnExamFinished_WhenAllQuestionsAreAnswered()
        {
            // Arrange
            var query = new GetNextExamQuestionQuery
            {
                SessionId = Guid.NewGuid(),
                QuestionIndex = 1
            };

            _applicationRepositoryMock
                .Setup(repo => repo.GetBySessionGuid(query.SessionId))
                .ReturnsAsync(new LightDomain.Entities.Application
                {
                    Id = 1,
                    IsActive = true,
                    EndDate = DateTime.UtcNow.AddDays(1),
                    ExamQuestionCount = 5
                });

            _applicantRepositoryMock
                .Setup(repo => repo.GetApplicantAnswersCount(1))
                .ReturnsAsync(5);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsTrue(result.IsFailure);
            Assert.AreEqual(ExamErrorType.ExamFinished, result.Error);
        }

        [Test]
        public async Task Handle_ShouldReturnOccuredError_WhenExceptionIsThrown()
        {
            // Arrange
            var query = new GetNextExamQuestionQuery
            {
                SessionId = Guid.NewGuid(),
                QuestionIndex = 1
            };

            _applicationRepositoryMock
                .Setup(repo => repo.GetBySessionGuid(query.SessionId))
                .ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsTrue(result.IsFailure);
            Assert.AreEqual(ExamErrorType.OccuredError, result.Error);
        }
    }
}