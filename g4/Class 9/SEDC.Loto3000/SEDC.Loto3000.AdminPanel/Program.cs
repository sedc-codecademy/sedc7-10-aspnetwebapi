using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.Loto3000.AdminPanel
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the SEDC.Loto3000.AdminPanel!");

            Console.Write("Please enter your email: ");
            string email = Console.ReadLine();

            Console.WriteLine("Please enter your password: ");
            string password = Console.ReadLine();

            using (var httpClient = new HttpClient())
            {
                var baseAddress = @"http://localhost:60884/api";//TODO: read from appsettings
                httpClient.BaseAddress = new Uri(baseAddress);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                using (var httpRequestMessage =
                            new HttpRequestMessage(HttpMethod.Post, new Uri($"{baseAddress}/user/authenticate")))
                {
                    var user = new { email, password };
                    httpRequestMessage.Content =
                        new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

                    var httpResponse = httpClient.SendAsync(httpRequestMessage)
                                                    .GetAwaiter()
                                                    .GetResult();
                    httpResponse.EnsureSuccessStatusCode();

                    string userToken = null;
                    if (httpResponse.Headers.TryGetValues("Authorization", out IEnumerable<string> values))
                    {
                        userToken = values.FirstOrDefault();
                        Console.WriteLine("You are logged in");//TODO: read fullName from token claims
                    }
                }
            }

            Console.ReadLine();
        }
    }
}
