using Moq;
using VCMApp.Application.Applicants.Vacancy;
using VCMApp.Application.Contracts;
using VCMApp.LightDomain.Entities;

namespace VCMApp.Application.Tests
{
    [TestFixture]
    public class CloseApplicationCommandHandlerTests
    {
        private Mock<IApplicationRepository> _applicationRepositoryMock;
        private Mock<IApplicantRepository> _applicantRepositoryMock;
        private Mock<IExamQuestionRepository> _examQuestionRepositoryMock;
        private CloseApplicationCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            _applicationRepositoryMock = new Mock<IApplicationRepository>();
            _applicantRepositoryMock = new Mock<IApplicantRepository>();
            _examQuestionRepositoryMock = new Mock<IExamQuestionRepository>();

            _handler = new CloseApplicationCommandHandler(
                _applicationRepositoryMock.Object,
                _applicantRepositoryMock.Object,
                _examQuestionRepositoryMock.Object);
        }

        [Test]
        public async Task Handle_ShouldReturnSuccess_WhenSessionIsInactive()
        {
            // Arrange
            var command = new CloseApplicationCommand
            {
                ApplicationSessionGuid = Guid.NewGuid()
            };

            _applicationRepositoryMock
                .Setup(repo => repo.GetBySessionGuid(command.ApplicationSessionGuid))
                .ReturnsAsync(new LightDomain.Entities.Application
                {
                    IsActive = false
                });

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            _applicationRepositoryMock.Verify(repo => repo.Update(It.IsAny<LightDomain.Entities.Application>()), Times.Never);
            _applicantRepositoryMock.Verify(repo => repo.AddApplicantExamResult(It.IsAny<ApplicantExamResult>()), Times.Never);
        }

        [Test]
        public async Task Handle_ShouldReturnSuccess_WhenApplicationIsClosedSuccessfully()
        {
            // Arrange
            var command = new CloseApplicationCommand
            {
                ApplicationSessionGuid = Guid.NewGuid()
            };

            var sessionApp = new LightDomain.Entities.Application
            {
                Id = 1,
                IsActive = true,
                ExamQuestionCount = 5
            };

            var answers = new List<ApplicantExamAnswer>
            {
                new ApplicantExamAnswer { ExamQuestionId = 1, SelectedExamQuestionOptionId = 1 },
                new ApplicantExamAnswer { ExamQuestionId = 2, SelectedExamQuestionOptionId = 2 },
                new ApplicantExamAnswer { ExamQuestionId = 3, SelectedExamQuestionOptionId = 3 }
            };

            _applicationRepositoryMock
                .Setup(repo => repo.GetBySessionGuid(command.ApplicationSessionGuid))
                .ReturnsAsync(sessionApp);

            _applicantRepositoryMock
                .Setup(repo => repo.GetApplicantAnswers(sessionApp.Id))
                .ReturnsAsync(answers);

            _examQuestionRepositoryMock
                .Setup(repo => repo.IsQuestionAnswerCorrect(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(true);

            _applicationRepositoryMock
                .Setup(repo => repo.Update(It.IsAny<LightDomain.Entities.Application>()))
                .Verifiable();

            _applicantRepositoryMock
                .Setup(repo => repo.AddApplicantExamResult(It.IsAny<ApplicantExamResult>()))
                .Verifiable();

            _applicantRepositoryMock
                .Setup(repo => repo.SaveChangesAsync())
                .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            _applicationRepositoryMock.Verify(repo => repo.Update(It.IsAny<LightDomain.Entities.Application>()), Times.Once);
            _applicantRepositoryMock.Verify(repo => repo.AddApplicantExamResult(It.IsAny<ApplicantExamResult>()), Times.Once);
            _applicantRepositoryMock.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task Handle_ShouldReturnCorrectResult_WhenSomeAnswersAreIncorrect()
        {
            // Arrange
            var command = new CloseApplicationCommand
            {
                ApplicationSessionGuid = Guid.NewGuid()
            };

            var sessionApp = new LightDomain.Entities.Application
            {
                Id = 1,
                IsActive = true,
                ExamQuestionCount = 5
            };

            var answers = new List<ApplicantExamAnswer>
            {
                new ApplicantExamAnswer { ExamQuestionId = 1, SelectedExamQuestionOptionId = 1 },
                new ApplicantExamAnswer { ExamQuestionId = 2, SelectedExamQuestionOptionId = 2 },
                new ApplicantExamAnswer { ExamQuestionId = 3, SelectedExamQuestionOptionId = 3 },
                new ApplicantExamAnswer { ExamQuestionId = 4, SelectedExamQuestionOptionId = 4 },
                new ApplicantExamAnswer { ExamQuestionId = 5, SelectedExamQuestionOptionId = 5 }
            };

            _applicationRepositoryMock
                .Setup(repo => repo.GetBySessionGuid(command.ApplicationSessionGuid))
                .ReturnsAsync(sessionApp);

            _applicantRepositoryMock
                .Setup(repo => repo.GetApplicantAnswers(sessionApp.Id))
                .ReturnsAsync(answers);

            _examQuestionRepositoryMock
                .Setup(repo => repo.IsQuestionAnswerCorrect(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(false);

            _applicationRepositoryMock
                .Setup(repo => repo.Update(It.IsAny<LightDomain.Entities.Application>()))
                .Verifiable();

            _applicantRepositoryMock
                .Setup(repo => repo.AddApplicantExamResult(It.IsAny<ApplicantExamResult>()))
                .Verifiable();

            _applicantRepositoryMock
                .Setup(repo => repo.SaveChangesAsync())
                .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            _applicationRepositoryMock.Verify(repo => repo.Update(It.IsAny<LightDomain.Entities.Application>()), Times.Once);
            _applicantRepositoryMock.Verify(repo => repo.AddApplicantExamResult(It.IsAny<ApplicantExamResult>()), Times.Once);
            _applicantRepositoryMock.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task Handle_ShouldReturnSuccess_WhenNoAnswersExist()
        {
            // Arrange
            var command = new CloseApplicationCommand
            {
                ApplicationSessionGuid = Guid.NewGuid()
            };

            var sessionApp = new LightDomain.Entities.Application
            {
                Id = 1,
                IsActive = true,
                ExamQuestionCount = 5
            };

            _applicationRepositoryMock
                .Setup(repo => repo.GetBySessionGuid(command.ApplicationSessionGuid))
                .ReturnsAsync(sessionApp);

            _applicantRepositoryMock
                .Setup(repo => repo.GetApplicantAnswers(sessionApp.Id))
                .ReturnsAsync(new List<ApplicantExamAnswer>());

            _applicationRepositoryMock
                .Setup(repo => repo.Update(It.IsAny<LightDomain.Entities.Application>()))
                .Verifiable();

            _applicantRepositoryMock
                .Setup(repo => repo.AddApplicantExamResult(It.IsAny<ApplicantExamResult>()))
                .Verifiable();

            _applicantRepositoryMock
                .Setup(repo => repo.SaveChangesAsync())
                .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            _applicationRepositoryMock.Verify(repo => repo.Update(It.IsAny<LightDomain.Entities.Application>()), Times.Once);
            _applicantRepositoryMock.Verify(repo => repo.AddApplicantExamResult(It.IsAny<ApplicantExamResult>()), Times.Once);
            _applicantRepositoryMock.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }
    }
}
