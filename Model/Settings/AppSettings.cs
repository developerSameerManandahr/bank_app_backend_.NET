namespace worksheet2.Model.Settings
{
    public class AppSettings
    {
        public string Secret { get; set; }

        //In Minutes
        public int TimeOut { get; set; }

        public string ExchangeApiUrl { get; set; }

        public string BaseCurrency { get; set; }

        public int CacheLifeInHours { get; set; }
    }
}