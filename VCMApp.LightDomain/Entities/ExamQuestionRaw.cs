namespace VCMApp.LightDomain.Entities
{
    public class ExamQuestionRaw
    {
        public string QuestionContent { get; set; }
        public int QuestionId { get; set; }
        public string Content { get; set; }
        public int QuestionOptionId { get; set; }
        public bool IsCorrect { get; set; }
        public Int16 PriorityNumber { get; set; }
    }
}
