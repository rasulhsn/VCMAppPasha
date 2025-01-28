using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VCMApp.Application.Applicants.Vacancy;
using VCMApp.Application.DTOs;
using VCMApp.LightDomain.DTOs;

namespace VCMApp.UI.Pages
{
    public class ExamStartModel : PageModel
    {
        private readonly IMediator _mediator;

        public ExamStartModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public VacancyExamDetailDto VacancyExamDetail { get; set; }

        public string FullName { get; set; }

        [BindProperty(SupportsGet = true)]
        public int VacancyId { get; set; }

        [BindProperty(SupportsGet = true)]
        public int ApplicantId { get; set; }

        public async Task<IActionResult> OnGetAsync(int vacancyId, int applicantId)
        {
            var result = await _mediator.Send(new GetExamDetailQueryQuery() { VacancyId = vacancyId});

            if (!result.IsSuccess)
            {
                return Redirect("/Error");
            }

            VacancyExamDetail = result.Value;

            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int vacancyId, int applicantId)
        {
            var result = await _mediator.Send(new CreateApplicationCommand() { VacancyId = vacancyId, ApplicantId = applicantId });

            if (!result.IsSuccess)
            {
                return Redirect("/Error");
            }


            return RedirectToPage("/Exam", new { sessionId = result.Value.ApplicationSessinGuid,
                timeInMinute = result.Value.ExamTotalTimeInMinute, questionIndex = 1 });
        }
    }
}
