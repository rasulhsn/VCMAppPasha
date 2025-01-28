using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VCMApp.Application.Admins.Exams;
using VCMApp.Application.Admins.Statistics;
using VCMApp.LightDomain.DTOs;

namespace VCMApp.UI.Areas.Admin.Pages
{
    [Authorize(Roles = "Admin")]
    public class ExamResultsModel : PageModel
    {
        private readonly IMediator _mediator;

        public ExamResultsModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public List<ExamResultDto> ExamResults { get; set; }

        public async Task OnGet()
        {
            var results = await _mediator.Send(new GetExamResultsQuery());
            ExamResults = results.Value ?? new List<ExamResultDto>();
        }

        public async Task<IActionResult> OnGetExamAnswers(int applicationId)
        {
            var questionResult = await _mediator.Send(new GetExamAnswersQuery() { ApplicationId = applicationId });
            return Partial("_ExamAnswersPartial", questionResult.Value);
        }
    }
}
