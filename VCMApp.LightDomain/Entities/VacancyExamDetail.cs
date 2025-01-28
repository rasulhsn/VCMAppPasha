
namespace VCMApp.LightDomain.Entities
{
    public class VacancyExamDetail
    {
        public int Id { get; set; }
        public int VacancyId { get; set; }
        public short ExamQuestionCount { get; set; }
        public short ExamQuestionTimeInMinute { get; set; }
        public int TotalTimeOfTestInMinute { get; set; }

        public Vacancy Vacancy { get; set; } = null!;
    }
}
