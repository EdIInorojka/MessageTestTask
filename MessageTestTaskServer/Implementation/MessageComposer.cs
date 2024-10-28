using MessageTestTaskServer.Interfaces;

namespace MessageTestTaskServer.Implementation
{
    public class MessageComposer : IMessageComposer
    {
        public string ComposeMessage(DateTime date)
        {
            List<int> numbers = ConvertDateToList(date);
            int evenCount = numbers.Count(x => x % 2 == 0);
            int oddCount = numbers.Count(y => y % 2 == 1);

            if (evenCount > oddCount) return "чет!";
            if (oddCount > evenCount) return "нечет!";
            return "равно!";
        }

        private List<int> ConvertDateToList(DateTime date)
        {
            return new List<int> { date.Day, date.Month, date.Year, date.Second, date.Minute, date.Hour };
        }
    }
}
