
namespace VCMApp.LightDomain.Entities
{
    public class ApplicantExamResult
    {
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public short CorrectAnswersCount { get; set; }
        public short IncorrectAnswersCount { get; set; }
        public short ResultPercentage { get; set; }

        public Application Application { get; set; }
    }
}
