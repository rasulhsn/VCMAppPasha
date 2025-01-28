using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VCMApp.Application.Applicants.Vacancy;

namespace VCMApp.UI.Pages
{
    public class ExamFinishModel : PageModel
    {
        private readonly IMediator _mediator;
        private readonly IWebHostEnvironment _environment;


        [BindProperty(SupportsGet = true)]
        public Guid SessionId { get; set; }

        public ExamFinishModel(IMediator mediator, IWebHostEnvironment environment)
        {
            _mediator = mediator;
            _environment = environment;
        }

        public async Task<IActionResult> OnGet(Guid sessionId)
        {
            HttpContext.Session.Clear();

            await _mediator.Send(new CloseApplicationCommand() { ApplicationSessionGuid = sessionId },
                                                        CancellationToken.None);

            return Page();
        }

        public async Task<IActionResult> OnPostUploadCvAsync(Guid sessionId,
                                                                IFormFile CVFile)
        {
            if (CVFile == null)
            {
                ModelState.AddModelError("CVFile", "Please select a file to upload.");
                return Page();
            }

            try
            {
                UploadFileCommand fileDataCommand = await ConvertToUploadFileCommandAsync(sessionId,CVFile);

                await _mediator.Send(fileDataCommand);

                return RedirectToPage("/CVUploadSuccess");
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("CVFile", ex.Message);
                return Page();
            }
        }

        private async Task<UploadFileCommand> ConvertToUploadFileCommandAsync(Guid sessionId, IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                return new UploadFileCommand
                {
                    SessionId = sessionId,
                    FileContent = memoryStream.ToArray(),
                    FileName = file.FileName,
                    ContentType = file.ContentType
                };
            }
        }
    }
}
