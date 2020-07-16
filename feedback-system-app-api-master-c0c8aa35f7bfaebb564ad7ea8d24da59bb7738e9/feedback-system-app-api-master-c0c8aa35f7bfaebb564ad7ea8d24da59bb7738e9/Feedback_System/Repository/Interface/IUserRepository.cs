using Feedback_System.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Feedback_System.Repository.Interface
{
    public interface IUserRepository
    {
        List<EmployeeUserMaster> GetUserData();
        List<Questions> GetQuestionData();
        List<Questions> PostQuestionData(Questions questions, List<Answers> answersList);
        List<Questions> DeleteQuestionData(int index);
        List<Questions> UpdateQuestionData(int index, Questions dbQuestion);
        void PostUserData(List<EmployeeUserMaster> userMasters);
        void PostFeedbackData(List<Feedback> feedbackEntities);
        List<Feedback> GetFeedbackData();

    }

}
