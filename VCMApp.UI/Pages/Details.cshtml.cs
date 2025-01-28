using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VCMApp.Application.Applicants.Vacancy;
using VCMApp.LightDomain.DTOs;

namespace VCMApp.UI.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly IMediator _mediator;

        public DetailsModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public VacancyDetailDto Vacancy { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var result = await _mediator.Send(new GetActiveVacancyQuery { Id = id });

            if (!result.IsSuccess)
            {
                return NotFound();
            }

            Vacancy = result.Value;

            return Page();
        }
    }

}
