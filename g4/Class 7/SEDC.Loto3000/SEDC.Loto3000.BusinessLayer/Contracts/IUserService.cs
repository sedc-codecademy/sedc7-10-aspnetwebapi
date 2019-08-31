using SEDC.Loto3000.Models;

namespace SEDC.Loto3000.BusinessLayer.Contracts
{
    public interface IUserService
    {
        void Register(User user);

        string AuthenticateAndGetJwtToken(string email, string password);
    }
}
