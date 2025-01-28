
namespace VCMApp.LightDomain.Entities
{
    public class ExamQuestionOption
    {
        public int Id { get; set; }
        public int ExamQuestionId { get; set; }
        public string Content { get; set; } = null!;
        public short PriorityNumber { get; set; }
        public bool IsCorrect { get; set; }
        public string CreatedBy { get; set; }

        public ExamQuestion ExamQuestion { get; set; } = null!;
    }
}
