using MessageTestTaskServer.Interfaces;

namespace MessageTestTaskServer.Implementation
{
    // Реализация интерфейса IMessageComposer
    public class MessageComposer : IMessageComposer
    {
        // Метод для компоновки сообщения на основе переданной даты
        public string ComposeMessage(DateTime date)
        {
            // Преобразование даты в список чисел
            List<int> numbers = ConvertDateToList(date);

            // Подсчет четных чисел в списке
            int evenCount = numbers.Count(x => x % 2 == 0);
            // Подсчет нечетных чисел (всего чисел минус четные)
            int oddCount = numbers.Count() - evenCount;

            // Формирование сообщения на основе сравнений четных и нечетных чисел
            if (evenCount > oddCount) return "чет!";  // Если четных больше, вернуть "чет!"
            if (oddCount > evenCount) return "нечет!"; // Если нечетных больше, вернуть "нечет!"
            return "равно!"; // Если их количество одинаково, вернуть "равно!"
        }

        // Приватный метод для преобразования даты в список чисел
        private List<int> ConvertDateToList(DateTime date)
        {
            // Возвращает список с днем, месяцем, годом, секундой, минутой и часом
            return new List<int> { date.Day, date.Month, date.Year, date.Second, date.Minute, date.Hour };
        }
    }
}
