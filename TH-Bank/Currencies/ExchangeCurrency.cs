namespace TH_Bank
{
    public static class ExchangeCurrency
    {
        // Static containers with current available currencies,
        // and exchange rates for each currency.
        public static Currency[] currencies = { new SEK(), new USD(), new EUR() }; 
        public static Dictionary<string, Dictionary<string, double>> AllCurrentRates { get; private set; }
        public static Dictionary<string,double> ThisCurrencyRates { get; private set; }
        private static DateTime LastReview { get; set; }

       // Loads eventual changes from file.
        private static void LoadRates(ExchangeDataHandler ex)
        {
            
            AllCurrentRates = new Dictionary<string, Dictionary<string, double>>();
            foreach(Currency c in currencies)
            {
                AllCurrentRates[c.NameShort] = ex.LoadRates(c.Name);
            }

            LastReview = ex.LoadLastReview();
        }

        public static void Review(DateTime dt)
        {
            LastReview = dt;
            var ex = new ExchangeDataHandler();
            ex.SaveReviewTime(dt);
        }

        public static DateTime GetLastReview()
        {
            var ex = new ExchangeDataHandler();
            LastReview = ex.LoadLastReview();
            return LastReview;
        }

        // This method is called to return converted amount
        public static decimal Exchange(decimal amount, string fromCurrency, string toCurrency)
        {
            ThisCurrencyRates = new Dictionary<string, double>();
            LoadRates(new ExchangeDataHandler());
            ThisCurrencyRates = AllCurrentRates[fromCurrency];

            double rate = ThisCurrencyRates[toCurrency];
            return amount * (decimal)rate;
        }

        // This method adds new currencies to bank
        public static void AddCurrency(Currency c, ExchangeDataHandler ex)
        {
            currencies = currencies.Append(c).ToArray();
            ex.Save(c);
        }
    }
}
