using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApiClient.Contracts
{
    public interface INotesWebApiClient
    {
        Task<UserModel> AuthenticateAsync(string username, string password);

        Task<IEnumerable<NoteModel>> GetNotesByUserAsync(string userToken);
    }
}
