
namespace VCMApp.LightDomain.Entities
{
    public class Application
    {
        public int Id { get; set; }
        public int ApplicantId { get; set; }
        public int VacancyId { get; set; }
        public Guid SessionGuid { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; } = true;
        public short ExamQuestionCount { get; set; }

        public Applicant Applicant { get; set; } = null!;
        public Vacancy Vacancy { get; set; } = null!;
        public ApplicantExamResult ApplicantExamResult { get; set; } = null!;
        public ICollection<ApplicantExamAnswer> ApplicantExamAnswers { get; set; } = new List<ApplicantExamAnswer>();
    }
}
