using Feedback_System.Entities;
using Feedback_System.Repository.Interface;
using Feedback_System.Services;
using Feedback_System.Services.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTestProject1
{
    [TestClass]
    public class UserServicesTests
    {
        private IUserService userService;
        private Mock<IUserRepository> userRepository;

        [TestInitialize]
        public void Initialize()
        {
            userRepository = new Mock<IUserRepository>();
            userService = new UserService(userRepository.Object);
        }

        [TestMethod]
        public void GetUserData_Success()
        {
            userRepository.Setup(ur => ur.GetUserData()).Returns(getRecords(1));
            var userList = userService.GetUserData();
            Assert.IsTrue(userList.Count > 0);
        }

        [TestMethod]
        public void GetQuestionData_Success()
        {
            userRepository.Setup(ur => ur.GetQuestionData()).Returns(getQuestions(1));
            var questions = userService.GetQuestionData();
            Assert.IsTrue(questions.Count > 0);
        }

        private List<EmployeeUserMaster> getRecords(int choice)
        {
            if (choice == 1)
            {
                List<EmployeeUserMaster> users = new List<EmployeeUserMaster>();
                users.Add(new EmployeeUserMaster());
                return users;
            }
            return null;
        }

        private List<Questions> getQuestions(int choice)
        {
            if (choice == 1)
            {
                List<Questions> questions = new List<Questions>();
                questions.Add(new Questions());
                return questions;
            }
            return null;
        }
    }

}
