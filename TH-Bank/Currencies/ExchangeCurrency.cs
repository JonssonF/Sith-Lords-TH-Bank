namespace TH_Bank
{
    public static class ExchangeCurrency
    {
        // Static containers with current available currencies,
        // and exchange rates for each currency.
        public static Currency[] currencies = { new SEK(), new USD(), new EUR() }; 
        public static Dictionary<string, Dictionary<string,double>> AllCurrentRates;
        public static Dictionary<string,double> ThisCurrencyRates { get; set; }
        public static double ConversionRate { get; set; }

       // Loads eventual changes from file.
        private static void LoadRates(ExchangeDataHandler ex)
        {
            foreach(Currency c in currencies)
            {
                AllCurrentRates[c.Name] = ex.LoadRates(c.Name);
            }
        }

        // This method is called to return converted amount
        public static decimal Exchange(decimal amount, string fromCurrency, string toCurrency)
        {
            LoadRates(new ExchangeDataHandler());
            ThisCurrencyRates = AllCurrentRates[fromCurrency];

            double rate = ThisCurrencyRates[toCurrency];
            return amount * (decimal)rate;
        }

        public static void AddCurrency(Currency c, ExchangeDataHandler ex)
        {
            currencies = currencies.Append(c).ToArray();
            ex.Save(c);
        }
    }
}
