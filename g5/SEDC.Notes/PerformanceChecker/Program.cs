using System;
using System.Net.Http;

namespace PerformanceChecker
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Performance checks started...");
            Console.WriteLine("-----------------------------");
            CheckNotesPerformance();
            Console.ReadLine();

        }

        static void CheckNotesPerformance()
        {
            HttpClient client = new HttpClient();
            string address =
                "http://localhost:50890/api/external/performance/getnote";
            int limit = 10;

            HttpResponseMessage response = client.GetAsync(address).Result;
            string responseBody = response.Content.ReadAsStringAsync().Result;
            Console.ForegroundColor = ConsoleColor.Green;
            if (int.Parse(responseBody) > limit)
                Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Performance: {responseBody} [Limit: {limit}]");
        }
    }
}
