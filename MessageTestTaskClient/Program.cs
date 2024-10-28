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
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
    }
}
