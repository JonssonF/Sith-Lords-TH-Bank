using System;

namespace TH_Bank
{
    public abstract class Account
    {
        
        public abstract string AccountType { get; }
        public decimal Balance { get; set; }
        public string Currency { get; private set; }
        public int AccountNumber { get; private set; }
        public string OwnerID { get; private set; }
        public double Interest { get; private set; }


        public Account(string ownerID, decimal balance, int accountNumber, string currency)
        {
            Balance = balance;
            Currency = currency;
            AccountNumber = accountNumber;
            OwnerID = ownerID;
            var adh = new AccountDataHandler();
            Interest = adh.GetInterest(AccountType);
        }

        public override string ToString()
        {
            return $"{OwnerID}|{AccountNumber}|{Balance}|{Currency}|{AccountType}";
        }

       

    }
}
