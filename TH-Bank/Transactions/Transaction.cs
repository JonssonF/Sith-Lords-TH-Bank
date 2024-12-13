

using System.Runtime.CompilerServices;

namespace TH_Bank
{
    public class Transaction
    {

        public decimal Amount { get; private set; }
        public Account FromAccount { get; private set; }
        public Account ToAccount { get; private set; }
        public string Id { get; private set; }

        public string Currency { get; private set; }

        public string DateAndTime { get; private set; }

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
