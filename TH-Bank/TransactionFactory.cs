namespace TH_Bank
{
    public class TransactionFactory
    {
        public Transaction CreateTransaction(decimal Amount, Account FromAccount, Account ToAccount, string Id)
        {
            Transaction transaction = new Transaction(Amount, FromAccount, ToAccount, Id);
            var transactionDataHandler = new TransactionDataHandler();
            transactionDataHandler.Save(transaction);
            return transaction;
        }
    }
}
