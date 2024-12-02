using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TH_Bank
{
    public sealed class TransactionSender
    {
        private static TransactionSender _instance;
        DateTime startTime;

        // Interval for the timer.
        TimeSpan interval = TimeSpan.FromMinutes(15);

        List<Transaction> PendingTransactions;

        // Timer to execute transactions every 15 minutes.
        private System.Timers.Timer timer;



        private TransactionSender()
        {
            //Set start time to current time.
            startTime = DateTime.Now;
            PendingTransactions = new List<Transaction>();

            // New timer with the interval of 15 minutes.
            timer = new System.Timers.Timer(interval.TotalMilliseconds);
            // Timer Elapsed triggers OnTimedEvent method.
            timer.Elapsed += OnTimedEvent;
            // Automatically reset that repeats every 15 minutes.
            timer.AutoReset = true;
            //Start timer.
            timer.Start();



        }

        public void AddPendingTransaction(Transaction transaction)
        {
            var accountHandler = new AccountDataHandler(); 

            PendingTransactions.Add(transaction);
            transaction.FromAccount.Balance -= transaction.Amount;
            accountHandler.Save(transaction.FromAccount);
        }

        public void ExecuteTransactions()
        {
            var accountHandler = new AccountDataHandler();

            foreach (Transaction t in PendingTransactions)
            {
                t.ToAccount.Balance += t.Amount;
                accountHandler.Save(t.ToAccount);
            }
            PendingTransactions.Clear();
        }

        // This method is triggered every time the timer triggers.
        // Connected to timer.Elapsed.
        public void OnTimedEvent(object sender, System.Timers.ElapsedEventArgs e)
        {
            ExecuteTransactions();

        }

        public static TransactionSender GetInstance()
        {
            if (_instance == null)
            {
                _instance = new TransactionSender();
            }
            return _instance;
        }


    }
}
