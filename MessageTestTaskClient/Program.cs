
namespace MessageTestTaskClient
{
    class Program
    {
        // Статическое поле HttpClient для выполнения HTTP-запросов
        private static readonly HttpClient client = new HttpClient();

        // Асинхронный метод Main, который является точкой входа в приложение
        static async Task Main(string[] args)
        {
            // Получение URL API из переменных окружения
            string apiUrl = Environment.GetEnvironmentVariable("API_URL");

            // Бесконечный цикл для периодического выполнения запроса
            while (true)
            {
                // Асинхронный вызов метода для получения сообщения
                await GetMessageAsync(apiUrl);

                // Задержка на 1 секунду перед следующим запросом
                await Task.Delay(1000);
            }
        }

        // Асинхронный метод для выполнения GET-запроса по указанному URL
        private static async Task GetMessageAsync(string url)
        {
            try
            {
                // Выполнение асинхронного запроса и получение ответа в виде строки
                var response = await client.GetStringAsync(url);
                WriteMessageToConsole(response);
            }
            catch (HttpRequestException ex)
            {
                // Обработка исключений, связанных с HTTP-запросами
                Console.WriteLine($"Ошибка запроса: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Обработка всех остальных исключений
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        const string format = "|{0,10}|{1,10}|{2,10}|";

        private static void WriteMessageToConsole(string message)
        {
            List<string> response = ParseResponse(message);
            Console.WriteLine(String.Format(format, DateTime.Now.ToString(), response[0], response[1]));
        }

        private static List<string> ParseResponse(string response)
        {
            return response.Split('|').ToList();
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