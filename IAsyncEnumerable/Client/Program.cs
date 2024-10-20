namespace Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Reflection;
    using System.Threading.Tasks;
    using IAsyncEnumerableWebAPI.Models;

    class Program
    {
        private static readonly HttpClient _client = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:5211/book/"),
        };

        private static async Task<IEnumerable<string>> GetAll()
        {
            var res = await _client.GetAsync("");

            var books = await res.Content.ReadFromJsonAsync<IEnumerable<Book>>();

            return books.Select(b => b.Title);
        }

        private static async IAsyncEnumerable<string> GetAsyncRecommendations(
            params string[] genres
        )
        {
            foreach (var genre in genres)
            {
                var res = await _client.GetAsync($"Recommendations/{genre}");

                var books = await res.Content.ReadFromJsonAsync<IEnumerable<Book>>();

                foreach (var b in books)
                {
                    yield return b.Title;
                }
            }
        }

        private static async Task<IEnumerable<string>> GetRecommendations(params string[] genres)
        {
            var title = new List<string>();

            foreach (var genre in genres)
            {
                var res = await _client.GetAsync($"Recommendations/{genre}");

                var books = await res.Content.ReadFromJsonAsync<IEnumerable<Book>>();
                title.AddRange(books.Select(b => b.Title));
            }
            return title;
        }

        static async Task Main()
        {
            Console.WriteLine("Press return when server ready.");
            Console.ReadLine();

            await foreach (var title in GetAsyncRecommendations("Horror", "SF"))
            {
                Console.WriteLine(title);
            }
            //var titles = await GetRecommendations("Horror", "SF");
            //foreach (var title in await GetRecommendations("Horror", "SF"))
            //{
            //    Console.WriteLine(title);
            //}

            Console.WriteLine();
            Console.WriteLine("Complete");
            Console.ReadLine();
        }
    }
}
