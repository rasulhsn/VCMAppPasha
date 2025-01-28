
namespace VCMApp.LightDomain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string CreatedBy { get; set; }

        public ICollection<Vacancy> Vacancies { get; set; } = new List<Vacancy>();
        public ICollection<ExamQuestion> ExamQuestions { get; set; } = new List<ExamQuestion>();
    }
}
