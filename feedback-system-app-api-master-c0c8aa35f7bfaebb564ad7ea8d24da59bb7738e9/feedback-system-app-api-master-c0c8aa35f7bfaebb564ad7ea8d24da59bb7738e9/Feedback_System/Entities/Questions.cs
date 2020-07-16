using System;
using System.Collections.Generic;

namespace Feedback_System.Entities
{
    public partial class Questions
    {
        public Questions()
        {
            Answers = new HashSet<Answers>();
        }

        public int QId { get; set; }
        public string QuestionTextArea { get; set; }
        public int TotalAnswers { get; set; }
        public string Fbtype { get; set; }
        public string RadioAnswer { get; set; }
        public bool IsMultipleAnswer { get; set; }
        public bool IsFreeTextAnswer { get; set; }
        public bool IsCustomQuestion { get; set; }

        public ICollection<Answers> Answers { get; set; }
    }
}
