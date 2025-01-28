using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VCMApp.Infrastructure.Entities
{
    public class QuestionAnswerDetailRaw
    {
        public string QuestionContent { get; set; }
        public string AnswerContent { get; set; }
        public bool AnswerIsCorrect { get; set; }
    }
}
