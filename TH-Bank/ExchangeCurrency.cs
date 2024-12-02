namespace TH_Bank
{
    public class ExchangeCurrency
    {
        public string FromCurrency { get; set; }
        public string ToCurrency { get; set; }
        public double Rate { get; private set; }

        // When creating a new instance, in paramaters are Currencys short names (eg. "SEK", "USD")
        public ExchangeCurrency(string from, string to)
        {
            FromCurrency = from;
            ToCurrency = to;

        }

        private void LoadRates()
        {

        }

        // This method is called to return converted amount
        public decimal Exchange(decimal amount)
        {
            return amount * (decimal)Rate;
        }
    }
}
