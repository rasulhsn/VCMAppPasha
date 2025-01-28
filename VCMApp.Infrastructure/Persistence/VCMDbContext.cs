using Microsoft.EntityFrameworkCore;
using VCMApp.LightDomain.Entities;

namespace VCMApp.Infrastructure.Persistence
{
    public class VCMDbContext : DbContext
    {
        public VCMDbContext(DbContextOptions<VCMDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Vacancy> Vacancies { get; set; }
        public DbSet<VacancyExamDetail> VacancyExamDetails { get; set; }
        public DbSet<ExamQuestion> ExamQuestions { get; set; }
        public DbSet<ExamQuestionOption> ExamQuestionOptions { get; set; }
        public DbSet<VacancyExamQuestion> VacancyExamQuestions { get; set; }
        public DbSet<Applicant> Applicants { get; set; }
        public DbSet<ApplicantCV> ApplicantCVs { get; set; }
        public DbSet<LightDomain.Entities.Application> Applications { get; set; }
        public DbSet<ApplicantExamAnswer> ApplicantExamAnswers { get; set; }
        public DbSet<ApplicantExamResult> ApplicantExamResults { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                    .HasIndex(c => c.Name)
                    .IsUnique();

            modelBuilder.Entity<Vacancy>()
                .HasOne(v => v.Category)
                .WithMany(c => c.Vacancies)
                .HasForeignKey(v => v.CategoryId);

            modelBuilder.Entity<VacancyExamDetail>()
                    .HasOne(v => v.Vacancy)
                    .WithOne(v => v.VacancyExamDetail)
                    .HasForeignKey<VacancyExamDetail>(v => v.VacancyId);

            modelBuilder.Entity<ExamQuestion>()
                .HasOne(e => e.Category)
                .WithMany(c => c.ExamQuestions)
                .HasForeignKey(e => e.CategoryId);

            modelBuilder.Entity<ExamQuestionOption>()
                .HasOne(e => e.ExamQuestion)
                .WithMany(eq => eq.ExamQuestionOptions)
                .HasForeignKey(e => e.ExamQuestionId);

            modelBuilder.Entity<VacancyExamQuestion>()
                .HasOne(v => v.Vacancy)
                .WithMany(v => v.VacancyExamQuestions)
                .HasForeignKey(v => v.VacancyId);

            modelBuilder.Entity<VacancyExamQuestion>()
                .HasOne(v => v.ExamQuestion)
                .WithMany(e => e.VacancyExamQuestions)
                .HasForeignKey(v => v.ExamQuestionId);

            modelBuilder.Entity<LightDomain.Entities.Application>()
                .HasOne(a => a.Applicant)
                .WithMany(a => a.Applications)
                .HasForeignKey(a => a.ApplicantId);

            modelBuilder.Entity<LightDomain.Entities.Application>()
                .HasOne(a => a.Vacancy)
                .WithMany(v => v.Applications)
                .HasForeignKey(a => a.VacancyId);

            modelBuilder.Entity<ApplicantExamAnswer>()
                .HasOne(a => a.Application)
                .WithMany(a => a.ApplicantExamAnswers)
                .HasForeignKey(a => a.ApplicationId);

            modelBuilder.Entity<ApplicantExamAnswer>()
                .HasOne(a => a.ExamQuestion)
                .WithMany(eq => eq.ApplicantExamAnswers)
                .HasForeignKey(a => a.ExamQuestionId);

            modelBuilder.Entity<ApplicantExamResult>()
                .HasOne(a => a.Application)
                .WithOne(a => a.ApplicantExamResult)
                .HasForeignKey<ApplicantExamResult>(a => a.ApplicationId);

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ExamQuestionRaw>(entity =>
            {
                entity.HasNoKey();
                entity.ToView(null);
            });

            modelBuilder.Entity<ApplicationExamResultRaw>(entity =>
            {
                entity.HasNoKey();
                entity.ToView(null);
            });

            modelBuilder.Entity<QuestionAnswerDetailRaw>(entity =>
            {
                entity.HasNoKey();
                entity.ToView(null);
            });
        }
    }
}
