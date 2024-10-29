using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using MessageTestTaskServer;  // Пространство имен вашего сервера
using Microsoft.AspNetCore.Mvc.Testing;

public class ServerResponseTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ServerResponseTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    private string CalculateExpectedMessage(DateTime date)
    {
        var numbers = new List<int> { date.Day, date.Month, date.Year, date.Second, date.Minute, date.Hour };
        int evenCount = numbers.Count(x => x % 2 == 0);
        int oddCount = numbers.Count(x => x % 2 == 1);

        if (evenCount > oddCount) return "чет!";
        if (oddCount > evenCount) return "нечет!";
        return "равно!";
    }

    [Fact]
    public async Task ServerResponse_ShouldBeCorrect_EverySecondForOneMinute()
    {
        var startTime = DateTime.Now;
        var endTime = startTime.AddMinutes(1);

        while (DateTime.Now < endTime)
        {
            DateTime currentDate = DateTime.Now;
            string expectedMessage = CalculateExpectedMessage(currentDate);

            // Отправляем запрос на сервер
            var response = await _client.GetAsync("/Message");
            response.EnsureSuccessStatusCode();
            string actualMessage = await response.Content.ReadAsStringAsync();

            // Проверяем, что ответ совпадает с ожидаемым
            Assert.Equal(expectedMessage, actualMessage);

            // Ждем одну секунду перед следующим запросом
            await Task.Delay(1000);
        }
    }
}
