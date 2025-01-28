namespace VCMApp.LightDomain.DTOs
{
    public class ExamQuestionDto
    {
        public int QuestionId { get; set; }
        public string QuestionContent { get; set; }
        public List<QuestionOption> Options { get; set; }
    }
    public class QuestionOption
    {
        public int QuestionOptionId { get; set; }
        public string Content { get; set; }
        public bool IsCorrect { get; set; }
        public int QuestionId { get; set; }
        public int PriorityNumber { get; set; }
    }
}
