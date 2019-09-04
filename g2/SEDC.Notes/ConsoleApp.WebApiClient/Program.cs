using Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

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

            using (var httpClient = new HttpClient())
            {
                var baseUri = @"http://localhost:64500/";
                httpClient.BaseAddress = new Uri(baseUri);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                using (var httpRequest = new HttpRequestMessage(HttpMethod.Post, $"{baseUri}api/users/authenticate"))
                {
                    var bodyRequest = new { username, password };
                    var stringContent = JsonConvert.SerializeObject(bodyRequest);
                    httpRequest.Content = new StringContent(stringContent, Encoding.UTF8, "application/json");

                    var httpResponse = await httpClient.SendAsync(httpRequest);
                    httpResponse.EnsureSuccessStatusCode();
                    var responseContent = await httpResponse.Content.ReadAsStringAsync();
                    var loggedUser = JsonConvert.DeserializeObject<UserModel>(responseContent);

                    Console.WriteLine($"Hello {loggedUser.FullName}");
                }
            }
                
            
        }
    }
}
