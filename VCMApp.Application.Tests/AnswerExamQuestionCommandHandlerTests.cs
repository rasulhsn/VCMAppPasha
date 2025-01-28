using Moq;
using VCMApp.Application.Applicants.Vacancy;
using VCMApp.Application.Contracts;
using VCMApp.LightDomain.Entities;

namespace VCMApp.Application.Tests
{
    [TestFixture]
    public class AnswerExamQuestionCommandHandlerTests
    {
        private Mock<IApplicantRepository> _applicantRepositoryMock;
        private Mock<IApplicationRepository> _applicationRepositoryMock;
        private AnswerExamQuestionCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            _applicantRepositoryMock = new Mock<IApplicantRepository>();
            _applicationRepositoryMock = new Mock<IApplicationRepository>();

            _handler = new AnswerExamQuestionCommandHandler(
                _applicantRepositoryMock.Object,
                _applicationRepositoryMock.Object);
        }

        [Test]
        public async Task Handle_ShouldReturnFailure_WhenExamSessionIsInactiveOrExpired()
        {
            // Arrange
            var command = new AnswerExamQuestionCommand
            {
                ApplicationSessionGuid = Guid.NewGuid(),
                QuestionId = 1,
                AnswerOptionId = 1
            };

            _applicationRepositoryMock
                .Setup(repo => repo.GetBySessionGuid(command.ApplicationSessionGuid))
                .ReturnsAsync(new LightDomain.Entities.Application
                {
                    IsActive = false,
                    EndDate = DateTime.UtcNow.AddDays(-1)
                });

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsTrue(result.IsFailure);
            Assert.AreEqual("Exam session is no longer active.", result.Error);
        }

        [Test]
        public async Task Handle_ShouldReturnFailure_WhenQuestionDoesNotExist()
        {
            // Arrange
            var command = new AnswerExamQuestionCommand
            {
                ApplicationSessionGuid = Guid.NewGuid(),
                QuestionId = 1,
                AnswerOptionId = 1
            };

            _applicationRepositoryMock
                .Setup(repo => repo.GetBySessionGuid(command.ApplicationSessionGuid))
                .ReturnsAsync(new LightDomain.Entities.Application
                {
                    Id = 1,
                    IsActive = true,
                    EndDate = DateTime.UtcNow.AddDays(1)
                });

            _applicantRepositoryMock
                .Setup(repo => repo.GetApplicantAnswer(1, command.QuestionId))
                .ReturnsAsync((ApplicantExamAnswer)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsTrue(result.IsFailure);
            Assert.AreEqual("Question/Answer not exists!", result.Error);
        }

        [Test]
        public async Task Handle_ShouldReturnSuccess_WhenAnswerIsSubmittedSuccessfully()
        {
            // Arrange
            var command = new AnswerExamQuestionCommand
            {
                ApplicationSessionGuid = Guid.NewGuid(),
                QuestionId = 1,
                AnswerOptionId = 1
            };

            var existingAnswer = new ApplicantExamAnswer
            {
                Id = 1,
                ApplicationId = 1,
                ExamQuestionId = 1,
                SelectedExamQuestionOptionId = null
            };

            _applicationRepositoryMock
                .Setup(repo => repo.GetBySessionGuid(command.ApplicationSessionGuid))
                .ReturnsAsync(new LightDomain.Entities.Application
                {
                    Id = 1,
                    IsActive = true,
                    EndDate = DateTime.UtcNow.AddDays(1)
                });

            _applicantRepositoryMock
                .Setup(repo => repo.GetApplicantAnswer(1, command.QuestionId))
                .ReturnsAsync(existingAnswer);

            _applicantRepositoryMock
                .Setup(repo => repo.UpdateApplicantAnswer(It.IsAny<ApplicantExamAnswer>()))
                .Verifiable();

            _applicantRepositoryMock
                .Setup(repo => repo.SaveChangesAsync())
                .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            _applicantRepositoryMock.Verify(repo => repo.UpdateApplicantAnswer(It.IsAny<ApplicantExamAnswer>()), Times.Once);
            _applicantRepositoryMock.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task Handle_ShouldReturnFailure_WhenExceptionOccurs()
        {
            // Arrange
            var command = new AnswerExamQuestionCommand
            {
                ApplicationSessionGuid = Guid.NewGuid(),
                QuestionId = 1,
                AnswerOptionId = 1
            };

            _applicationRepositoryMock
                .Setup(repo => repo.GetBySessionGuid(command.ApplicationSessionGuid))
                .ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsTrue(result.IsFailure);
            Assert.AreEqual("Database error", result.Error);
        }
    }
}
