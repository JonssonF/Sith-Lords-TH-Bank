namespace TH_Bank
{
    public class TransactionFactory
    {
        public Transaction CreateTransaction(decimal Amount, Account FromAccount, Account ToAccount)
        {
            var sysData = new SystemDataHandler();

            string id = $"TRA{sysData.GetTransactionIDCount().ToString("D8")}";
            sysData.Save("Transaction", sysData.GetTransactionIDCount() + 1);

            Transaction transaction = new Transaction(id, null, Amount, FromAccount, ToAccount);
            
            return transaction;
        }
    }
}
