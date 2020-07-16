using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Feedback_System.Models
{
    public class QuestionModel
    {
        public int Q_ID { get; set; }
        public string Questions { get; set; }
        public int? TotalAnswers { get; set; }
        public string FBType { get; set; }
        public AnswersFormValue Answers { get; set; }
    }
    public class AnswersFormValue
    {
        public string radioAns { get; set; }
        public bool? multipleAnswer { get; set; }
        public bool? freeTextAnswer { get; set; }
        public bool? customQuestion { get; set; }
        public string questionTextArea { get; set; }
        public List<AnswerArray> answerArray { get; set; }
    }
    public class AnswerArray
    {
        public string answerTextArea { get; set; }
    }
}
