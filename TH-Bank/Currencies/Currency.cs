namespace TH_Bank
{
    public abstract class Currency
    {
        public abstract string Name { get; }
        public abstract string NameShort { get; }
        public Dictionary<string,double> ExchangeRates { get; private set; }

        public Currency()
        {
            ExchangeRates = new Dictionary<string, double>();
            UpdateRates();
        }

        public override string ToString()
        {
            string rates = "";
            foreach (var rate in ExchangeRates)
            {

                rates += $"{rate.Key}|{rate.Value.ToString()}\n";
            }

            return $"{NameShort}\n{Name}\n{rates}\n//End{Name}//\n";
        }

        private void UpdateRates()
        {
            var exchangeDataHandler = new ExchangeDataHandler();

            ExchangeRates = exchangeDataHandler.LoadRates(Name);

        }
    }
}
