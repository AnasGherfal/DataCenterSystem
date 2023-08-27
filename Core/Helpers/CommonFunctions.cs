namespace Core.Helpers
{
    public class CommonFunctions
    {
        public static TimeSpan? ObjectToTimeSpan(object value)
        {
            var stringValue = value is null ? string.Empty : value.ToString();

            if (string.IsNullOrWhiteSpace(stringValue)) return null;

            var timespanArray = stringValue.Split(':', 3);
            var hours = Convert.ToInt32(timespanArray[0]);
            var mins = Convert.ToInt32(timespanArray[1]);
            var seconds = Convert.ToInt32(timespanArray[2]);

            return new TimeSpan(hours, mins, seconds);
        }

        public static string RandomString()
        {
            Random random = new();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 8)
              .Select(s => s[random.Next(8)]).ToArray());
        }

        public static int GetTotalPages(int totalCount) => (int)Math.Ceiling(totalCount / 10.00);

        public static string CreateSerialNo(string? lastSerialNo)
        {
            int currentYear = DateTime.Today.Year;

            //Check if empty (First Card)
            if (string.IsNullOrEmpty(lastSerialNo)) return $"{currentYear}-0001";

            //Check last card year
            int lastSerialYear = Int32.Parse(lastSerialNo.Split('-')[0]);
            if (lastSerialYear != currentYear) return $"{currentYear}-0001";

            //Generate Next Counter
            int lastSerialCounter = Int32.Parse(lastSerialNo.Split('-')[1]);
            var newSerialNo = (lastSerialCounter + 1).ToString().PadLeft(4, '0');

            return $"{currentYear}-{newSerialNo}";
        }
    }
}