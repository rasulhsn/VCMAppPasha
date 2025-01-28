using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using VCMApp.Application.Applicants.Vacancy;

namespace VCMApp.UI.Pages
{
    public class ApplyFormModel : PageModel
    {
        private readonly IMediator _mediator;

        public ApplyFormModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty]
        public ApplicantInputModel Applicant { get; set; }

        [BindProperty(SupportsGet = true)]
        public int VacancyId { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var applicantResult = await _mediator.Send(new ApplyApplicantCommand
            {
                VacancyId = VacancyId,
                FirstName = Applicant.FirstName,
                LastName = Applicant.LastName,
                PhoneNumber = Applicant.PhoneNumber,
                Email = Applicant.Email
            });

            if (applicantResult.IsSuccess)
            {
                int vacancyApplicantId = applicantResult.Value.ApplicantId; 
                return RedirectToPage("/ExamStart", new { vacancyId = VacancyId, applicantId = vacancyApplicantId });
            }

            ModelState.AddModelError("Main", applicantResult.Error);

            return Page();
        }

        public class ApplicantInputModel
        {
            [Required]
            [StringLength(50)]
            public string FirstName { get; set; }

            [Required]
            [StringLength(50)]
            public string LastName { get; set; }

            [Required]
            [Phone]
            public string PhoneNumber { get; set; }

            [Required]
            [EmailAddress]
            public string Email { get; set; }
        }
    }
}
