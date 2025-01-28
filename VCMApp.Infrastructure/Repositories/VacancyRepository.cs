using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System.Dynamic;
using VCMApp.LightDomain.Entities;
using VCMApp.Infrastructure.Persistence;
using VCMApp.Application.Contracts;

namespace VCMApp.Infrastructure.Repositories
{
    public class VacancyRepository : Repository<Vacancy>, IVacancyRepository
    {
        private readonly VCMDbContext _context;
        private readonly IMapper _mapper;

        public VacancyRepository(VCMDbContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<TResult>> GetActiveVacanciesAsync<TResult>()
        {
            return await _context.Vacancies
                        .Where(v => v.IsActive && v.StartDate <= DateTime.UtcNow && v.EndDate >= DateTime.UtcNow)
                        .ProjectTo<TResult>(_mapper.ConfigurationProvider)
                        .ToListAsync();
        }

        public async Task<TResult> GetActiveVacancyAsync<TResult>(int id)
        {
            return await _context.Vacancies
                        .Where(v => v.Id == id && v.IsActive && v.StartDate <= DateTime.UtcNow && v.EndDate >= DateTime.UtcNow)
                        .ProjectTo<TResult>(_mapper.ConfigurationProvider)
                        .SingleOrDefaultAsync();
        }

        public async Task<TResult> GetVacancyExamDetailByVacancyId<TResult>(int vacancyId)
        {
            return await _context.VacancyExamDetails
                        .Where(v => v.VacancyId == vacancyId)
                        .ProjectTo<TResult>(_mapper.ConfigurationProvider)
                        .FirstOrDefaultAsync();
        }

        public async Task<Vacancy> GetVacancyWithDetailsAsync(int id)
        {
            return await _context.Vacancies.Include(x => x.VacancyExamDetail)
                                            .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<VacancyExamDetail> GetVacancyExamDetailByVacancyId(int vacancyId)
        {
            return await _context.VacancyExamDetails
                        .Where(v => v.VacancyId == vacancyId)
                        .FirstOrDefaultAsync();
        }

        public async Task<dynamic> GetNextQuestionAsync(int vacancyId)
        {
            var rawSql = @"
            SELECT 
                    EQ.QuestionContent,
                    EQ.Id AS QuestionId,
                    EQO.Content,
                    EQO.Id AS QuestionOptionId,
                    EQO.IsCorrect,
                    EQO.PriorityNumber
                FROM ExamQuestions AS EQ
                INNER JOIN ExamQuestionOptions AS EQO
                    ON EQO.ExamQuestionId = EQ.Id
                WHERE EQ.Id = (
                    SELECT TOP 1 VEQ.ExamQuestionId 
                    FROM VacancyExamQuestions AS VEQ
                    WHERE VEQ.VacancyId = {0}
                    ORDER BY NEWID()
                )ORDER BY NEWID()";

            var rawResults = await _context.Set<ExamQuestionRaw>()
                .FromSqlRaw(rawSql, vacancyId)
                .AsNoTracking()
                .ToListAsync();

            if (!rawResults.Any())
            {
                return null;
            }

            // Temporary implementation for encapsulation issue!
            dynamic questionDto = new ExpandoObject();
            questionDto.QuestionId = rawResults.First().QuestionId;
            questionDto.QuestionContent = rawResults.First().QuestionContent;

            List<ExpandoObject> answerDto = new List<ExpandoObject>();
            foreach (var item in rawResults)
            {
                dynamic _item = new ExpandoObject();
                _item.QuestionOptionId = item.QuestionOptionId;
                _item.Content = item.Content;
                _item.IsCorrect = item.IsCorrect;
                _item.PriorityNumber = item.PriorityNumber;
                _item.QuestionId = item.QuestionId;
                answerDto.Add(_item);
            }

            questionDto.Options = answerDto;
            return questionDto;
        }
    }
}
