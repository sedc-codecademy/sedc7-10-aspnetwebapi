using DataModels;
using Models;

namespace Mappers
{
    public static class UserMapper
    {
        public static UserModel ToModel(this User user)
        {
            return new UserModel
            {
                Id = user.Id,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }
    }
}
