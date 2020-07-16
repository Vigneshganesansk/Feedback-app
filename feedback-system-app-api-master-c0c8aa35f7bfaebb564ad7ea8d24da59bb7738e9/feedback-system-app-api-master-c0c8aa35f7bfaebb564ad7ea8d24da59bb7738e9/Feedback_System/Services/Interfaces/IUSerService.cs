using Feedback_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Feedback_System.Services.Interfaces
{
    public interface IUserService
    {
        List<UserModel> GetUserData();
        UserModel GetLoggedInUserData(string id, string password);
        List<QuestionModel> GetQuestionData();
        List<QuestionModel> PostQuestionData(QuestionModel question);
        List<QuestionModel> DeleteQuestionData(int index);
        List<QuestionModel> UpdateQuestionData(int index, QuestionModel updateQuestion);
        void PostFeedbackData(List<FeedbackModel> feedbackList);
        void SendMail(List<string> mailList);

    }

}
