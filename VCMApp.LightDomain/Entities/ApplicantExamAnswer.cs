
namespace VCMApp.LightDomain.Entities
{
    public class ApplicantExamAnswer
    {
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public int ExamQuestionId { get; set; }
        public int? SelectedExamQuestionOptionId { get; set; }

        public Application Application { get; set; } = null!;
        public ExamQuestion ExamQuestion { get; set; } = null!;
        public ExamQuestionOption? SelectedExamQuestionOption { get; set; }
    }
}
