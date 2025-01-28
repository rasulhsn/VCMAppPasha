using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VCMApp.Application.Applicants.Vacancy;
using VCMApp.LightDomain.DTOs;

namespace VCMApp.UI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IMediator _mediator;

        public IndexModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public List<VacancyDto> ActiveVacancies { get; set; } = new();

        public async Task OnGetAsync()
        {
            var result = await _mediator.Send(new GetActiveVacanciesQuery());
            ActiveVacancies = result.Value ?? new List<VacancyDto>();
        }
    }
}
