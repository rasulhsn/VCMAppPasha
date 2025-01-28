using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VCMApp.Application.Admins.Statistics;

namespace VCMApp.UI.Areas.Admin.Pages
{
    [Authorize(Roles = "Admin")]
    public class DashboardModel : PageModel
    {
        private readonly IMediator _mediator;

        public DashboardModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public int TotalVacancies { get; set; }
        public int AppliedApplicants { get; set; }
        public int FailedApplicants { get; set; }

        public async Task OnGetAsync()
        {
            var result = await _mediator.Send(new GetDashboardStatisticsQuery());

            if (result.IsSuccess)
            {
                TotalVacancies = result.Value.TotalVacancies;
                AppliedApplicants = result.Value.AppliedApplicants;
                FailedApplicants = result.Value.FailedApplicants;
            }
        }
    }
}
