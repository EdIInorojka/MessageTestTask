using DotNetEnv;
using static System.Net.WebRequestMethods;
using System.Net;

namespace MessageTestTaskClient
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();

        static async Task Main(string[] args)
        {
            string apiUrl = Environment.GetEnvironmentVariable("API_URL");

            while (true)
            {
                await GetMessageAsync(apiUrl);
                await Task.Delay(1000);
            }
        }

        private static async Task GetMessageAsync(string url)
        {
            try
            {
                var response = await client.GetStringAsync(url);
                Console.WriteLine($"{response}");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Ошибка запроса: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
    }
}


//клиент на докере
/*message_test_task_client:
build:
context: .
dockerfile: MessageTestTaskClient / Dockerfile
     depends_on:
-message_test_task_server
     ports:
-"5000:5000"
     networks:
-app - network
     command: ["sh", "-c", "exec docker-compose run message_test_task_client"]
     tty: true
     stdin_open: true*/