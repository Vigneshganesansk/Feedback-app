using Feedback_System.Entities;
using Feedback_System.Repository;
using Feedback_System.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTestProject1
{
    [TestClass]
    public class UserRepositiryTest
    {
        private Mock<FeedbackDbContext> dbContext;
        private IUserRepository userRepo;

        public virtual DbSet<EmployeeUserMaster> users { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            dbContext = new Mock<FeedbackDbContext>();
            userRepo = new UserRepository(dbContext.Object);
        }

        [TestMethod]
        public void GetUserData_Failure()
        {
            dbContext.Setup(db => db.EmployeeUserMaster).Returns(users);
            var userList = userRepo.GetUserData();
            Assert.IsTrue(userList == null);
        }
    }

}
