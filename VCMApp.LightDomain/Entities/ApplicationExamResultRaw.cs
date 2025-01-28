

namespace VCMApp.LightDomain.Entities
{
    public class ApplicationExamResultRaw
    {
        public int ApplicantExamResultId { get; set; }
        public int ApplicationId { get; set; }
        public int ApplicantId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set;}
        public string Email { get; set; }
        public string VacancyName { get; set; }
        public short CorrectAnswersCount { get; set; }
        public short InCorrectAnswersCount { get; set; }
        public short ResultPercentage { get; set; }
    }
}
