using SEDC.Loto3000.ApiClient;
using SEDC.Loto3000.ApiClient.Contracts;
using System;

namespace SEDC.Loto3000.AdminPanel
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the SEDC.Loto3000.AdminPanel!");

            Console.Write("Please enter your email: ");
            string email = Console.ReadLine();

            Console.Write("Please enter your password: ");
            string password = Console.ReadLine();

            var baseApiAddress = @"http://localhost:60884/api";//TODO: read from appsettings
            ILoto3000ApiClient apiClient = new Loto3000ApiClient(baseApiAddress);//TODO: resolve this with IoC (serviceContainer)
            string loggedUserToken = apiClient.AuthenticateAsync(email, password)
                                            .GetAwaiter()
                                            .GetResult();
            if (!string.IsNullOrWhiteSpace(loggedUserToken))
                Console.WriteLine($"You are logged in, your token: {loggedUserToken}");//TODO: read fullName from token claims

            var draw = apiClient.CreateDrawAsync(loggedUserToken)
                                .GetAwaiter()
                                .GetResult();

            Console.WriteLine($"Draw with id: {draw.Id} was created");

            Console.ReadLine();
        }
    }
}
