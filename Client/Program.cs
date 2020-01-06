using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync().Wait();
        }

        static async Task MainAsync()
        {
            var httpClient = new HttpClient();
            var postUri = "https://localhost:5001/graphql";

            var query = @"
            {
            author(id: 1) {
                name
            }
            }";

            var postData = new { Query = query };
            var stringContent = new StringContent(JsonConvert.SerializeObject(postData), Encoding.UTF8, "application/json");

            var res = await httpClient.PostAsync(postUri, stringContent);
            if (res.IsSuccessStatusCode)
            {
                var content = await res.Content.ReadAsStringAsync();

                Console.WriteLine(content);
            }
            else
            {
                Console.WriteLine($"Error occurred... Status code:{res.StatusCode}");
            }
        }
    }
}
