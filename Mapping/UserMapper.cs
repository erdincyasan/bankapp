using System.Collections.Generic;
using System.Linq;
using Udemy.BankApp.Web.Data.Entities;
using Udemy.BankApp.Web.Models;

namespace Udemy.BankApp.Web.Mapping
{
    public class UserMapper : IUserMapper
    {
       

        public List<UserListModel> MapToUserList(List<ApplicationUser> usersEntity)
        {
            return usersEntity
                .Select(x => new UserListModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    SurName = x.SurName,
                }
                ).ToList();
        }

        public UserListModel MaptoUserModel(ApplicationUser userEntity)
        {
            return new UserListModel
            {
                Id = userEntity.Id,
                Name = userEntity.Name,
                SurName = userEntity.SurName,
            };
        }
    }
}
