

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

        public string DateAndTime { get; set; }

        public Transaction(string id, string? dateTime, decimal amount, int fromAccount, int toAccount)
        {
            Amount = amount;
            FromAccount = fromAccount;
            ToAccount = toAccount;
            Id = id;

            if(dateTime == null)
            {
                DateAndTime = DateTime.Now.ToString();
            }
            
        }


        //public bool TransferFunds() //Method for moneytransfer
        //{
        //    if (FromAccount.Balance >= Amount)
        //    {
        //        FromAccount.Balance -= Amount;
        //        ToAccount.Balance += Amount;
        //        return true;
        //    }
        //    else
        //    {
        //        Console.WriteLine("Transfer not complete. Check account balance");
        //        return false;
        //    }
        //}

        public override string ToString()
        {
            return $"{Id}|{DateAndTime}|{Amount}|{FromAccount}|{ToAccount}";
            //Add "Id" to ToString. Figure out a way to make this unique
        }
        

    }
}
