
namespace VCMApp.LightDomain.Entities
{
    public class ExamQuestion
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string QuestionContent { get; set; } = null!;
        public short QuestionPoint { get; set; }
        public string CreatedBy { get; set; }

        public Category Category { get; set; } = null!;
        public ICollection<ExamQuestionOption> ExamQuestionOptions { get; set; } = new List<ExamQuestionOption>();
        public ICollection<VacancyExamQuestion> VacancyExamQuestions { get; set; } = new List<VacancyExamQuestion>();
        public ICollection<ApplicantExamAnswer> ApplicantExamAnswers { get; set; } = new List<ApplicantExamAnswer>();
    }
}
