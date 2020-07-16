using Feedback_System.Entities;
using Feedback_System.Mappers;
using Feedback_System.Models;
using Feedback_System.Repository.Interface;
using Feedback_System.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Feedback_System.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly UserMapper userMapper;

        public UserService(IUserRepository _userRepository)
        {
            userRepository = _userRepository;
        }
        public List<UserModel> GetUserData()
        {
            UserMapper userMapper = new UserMapper();
            List<EmployeeUserMaster> userEntityList = userRepository.GetUserData();
            List<UserModel> allUserList = new List<UserModel>();
            userEntityList.ForEach(x => {
                allUserList.Add(userMapper.MapUserEntityToUserModel(x));
            });
            return allUserList;
        }
        public UserModel GetLoggedInUserData(string id, string password)
        {
            UserMapper userMapper = new UserMapper();
            EmployeeUserMaster userEntity = userRepository.GetUserData().FirstOrDefault(x => x.AssociateId.ToString() == id && !x.IsDeleted);
            Encryptor encryptObj = new Encryptor();
            string encryptPass = encryptObj.getHash(password + userEntity.Salt);
            if (userEntity != null && encryptPass.Equals(userEntity.Password))
            {
                return userMapper.MapUserEntityToUserModel(userEntity);
            }
            else
                return null;
        }

        public List<QuestionModel> GetQuestionData()
        {
            QuestionMapper questionMapper = new QuestionMapper();
            List<QuestionModel> questionModel = questionMapper.MapUserEntityToQuestionModel(userRepository.GetQuestionData());
            return questionModel;
        }

        public List<QuestionModel> PostQuestionData(QuestionModel question)
        {
            QuestionMapper questionMapper = new QuestionMapper();
            Questions dbQuestion = new Questions();
            dbQuestion = questionMapper.MapQuestionModelToEntity(question);
            List<QuestionModel> questionModel = questionMapper.MapUserEntityToQuestionModel(userRepository.PostQuestionData(dbQuestion, dbQuestion.Answers.ToList()));
            return questionModel;
        }
        public List<QuestionModel> DeleteQuestionData(int index)
        {
            QuestionMapper questionMapper = new QuestionMapper();
            Questions dbQuestion = new Questions();
            List<QuestionModel> questionModel = questionMapper.MapUserEntityToQuestionModel(userRepository.DeleteQuestionData(index));
            return questionModel;
        }

        public List<QuestionModel> UpdateQuestionData(int index, QuestionModel updateQuestion)
        {
            QuestionMapper questionMapper = new QuestionMapper();
            Questions dbQuestion = new Questions();
            dbQuestion = questionMapper.MapQuestionModelToEntity(updateQuestion);
            List<QuestionModel> questionModel = questionMapper.MapUserEntityToQuestionModel(userRepository.UpdateQuestionData(index, dbQuestion));
            return questionModel;
        }

        public void PostFeedbackData(List<FeedbackModel> feedbackList)
        {
            QuestionMapper questionMapper = new QuestionMapper();
            List<Feedback> feedbackEntities = new List<Feedback>();
            feedbackEntities = questionMapper.MapFeedbackModelToEntity(feedbackList);
            userRepository.PostFeedbackData(feedbackEntities);
        }

        public void SendMail(List<string> mailList)
        {
            foreach (string mail in mailList)
            {
                try
                {
                    MailMessage message = new MailMessage();
                    SmtpClient smtp = new SmtpClient();
                    message.From = new MailAddress("FROM@gmail.com");
                    message.To.Add(new MailAddress(mail));
                    message.Subject = "Test-Mail from C#";
                    message.IsBodyHtml = true; //to make message body as html  
                    message.Body = "Test";
                    smtp.Port = 587;
                    smtp.Host = "smtp.gmail.com"; //for gmail host  
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential("FROM@gmail.com", "**");
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Send(message);
                }
                catch (Exception e) { }
            }
        }
    }



}
