using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VCMApp.Application.Applicants.Vacancy;
using VCMApp.Application.DTOs;
using VCMApp.LightDomain.DTOs;
using VCMApp.LightDomain.DTOs.ErrorTypes;

namespace VCMApp.UI.Pages
{
    public class ExamModel : PageModel
    {
        private readonly IMediator _mediator;

        public ExamModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public ExamQuestionDto CurrentQuestion { get; set; }

        public int TimeInMinute { get; set; }

        [BindProperty]
        public int SelectedOptionId { get; set; }

        private int _currentQuestionIndex;

        public async Task<IActionResult> OnGetAsync(Guid sessionId, int timeInMinute, int questionIndex = 1, bool isInternal = false)
        {
            _currentQuestionIndex = questionIndex;
            TimeInMinute = timeInMinute;

            if (!isInternal && HttpContext.Session.TryGetValue("CurrentQuestion", out var currentQuestionBytes))
            {
                CurrentQuestion = System.Text.Json.JsonSerializer.Deserialize<ExamQuestionDto>(currentQuestionBytes);
            }
            else
            {
                var examQuestionResult = await _mediator.Send(new GetNextExamQuestionQuery
                { QuestionIndex = _currentQuestionIndex, SessionId = sessionId });

                if (examQuestionResult.IsSuccess)
                {
                    CurrentQuestion = examQuestionResult.Value;

                    HttpContext.Session.Set("CurrentQuestion",
                        System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(CurrentQuestion));

                    return RedirectToPage("Exam", new { QuestionIndex = _currentQuestionIndex, TimeInMinute = timeInMinute, IsInternal = false });
                }
                else if (examQuestionResult.Error == ExamErrorType.ExamIsExpired)
                {
                    HttpContext.Session.Clear();
                    return RedirectToPage("Error", new { message = "This exam is expired!" });
                }
                else
                {
                    CurrentQuestion = null;
                }
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid sessionId, int questionId,
            int SelectedOptionId, int questionIndex)
        {
            _currentQuestionIndex = questionIndex;

            var nextQuestionIndex = _currentQuestionIndex + 1;

            var response = await _mediator.Send(new AnswerExamQuestionCommand()
            {
                ApplicationSessionGuid = sessionId,
                QuestionId = questionId,
                AnswerOptionId = SelectedOptionId,
            });

            // Have to check error type for redirecting to error page of finish page!
            if(response.IsFailure)
            {
                HttpContext.Session.Clear();
                return RedirectToPage($"ExamFinish", new { SessionId = sessionId });
            }
            else
                return RedirectToPage(new { questionIndex = nextQuestionIndex, isInternal = true });
        }
    }

}
