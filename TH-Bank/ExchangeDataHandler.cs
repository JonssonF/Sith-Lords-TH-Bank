
namespace TH_Bank
{
    public class ExchangeDataHandler : IDataHandler<Currency>
    {
        public string FilePath { get; set; }

        public ExchangeDataHandler()
        {
            FilePath = FilePaths.CurrencyPath;
        }

        public void Delete(Currency deleteThis)
        {
            throw new NotImplementedException();
        }

        public Currency Load()
        {
            throw new NotImplementedException();
        }

        public Currency Load(string id)
        {
            
            throw new NotImplementedException();
        }

        public Dictionary<string,double> LoadRates(string currency)
        {
            string[] openFile = File.ReadAllLines(FilePath);
            var loaded = new Dictionary<string, double>();

            // Looks for start and end of exchange rates pertaining to this currency
            int startindex = Array.FindIndex(openFile, x => x == currency) + 1;
            int endindex = Array.FindIndex(openFile, y => y == $"//END{currency}//");

            for(int i = startindex; i < endindex; i++)
            {
                string[] split = openFile[i].Split('|');
                loaded.Add(split[0], double.Parse(split[1]));
            }

            return loaded;
        }

        public List<Currency> LoadAll()
        {
            throw new NotImplementedException();
        }

        public List<Currency> LoadAll(string userid)
        {
            throw new NotImplementedException();
        }

        public void Save(Currency saveThis)
        {
            string[] openFile = File.ReadAllLines(FilePath);

            string x = Array.Find(openFile, y => y.Contains(saveThis.Name));

            if (x == null)
            {
                openFile = openFile.Append(saveThis.ToString()).ToArray();

            }
            else
            {
                int startindex = Array.IndexOf(openFile, saveThis.Name) + 1;
                int endindex = Array.IndexOf(openFile, $"//END{saveThis.Name}//");
                string[] split = saveThis.ToString().Split("\n");
                int splitcounter = Array.IndexOf(split, saveThis.Name) + 1;
                for(int i = startindex; i < endindex; i++)
                {
                    openFile[i] = split[splitcounter];
                    splitcounter++;
                }
            }

            File.WriteAllLines(FilePath, openFile);
        }

        public void SaveAll(List<Currency> saveList)
        {
            string[] saveThis = new string[0];
            
            foreach(Currency c in saveList)
            {
                saveThis = saveThis.Append(c.ToString()).ToArray();
            }
            File.WriteAllLines(FilePath, saveThis);
        }
    }
}
