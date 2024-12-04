

using System.Runtime.CompilerServices;

namespace TH_Bank
{
    public class Transaction
    {
        private decimal _amount;
        private int _fromAccount;
        private int _toAccount;
        private DateTime _transferDate;

        public decimal Amount { get; set; }
        public Account FromAccount { get; set; }
        public Account ToAccount { get; set; }
        public string Id { get; set; }

        public string Currency { get; set; }

        public string DateAndTime { get; set; }

        public Transaction(string id, string? dateTime, decimal amount, Account fromAccount, Account toAccount)
        {
            Amount = amount;
            FromAccount = fromAccount;
            ToAccount = toAccount; 
            Id = id;
            Currency = fromAccount.Currency;

            if(dateTime == null)
            {
                DateAndTime = DateTime.Now.ToString();
            }
            else
            {
                DateAndTime = dateTime;
            }
            
        }

        public override string ToString()
        {
            return $"{Id}|{DateAndTime}|{Amount}|{FromAccount.AccountNumber}|{ToAccount.AccountNumber}|{Currency}|{FromAccount.OwnerID}|{ToAccount.OwnerID}";
            //Add "Id" to ToString. Figure out a way to make this unique
        }
        

    }
}
