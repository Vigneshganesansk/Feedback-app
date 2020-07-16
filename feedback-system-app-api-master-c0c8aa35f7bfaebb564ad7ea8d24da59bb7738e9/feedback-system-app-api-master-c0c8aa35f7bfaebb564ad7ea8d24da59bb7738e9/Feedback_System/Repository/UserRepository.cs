using Feedback_System.Entities;
using Feedback_System.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Feedback_System.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly FeedbackDbContext dbContext;

        public UserRepository(FeedbackDbContext _context)
        {
            dbContext = _context;
        }

        public List<EmployeeUserMaster> GetUserData()
        {
            return dbContext.EmployeeUserMaster?.ToList<EmployeeUserMaster>();
        }

        public List<Questions> GetQuestionData()
        {
            return dbContext.Questions.Include(m => m.Answers).ToList();
        }
        public List<Questions> PostQuestionData(Questions questions, List<Answers> answersList)
        {
            questions.Answers = null;
            dbContext.Questions.AddRange(questions);
            dbContext.SaveChanges();

            int qId = dbContext.Questions.Where(x => x.QuestionTextArea == questions.QuestionTextArea).Select(x => x.QId).FirstOrDefault();
            answersList.ForEach(answers =>
            {
                answers.QIdQuestion = qId;
                dbContext.Answers.Add(answers);
            });
            dbContext.SaveChanges();
            return dbContext.Questions.Include(m => m.Answers).ToList();

        }
        public List<Questions> UpdateQuestionData(int index, Questions dbQuestion)
        {
            this.DeleteQuestionData(index);
            return this.PostQuestionData(dbQuestion, dbQuestion.Answers.ToList());
        }

        public List<Questions> DeleteQuestionData(int index)
        {
            dbContext.Answers.RemoveRange(dbContext.Answers.Where(x => x.QIdQuestion == index).ToList());
            dbContext.SaveChanges();
            dbContext.Questions.RemoveRange(dbContext.Questions.Where(x => x.QId == index));
            dbContext.SaveChanges();

            return dbContext.Questions.Include(m => m.Answers).ToList();
        }

        public void PostUserData(List<EmployeeUserMaster> userMasters)
        {
            dbContext.EmployeeUserMaster.AddRange(userMasters);
            dbContext.SaveChanges();
        }

        public void PostFeedbackData(List<Feedback> feedbackEntities)
        {
            dbContext.Feedback.AddRange(feedbackEntities);
            dbContext.SaveChanges();
        }

        public List<Feedback> GetFeedbackData()
        {
            return dbContext.Feedback.ToList<Feedback>();
        }
    }

}
