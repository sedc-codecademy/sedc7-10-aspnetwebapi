using SEDC.Loto3000.Models;

namespace SEDC.Loto3000.BusinessLayer.Contracts
{
    public interface IUserService
    {
        void Register(User user);

        //TODO: remove the out parameter and create other user model for the result with token, without password
        User Get(string email, string password, out string token);
    }
}
