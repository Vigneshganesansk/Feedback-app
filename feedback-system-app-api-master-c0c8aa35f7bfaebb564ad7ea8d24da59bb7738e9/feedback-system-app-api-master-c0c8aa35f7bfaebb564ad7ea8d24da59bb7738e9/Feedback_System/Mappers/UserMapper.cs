using Feedback_System.Entities;
using Feedback_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Feedback_System.Mappers
{
    public class UserMapper
    {
        public UserModel MapUserEntityToUserModel(EmployeeUserMaster userEntity)
        {
            UserModel userModel = new UserModel();
            userModel.AssociateId = userEntity.AssociateId;
            userModel.AssociateName = userEntity.AssociateName;
            userModel.Email = userEntity.Email;
            userModel.Designation = userEntity.Designation;
            userModel.RoleId = userEntity.RoleId;
            userModel.Buid = userEntity.Buid;
            userModel.AddedUserId = userEntity.AddedUserId;
            userModel.AddedDate = userEntity.AddedDate;
            return userModel;
        }
    }
}
