using Microsoft.EntityFrameworkCore;
using VCMApp.LightDomain.Entities;
using VCMApp.Infrastructure.Persistence;
using VCMApp.Application.Contracts;

namespace VCMApp.Infrastructure.Repositories
{
    public class ExamQuestionRepository : Repository<ExamQuestion>, IExamQuestionRepository
    {
        private readonly VCMDbContext _context;

        public ExamQuestionRepository(VCMDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> IsQuestionAnswerCorrect(int questionId, int optionId)
        {
            var option = await _context.ExamQuestionOptions.FirstOrDefaultAsync(x => x.ExamQuestionId == questionId &&
                                                                x.Id == optionId);
            if (option != null && option.IsCorrect)
                return true;

            return false;
        }
    }
}
