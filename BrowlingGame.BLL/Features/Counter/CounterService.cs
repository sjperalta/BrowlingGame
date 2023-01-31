namespace BrowlingGame.BLL.Features.Counter
{
    public class CounterService: ICounterService
    {
        private int Count { get; set; }
        private Dictionary<string, int> counterDict = new Dictionary<string, int>();
        private Dictionary<string, DateTime> lastDateDict = new Dictionary<string, DateTime>();
        private const int RANGE_OF_TIME_IN_SECS = 10;
        private const int MAX_REQUEST = 2;

        public CounterService() { }

        public int Get(string routeName)
        {
            counterDict.TryGetValue(routeName, out var counter);
            return counter;
        }

        public void Update(string routeName)
        {
            if (counterDict.ContainsKey(routeName))
            {
                counterDict[routeName]++;
            }
            else
            {
                counterDict.Add(routeName, Count);
                lastDateDict.Add(routeName, DateTime.Now);
            }
        }

        public bool ShouldCache(string routeName)
        {
            var hasHalue = counterDict.TryGetValue(routeName, out var counter);
            var hasLastDate = lastDateDict.TryGetValue(routeName, out var lastDate);
            if (!hasHalue || !hasLastDate)
                return false;

            var diff = DateTime.Now - lastDate;
            if ((diff.Seconds <= RANGE_OF_TIME_IN_SECS) && (counter >= MAX_REQUEST))
                return true;

            return false;
        }

    }
}
