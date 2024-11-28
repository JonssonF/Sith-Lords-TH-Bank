using System;

namespace TH_Bank
{
    public abstract class Account
    {
        
        private string _accountName;
        private decimal _amount;
        private decimal _currency;
        private int _accountNumber;
        private string _ownerID;

        public abstract string AccountType { get; }
        public decimal Balance { get; set; }
        public string Currency { get; set; }
        public int AccountNumber { get; set; }
        public string OwnerID { get; set; }
        public decimal Interest { get; set; }
        public List<Record> Log {get; set; }


        public Account(string ownerID, int accountNumber, decimal balance, string currency)
        {
            Log = new List<Record>();
            Balance = balance;
            Currency = currency;
            AccountNumber = accountNumber;
            OwnerID = ownerID;
        }

        public override string ToString()
        {
            return $"{OwnerID}|{AccountNumber}|{Balance}|{Currency}|{AccountType}";
        }

        public abstract decimal GetInterest();

    }
}
