using Feedback_System.Controllers;
using Feedback_System.Models;
using Feedback_System.Services.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace UnitTestProject1
{
    [TestClass]
    public class UserControllerTest
    {
        [TestClass]
        public class UserControllerTests
        {
            private UserController userControl;
            private Mock<IUserService> userService;
            private Mock<IExcelUploadService> uploadService;

            [TestInitialize]
            public void Initialize()
            {
                userService = new Mock<IUserService>();
                uploadService = new Mock<IExcelUploadService>();
                userControl = new UserController(userService.Object, uploadService.Object);
            }

            [TestMethod]
            public void GetUserData_Success()
            {
                userService.Setup(us => us.GetUserData()).Returns(getRecords(1));
                var userList = userControl.GetUserData().Value;
                Assert.IsTrue(userList.Count > 0);
            }

            [TestMethod]
            public void GetLoggedInUserData_Success()
            {
                userService.Setup(us => us.GetLoggedInUserData(It.IsAny<string>(), It.IsAny<string>())).Returns(getLoggedInUser(1));
                var user = userControl.GetLoggedInUserData().Value;
                Assert.IsTrue(user.AssociateId == 12345);
            }

            private List<UserModel> getRecords(int choice)
            {
                if (choice == 1)
                {
                    List<UserModel> users = new List<UserModel>();
                    users.Add(new UserModel());
                    return users;
                }
                return null;
            }

            private UserModel getLoggedInUser(int choice)
            {
                if (choice == 1)
                {
                    UserModel users = new UserModel();
                    users.AssociateId = 12345;
                    return users;
                }
                return null;
            }
        }

    }
}
