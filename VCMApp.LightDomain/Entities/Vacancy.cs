
namespace VCMApp.LightDomain.Entities
{
    public class Vacancy
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; } = true;
        public string CreatedBy { get; set; }

        public Category Category { get; set; } = null!;
        public VacancyExamDetail VacancyExamDetail { get; set; } = null!;
        public ICollection<VacancyExamQuestion> VacancyExamQuestions { get; set; } = new List<VacancyExamQuestion>();
        public ICollection<Application> Applications { get; set; } = new List<Application>();
    }
}
