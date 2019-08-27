using SEDC.Loto3000.Models;

namespace SEDC.Loto3000.DataLayer.Contracts
{
    public interface IUserRepository
    {
        User GetUser(string email);
    }
}
