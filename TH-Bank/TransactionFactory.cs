namespace TH_Bank
{
    public abstract class TransactionFactory
    {
        public abstract Transaction DoTransaction(TransactionDataHandler transactionDataHandler);
        public Transaction CreateTransaction(Transaction trans)
        {
            Transaction transaction = new Transaction(trans.Amount, trans.FromAccount, trans.ToAccount, trans.Id);
            var transactionDataHandler = new TransactionDataHandler();
            transactionDataHandler.Save(transaction);
            return this.DoTransaction(transactionDataHandler);
        }
    }
}
