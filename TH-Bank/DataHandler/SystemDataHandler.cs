
namespace TH_Bank

{// This class is not related to any objects,
 // and therefor does not inherit from interfaces
    public class SystemDataHandler
    {
        public string FilePath { get; set; }
        private int _customerIDCount;
        private int _adminIDCount;
        private int _transactionIDCount;
        private int _loanIDCount;
        

        public SystemDataHandler()
        {
            FilePath = FilePaths.SystemPath;
            string[] openFile = File.ReadAllLines(FilePath);

            foreach (string line in openFile)
            {
                if (line.Contains("CustomerIDCount"))
                {
                    string[] split = line.Split('|');
                    _customerIDCount = int.Parse(split[1]);
                }
                else if (line.Contains("AdminIDCount"))
                {
                    string[] split = line.Split('|');
                    _adminIDCount = int.Parse(split[1]);
                }
                else if (line.Contains("TransactionIDCount"))
                {
                    string[] split = line.Split('|');
                    _transactionIDCount = int.Parse(split[1]);
                }
                else if (line.Contains("LoanIDCount"))
                {
                    string[] split = line.Split('|');
                    _loanIDCount = int.Parse(split[1]);
                }
            }
        }
        public int GetCustomerIDCount()
        {
            return _customerIDCount;
        }
        public int GetAdminIDCount()
        {
            return _adminIDCount;
        }

        public int GetTransactionIDCount()
        {
            return _transactionIDCount;
        }

        public void Save(string valueToChange, int saveThis)
        {
            string[] openFile = File.ReadAllLines(FilePath);

            foreach(string line in openFile)
            {
                if(line.Contains(valueToChange))
                {
                   int changeThis = Array.IndexOf(openFile, line);

                    openFile[changeThis] = $"{valueToChange}IDCount|{saveThis}";
                }
            }
            File.WriteAllLines(FilePath, openFile);
        }
    }

}