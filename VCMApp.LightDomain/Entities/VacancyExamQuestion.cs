
namespace VCMApp.LightDomain.Entities
{
    public class VacancyExamQuestion
    {
        public int Id { get; set; }
        public int VacancyId { get; set; }
        public int ExamQuestionId { get; set; }
        public string CreatedBy { get; set; }

        public Vacancy Vacancy { get; set; } = null!;
        public ExamQuestion ExamQuestion { get; set; } = null!;
    }
}
