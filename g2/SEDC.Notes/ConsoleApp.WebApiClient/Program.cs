using System;
using System.Threading.Tasks;
using WebApiClient;
using WebApiClient.Contracts;

namespace ConsoleApp.WebApiClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.Write("Enter username: ");
            string username = Console.ReadLine();

            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            var baseUri = @"http://localhost:64500/";//TODO: read from appsettings
            INotesWebApiClient apiClient = new NotesWebApiClient(baseUri);//TODO: resolve this using IoC

            var loggedUser = await apiClient.AuthenticateAsync(username, password);
            Console.WriteLine($"Hello {loggedUser.FullName}");

            var userNotes = await apiClient.GetNotesByUserAsync(loggedUser.Token);
            Console.WriteLine("Notes for the logged in user:");
            foreach (var note in userNotes)
            {
                Console.WriteLine($"{note.Text}");
            }

            Console.ReadLine();
                
            
        }
    }
}
