using SEDC.Loto3000.Models;
using System.Threading.Tasks;

namespace SEDC.Loto3000.ApiClient.Contracts
{
    public interface ILoto3000ApiClient
    {
        Task<string> AuthenticateAsync(string email, string password);
        Task<Draw> CreateDrawAsync(string userToken);
    }
}
