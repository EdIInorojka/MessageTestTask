using System;
using System.Collections.Generic;
using MessageTestTaskServer.Implementation;
using Xunit;

public class MessageComposerTests
{
    private readonly MessageComposer _messageComposer;

    public MessageComposerTests()
    {
        _messageComposer = new MessageComposer();
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

    public static IEnumerable<object[]> GenerateRandomDates(int count = 100)
    {
        var random = new Random();
        var testDates = new List<object[]>();

        for (int i = 0; i < count; i++)
        {
            var year = random.Next(2000, 2030);
            var month = random.Next(1, 13);
            var day = random.Next(1, DateTime.DaysInMonth(year, month) + 1);
            var hour = random.Next(0, 24);
            var minute = random.Next(0, 60);
            var second = random.Next(0, 60);

            testDates.Add(new object[] { new DateTime(year, month, day, hour, minute, second) });
        }

        return testDates;
    }

    [Theory]
    [MemberData(nameof(GenerateRandomDates), 100)]
    public void ComposeMessage_ReturnsExpectedMessage(DateTime testDate)
    {
        // Вычисляем ожидаемое сообщение
        var expectedMessage = CalculateExpectedMessage(testDate);

        // Вызываем метод ComposeMessage с заданной датой
        var actualMessage = _messageComposer.ComposeMessage(testDate);

        // Проверяем, что полученное сообщение совпадает с ожидаемым
        Assert.Equal(expectedMessage, actualMessage);
    }
}
