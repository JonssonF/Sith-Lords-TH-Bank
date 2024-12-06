
using System.ComponentModel.Design;
using System.Globalization;

namespace TH_Bank
{
    public class CustomerMenu : Menu
    {
        public CustomerMenu()
        {
            _menu = new string[] // Menu in array = easy to add options.
            {

                "1. Accounts",
                "2. New transaction",
                "3. View transactions",
                "4. Loan",
                "5. Open new account",
                "6. Logout",
                "7. Exit program",

            };
            menuWidth = CalculateWidth(extraWidth: 10);
        }



        public override void MenuChoices()
        {
            optionCount = _menu.Length; // Combined with Choice method from MenuClass wrongful inputs can't be made.
            access = true;
            while (access)
            {
                int customerChoice = Format.Choice(optionCount);
                switch (customerChoice)
                {

                    case 1:
                        ShowAccounts(ActiveUser.GetInstance(), new AccountDataHandler());
                        Console.Write("Press any key to return to menu. . .");
                        Console.ReadKey();
                        Console.Clear();
                        ShowMenu();
                        break;
                    case 2:
                        MakeTransaction(ActiveUser.GetInstance(), new AccountDataHandler(), new TransactionDataHandler());
                        break;
                    case 3:
                        ShowTransactions(ActiveUser.GetInstance(), new TransactionDataHandler());
                        break;
                    case 4:
                        LoanSection(ActiveUser.GetInstance(), new LoanDataHandler());
                        Console.Clear();
                        ShowMenu();
                        break;
                    case 5:
                        CreateNewAccount(ActiveUser.GetInstance(), new AccountFactory(), new AccountDataHandler());
                        Thread.Sleep(2500);
                        Console.Clear();
                        ShowMenu();
                        break;
                    case 6:
                        Return(); //Log out.
                        break;
                    case 7:
                        Close(); // Close application.
                        break;
                    case 8:
                        break;
                }
            }
        }
        public void ShowAccounts(User user, AccountDataHandler activeUser)
        {
            Console.Clear();
            int width = 20;
            int center = 86; // Used for dividing lines, and to align column headers.
            string text = $".:{user.UserName}'s Accounts:."; //Headline.
            int padding = (center - text.Length) / 2;
            string centeredText = new string('.', padding) + text + new string('.', padding);
            ConsoleColor textColor;
            CultureInfo currencyFormat;
            List<Account> accountList = activeUser.LoadAll(user.Id);

            if (accountList == null || accountList.Count == 0)
            {
                textColor = ConsoleColor.Red;
                Console.ForegroundColor = textColor;
                Console.WriteLine($"Username: {user.UserName} have no registered accounts at the moment.");
                Console.ResetColor();
                Thread.Sleep(2000);
                ShowMenu();
                return;
            }

            Console.WriteLine(centeredText);  // Headline for account show.
            Console.WriteLine(new string('-', center));
            Console.WriteLine(
                $"{"Nr:."}" +
                $"{CenterText(".:Account Type:.", width)}" +
                $"{CenterText(".:Account Number:.", width)}" +
                $"{CenterText(".:Balance:.", width)}" +
                $"{CenterText(".:Interest:.", width)}");
            Console.WriteLine(new string('-', center));

            int nr = 1;
            foreach (var acc in accountList)
            {
                textColor = ConsoleColor.White;
                string currentCurrency = ""; // Variable that holds balance and current Currency.

                if (acc.Currency == "SEK")
                {
                    textColor = ConsoleColor.Green;
                    currentCurrency = acc.Balance.ToString("C", new CultureInfo("sv-SE")); //Displays balance as swedish krona.
                }
                else if (acc.Currency == "USD")
                {
                    textColor = ConsoleColor.DarkMagenta;
                    currentCurrency = acc.Balance.ToString("C", new CultureInfo("en-US")); //Displays balance as US dollar.
                }
                else if (acc.Currency == "EUR")
                {
                    textColor = ConsoleColor.DarkCyan;
                    Console.OutputEncoding = System.Text.Encoding.UTF8;
                    currentCurrency = acc.Balance.ToString("C", new CultureInfo("en-IE")); //Displays balance as euro.
                }
                else
                {
                    currentCurrency = acc.Balance.ToString("C", CultureInfo.CurrentCulture);
                    textColor = ConsoleColor.White;
                }
                Console.ForegroundColor = textColor;

                Console.WriteLine(
                    $"{" "}{nr}{"."}{CenterText(acc.AccountType, width)}" + // Type of Account.
                    $"{CenterText(acc.AccountNumber.ToString(), width)}" + //Accountnumber.
                    $"{CenterText(currentCurrency, width)}" + //Formatted Currency variable, from if statement. Shows balance and currency.
                    $"{CenterText(acc.Interest.ToString("P"), width)}");
                nr++;
                Console.ResetColor();      // Lägga till Kolumn för VALUTA. // Lägg till vart pengarna ska hamna efter lån.
                Console.WriteLine(new string('-', center));
            }
        }

        public string CenterText(string text, int width) // A method to align the text in columns when showing accounts.
        {

            int padding = (width - text.Length) / 2;
            string paddedText = text.PadLeft(padding + text.Length);
            paddedText = paddedText.PadRight(width);
            return paddedText;
        }



        public void ShowTransactions(User user, TransactionDataHandler tdh)
        // Presents transactions made to or from logged in user
        {
            Console.Clear();   // Headings 
            int width = 20;
            int center = 100;
            string text = $".:{user.UserName}'s Transactions:.";
            int padding = (center - text.Length) / 2;
            string centeredText = new string('.', padding) + text + new string('.', padding);
            ConsoleColor textColor;
            List<Transaction> tList = tdh.LoadAll(user.Id);

            if (tList == null || tList.Count == 0)    // If no transactions to show
            {
                textColor = ConsoleColor.Red;
                Console.ForegroundColor = textColor;
                Console.WriteLine($"Username: {user.UserName} has no transactions to show.");
                Console.ResetColor();
                Thread.Sleep(2000);
                ShowMenu();
                return;
            }

            Console.WriteLine(centeredText);  // Headline for transaction show
            Console.WriteLine(new string('-', center));
            Console.WriteLine(
                $"{CenterText(".:Date/Time:.", width)}" +
                $"{CenterText(".:Amount:.", width)}" +
                $"{CenterText(".:Currency:.", width)}" +
                $"{CenterText(".:From Account:.", width)}" +
                $"{CenterText(".:To Account:.", width)}");
            Console.WriteLine(new string('-', center));

            tList.Reverse();     // Reverse the transaction list to show newest transactions at top

            foreach (var t in tList)
            {
                textColor = ConsoleColor.Gray;        // Transactions between own accounts are Gray
                string signedAmount = t.Amount.ToString("0.00");

                if (t.FromAccount.OwnerID != user.Id && t.ToAccount.OwnerID == user.Id)
                {
                    textColor = ConsoleColor.Green;   // Transactions from other users are Green with + 
                    signedAmount = $"+{signedAmount}";
                }
                else if (t.FromAccount.OwnerID == user.Id && t.ToAccount.OwnerID != user.Id)
                {
                    textColor = ConsoleColor.Red;     // Transactions to other users are Red with - 
                    signedAmount = $"-{signedAmount}";
                }
                Console.ForegroundColor = textColor;

                Console.WriteLine(
                    $"{CenterText(t.DateAndTime, width)}" +
                    $"{CenterText(signedAmount, width)}" +
                    $"{CenterText(t.Currency, width)}" +
                    $"{CenterText(t.FromAccount.AccountNumber.ToString(), width)}" +
                    $"{CenterText(t.ToAccount.AccountNumber.ToString(), width)}");
                Console.ResetColor();
            }
            Console.Write("\nPress any key to return to menu. . .");
            Console.ReadKey();
            Console.Clear();
            ShowMenu();
        }

        public void MakeTransaction(User user, AccountDataHandler adh, TransactionDataHandler tdh)
        // Method for making a transaction which results in a new Transaction object
        {
            ShowAccounts(user, adh);
            Console.WriteLine("\n[1] - Transaction between own accounts" +
                              "\n[2] - Transaction to external account" +
                              "\n[3] - Return to menu");
            int transChoice = Format.Choice(3);
            Console.Clear();
            ShowAccounts(user, adh);

            List<Account> userAccounts = adh.LoadAll(user.Id);
            List<Account> allAccounts = adh.LoadAll();
            optionCount = userAccounts.Count;
            Account? fromAccount = null;
            Account? toAccount = null;
            bool validToAccount = false;

            switch (transChoice)
            {
                case 1:
                    Console.WriteLine("\n[1] - Transaction between own accounts\n");
                    int accChoiceFrom = ValidOwnAccount("from");  // Method for choosing valid own accounts
                    int accChoiceTo = ValidOwnAccount("to");      // based on number of accounts for user
                    if (accChoiceTo == accChoiceFrom)
                    {
                        Console.WriteLine("\nTo and from account are the same. Transaction aborted." +
                            "\nPress any key to return to menu. . .");
                        Console.ReadKey();
                        ShowMenu();
                    }
                    else
                    {
                        fromAccount = userAccounts[accChoiceFrom - 1];
                        toAccount = userAccounts[accChoiceTo - 1];
                        if (fromAccount.Balance <= 0)
                        {
                            Console.WriteLine("The account is empty. Transaction aborted." +
                                "\nPress any key to return to menu. . .");
                            Console.ReadKey();
                            Console.Clear();
                            ShowMenu();
                        }
                    }
                    break;
                case 2:
                    Console.WriteLine("\n[2] Transaction to external account\n");
                    int accChoice = ValidOwnAccount("from");
                    fromAccount = userAccounts[accChoice - 1];
                    Console.WriteLine("Enter recieving account number: ");
                    int toAccountInt = Format.IntegerInput(6);

                    foreach (Account account in allAccounts)
                    {
                        if (account.AccountNumber == toAccountInt)   // If valid reciever account - proceed
                        {
                            toAccount = account;
                            validToAccount = true;
                            string ownerID = toAccount.OwnerID;
                            UserDataHandler udh = new UserDataHandler();
                            Customer owner = (Customer)udh.Load(ownerID);
                            Console.WriteLine($"Reciever: [{owner.FirstName} {owner.LastName}]");
                        }
                    }
                    if (validToAccount == false)    // If not valid reciever account - abort
                    {
                        Console.WriteLine("\nInvalid account number. Transaction aborted." +
                            "\nPress any key to return to menu. . .");
                        Console.ReadKey();
                        ShowMenu();
                    }
                    break;
                case 3:
                    Console.Clear();
                    ShowMenu();
                    break;
            }

            decimal amount = 0;
            do
            {
                Console.WriteLine("Enter amount to transfer: ");
                amount = Format.DecimalInput();
                if (amount > fromAccount.Balance)
                {
                    Console.WriteLine("Invalid amount - check your balance");
                }
            }
            while (amount > fromAccount.Balance);

            Console.Clear();
            ShowAccounts(user, adh);
            Console.WriteLine($"\n[{amount.ToString("0.00")} {fromAccount.Currency}] will be tranferred from account " +
                $"[{fromAccount.AccountNumber}] to account [{toAccount.AccountNumber}]" +
                $"\nDo you wish to continue?\n\n[1] - Yes\n[2] - No");   // Possibility for user to abort transaction

            int proceed = Format.Choice(2);
            if (proceed == 1)
            {
                Console.ForegroundColor = ConsoleColor.Green;    // Fictive processing...
                string Proccess = "Processing...";
                string rounds = "1";
                foreach (char c in rounds)
                {
                    Console.Clear();
                    foreach (char d in Proccess)
                    {
                        Console.Write(d);
                        Thread.Sleep(35);
                    }
                }
                Console.ResetColor();                                   // New transaction object created
                Transaction transaction = new TransactionFactory().CreateTransaction(amount, fromAccount, toAccount);
                TransactionSender transactionSender = TransactionSender.GetInstance();
                transactionSender.AddPendingTransaction(transaction);    // Transaction from account is updated immediately
                                                                         // Transaction to account is added to Pending Transactions
                tdh.Save(transaction);                                   // and executed at specific intervals.
                Console.Clear();
                ShowAccounts(user, adh);
                Console.WriteLine($"\nTransaction successful!\nRecieving account will be updated shortly.");
            }
            else if (proceed == 2)
            {
                Console.WriteLine("\nTransaction aborted.");
            }
            Console.Write("Press any key to return to menu. . .");
            Console.ReadKey();
            Console.Clear();
            ShowMenu();
        }

        public int ValidOwnAccount(string toOrFrom)  // Method for valid user input regarding own accounts
        {
            int ownAccount = 0;
            do
            {
                Console.WriteLine($"Enter account to transfer {toOrFrom} (1 - {optionCount}): ");

                if (optionCount <= 9)
                {
                    ownAccount = Format.Choice(optionCount);   //Format.Choice is possible for accounts < 10
                    Console.WriteLine(ownAccount);
                }
                else if (optionCount >= 10)
                {
                    ownAccount = Format.IntegerInput(2);      //Format.IntegerInput is possible for accounts > 9
                }
            }
            while (ownAccount > optionCount);
            return ownAccount;
        }

        public void CreateNewAccount(User user, AccountFactory accountFactory, AccountDataHandler activeUser)
        {
            Console.Clear();
            Console.WriteLine(new string('-', 80));
            Console.WriteLine(new string('-', 80));

            List<Account> accountList = activeUser.LoadAll(user.UserName);
            decimal balance = 0;

            AccountFactory acc1 = new AccountFactory();
            Console.WriteLine("In which currency would you like your account to be denominated?\n");
            Console.WriteLine("[1] SEK - Swedish kronor. ");
            Console.WriteLine("[2] USD - US Dollar. ");
            Console.WriteLine("[3] EUR - EU Euro. ");
            Console.Write("Enter currency: ");
            int currencyChoice = Format.Choice(3);
            Console.WriteLine(currencyChoice);
            string currency = "";
            switch (currencyChoice)
            {
                case 1:
                    currency = "SEK";
                    break;
                case 2:
                    currency = "USD";
                    break;
                case 3:
                    currency = "EUR";
                    break;
                default:
                    Console.WriteLine("Please enter 1 or 3.");
                    break;
            }
            Console.WriteLine(" ");
            Console.WriteLine(new string('-', 80));
            Console.WriteLine("What type of bank account would you like to open?\n");
            Console.WriteLine("[1] - Salaryaccount ");
            Console.WriteLine("[2] - Savingsaccount ");
            Console.Write("\nEnter account type: ");
            int accountChoice = Format.Choice(2);
            Console.WriteLine(accountChoice);
            string userchoice = "";

            switch (accountChoice)
            {
                case 1:
                    userchoice = "SalaryAccount";
                    break;
                case 2:
                    userchoice = "SavingsAccount";
                    break;
                default:
                    Console.WriteLine("Please enter 1 or 2.");
                    break;
            }
            Console.WriteLine(" ");
            Console.WriteLine(new string('-', 80));
            Console.WriteLine(new string('-', 80));
            Console.WriteLine($"You have succesfully created a new account.\n" +
                $"[Account type: {userchoice} with currency: {currency}.]");
            Account account = accountFactory.CreateAccount(user.Id, balance, currency, userchoice);
            Console.WriteLine("\nPress any key to return to menu. . .");
            Console.ReadKey();
            Console.Clear();
            ShowMenu();
        }
        public void LoanSection(User user, LoanDataHandler loanUser)
        {
            /*------------Variables and Objects used throughout Loan Process--------*/
            int width = 20;
            int center = 80; // Used for dividing lines, and to align column headers.
            string text = $".:{user.UserName}'s loan section:."; //Headline. //Headline.
            int padding = (center - text.Length) / 2;
            string centeredText = new string('.', padding) + text + new string('.', padding);
            double interest = 0;
            string id = user.Id;
            decimal amount = 0;
            string currentLoan = "";
            var ldhD = new LoanDataHandler();
            double displayCar = ldhD.GetInterest("CarLoan");
            double displayMortg = ldhD.GetInterest("MortgageLoan");
            DateTime loanTimeStamp = DateTime.Now;
            DateTime LastPay = DateTime.Now;
            ConsoleColor textColor;
            List<Loan> allLoans = loanUser.LoadAll(user.Id);
            /*----------------------------------------------------------------------*/
            Console.Clear();
            LoanLogo();
            Console.WriteLine($"Welcome to TH-Bank's loan section {user.UserName}.");
            Console.WriteLine($"\nChoose a number to proceed.");
            Console.WriteLine($"[1] Apply for a new loan.");
            Console.WriteLine($"[2] Display previously taken loans.");
            Console.WriteLine($"[3] Review current loan interest rates.");
            Console.WriteLine($"[4] Return to main menu.\n");
            Console.Write("Choose option:");
            int userChoice = Format.Choice(4);
            switch (userChoice)
            {
                case 1:

                    ApplyForLoan(ActiveUser.GetInstance(), new LoanDataHandler(), new LoanFactory(), new AccountDataHandler());
                    break;
                case 2:
                    ShowLoans(ActiveUser.GetInstance(), new LoanDataHandler());
                    break;
                case 3:
                    ShowLoanRates();
                    break;
                case 4:
                    return;
                default:
                    Console.WriteLine("Please choose a valid option.");
                    break;
            }
            ShowMenu();


            void ShowLoanRates()
            {
                Console.Clear();
                var ldhD = new LoanDataHandler();
                double displayCar = ldhD.GetInterest("CarLoan");
                double displayMortg = ldhD.GetInterest("MortgageLoan");
                LoanLogo();
                Console.WriteLine($"::Type: Car - Loan".PadRight(30) + $"[Interest Rate: {displayCar.ToString("P")}]::..");
                Console.WriteLine(new string('-', 80));
                Console.WriteLine($"::Type: House - Loan".PadRight(30) + $"[Interest Rate: {displayMortg.ToString("P")}]::..");
                Console.WriteLine(new string('-', 80));
                Console.Write("Press any key to return to menu. . .");
                Console.ReadKey();
                LoanSection(ActiveUser.GetInstance(), new LoanDataHandler());

            }
            void ShowLoans(User user, LoanDataHandler loanUser)
            {
                Console.Clear();
                Console.WriteLine(new string('-', center));
                Console.WriteLine(new string('-', center));
                Console.WriteLine(centeredText);  // Headline for User.
                Console.WriteLine(new string('-', center));
                Console.WriteLine(
                    $"{"Nr:."}" +
                    $"{CenterText(".:Loan Type:.", width)}" +
                    $"{CenterText(".:Amount:.", width)}" +
                    $"{CenterText(".:Interest:.", width)}" +
                    $"{CenterText(".:Approved:.", width)}" +
                    $"{CenterText(".:Expire:.", width)}");
                Console.WriteLine(new string('-', center));

                if (allLoans.Count == 0)
                {
                    Console.WriteLine($"{user.UserName} currently has no loans with us. ");
                }
                else
                {
                    int nr = 1;
                    foreach (var loan in allLoans)
                    {
                        if (loan.LoanType == "Carloan")
                        {
                            textColor = ConsoleColor.Yellow;
                        }
                        else if (loan.LoanType == "Mortgage")
                        {
                            textColor = ConsoleColor.Red;
                        }
                        else
                        {
                            textColor = ConsoleColor.White;
                        }
                        Console.ForegroundColor = textColor;
                        Console.WriteLine(
                        $"{" "}{nr}{"."}{CenterText(loan.LoanType, width)}" +
                        $"{CenterText(loan.Amount.ToString("C"), width)}" +
                        $"{CenterText(loan.Interest.ToString("P"), width)}" +
                        $"{CenterText(loan.LoanStart, width)}" +
                        $"{CenterText(loan.LoanExp, width)}");
                        nr++;
                        Console.ResetColor();
                        Console.WriteLine(new string('-', center));
                    }
                }
                Console.Write("Press any key to go back. . .");
                Console.ReadKey();
                LoanSection(ActiveUser.GetInstance(), new LoanDataHandler());
            }


            void ApplyForLoan(User user, LoanDataHandler loanData, LoanFactory loanFactory, AccountDataHandler activeUser)
            {
                Console.Clear();
                LoanLogo();
                user.LoanLimit = user.SetMaxLoan();
                Console.WriteLine($"::[ Loan :******* ]::..::[ Max Amount: ******* ]::..::[ Interest: ******* ]::..");
                Console.WriteLine("--------------------------------------------------------------------------------");
                if(user.LoanLimit <= 0)
                {
                    Console.WriteLine("Currently you are not eligiable for a loan at TH-Bank.");
                    Thread.Sleep(1500);
                    Console.WriteLine("Redirecting you to main menu. . .");
                    Thread.Sleep(1500);
                    ShowMenu();
                }
                Console.WriteLine($"Wich type of loan would you like to apply for {user.UserName}?\n");
                Console.WriteLine("[1] Car - Loan.");
                Console.WriteLine("[2] House - Loan.");
                Console.WriteLine("[3] Return to Loan menu.\n");
                Console.Write("Choose option:");
                int userChoice = Format.Choice(3);

                switch (userChoice)
                {
                    case 1:
                        currentLoan = "Car - Loan";
                        IntroLoan();
                        return;
                    case 2:
                        currentLoan = "House - Loan";
                        IntroLoan();
                        return;
                    case 3:
                        LoanSection(ActiveUser.GetInstance(), new LoanDataHandler()); // Returns to loan section.
                        return;
                    default:
                        throw new Exception("Invalid menu choice");
                }
            }

            void IntroLoan()
            {
                Console.Clear();

                SetInterest(ref interest);
                LoanLogo();
                Console.WriteLine($"::[ {currentLoan} ]::..::[ Max Amount: {user.LoanLimit.ToString("C")} ]::..::[ Interest: {interest.ToString("P")} ]::.");
                Console.WriteLine(new string('-', 80));
                Console.WriteLine($"If you wish to apply for a {currentLoan},\n" +
                    $"the maximum loan amount is {user.LoanLimit.ToString("C")},\n" +
                    $"and the interest rate we can offer is {interest.ToString("P")}.");
                Console.WriteLine(new string('-', 80));
                Console.WriteLine("Would you like to continue?");
                Console.WriteLine("[1] - Yes.");
                Console.WriteLine("[2] - No.\n");
                Console.Write("Choose option:");
                int userChoice = Format.Choice(2);
                switch (userChoice)
                {
                    case 1:
                        Credibility();
                        break;
                    case 2:
                        LoanSection(ActiveUser.GetInstance(), new LoanDataHandler());
                        break;
                    default: throw new Exception("Invalid menu choice");
                }
                return;
            }

            void Credibility()
            {
                Console.Clear();
                LoanLogo();
                Console.WriteLine($"::[ {currentLoan} ]::..::[ Max Amount: {user.LoanLimit.ToString("C")} ]::..::[ Interest: {interest.ToString("P")} ]::.");
                Console.WriteLine(new string('-', 80));
                Console.Write("How much would you like to loan: ");
                decimal amount = Format.DecimalInput();
                if (amount > user.LoanLimit)
                {
                    Process(amount);
                    LoanLogo();
                    Console.WriteLine($"::[ {currentLoan} ]::..::[ Max Amount: {user.LoanLimit.ToString("C")} ]::..::[ Interest: {interest.ToString("P")} ]::.");
                    Console.WriteLine(new string('-', 80));
                    Console.WriteLine($"\nYour current balance is insufficient to meet the requirements for a loan.\n");
                    Console.Write("Press any key to try again. . .");
                    Console.ReadKey();
                    amount = 0;
                    Console.Clear();
                    IntroLoan();
                }
                else if (amount <= user.LoanLimit)
                {
                    Process(amount);
                    Repayment(amount);
                }
            }

            void Repayment(decimal amount)
            {

                LoanLogo();
                Console.WriteLine($"::[ {currentLoan} ]::..::[ Amount: {amount.ToString("C")} ]::..::[ Interest: {interest.ToString("P")} ]::.");
                Console.WriteLine(new string('-', 80));
                Console.Write($"We can offer you a {currentLoan} for the desired amount of: {amount.ToString("C")}.\n\n" +
                    $"What repayment term would you prefer?");

                Console.WriteLine("\n[1] 6 Months.");
                Console.WriteLine("[2] 12 Months.");
                Console.WriteLine("[3] 18 Months.");
                Console.WriteLine("\n[4] Cancel loan negotiations.\n");
                Console.Write("Choose term:");

                int userTime = Format.Choice(4);
                decimal intCal = 0;
                switch (userTime)
                {
                    case 1:
                        userTime = 6;
                        LastPay = LastPay.AddMonths(6);
                        intCal = interestCalc(amount, interest, 0.5);
                        PresentLoan(userTime, intCal, amount);
                        break;
                    case 2:
                        userTime = 12;
                        LastPay = LastPay.AddMonths(12);
                        intCal = interestCalc(amount, interest, 1);
                        PresentLoan(userTime, intCal, amount);
                        break;
                    case 3:
                        userTime = 18;
                        LastPay = LastPay.AddMonths(18);
                        intCal = interestCalc(amount, interest, 1.5);
                        PresentLoan(userTime, intCal, amount);
                        break;
                    case 4:
                        LoanSection(ActiveUser.GetInstance(), new LoanDataHandler());
                        break;
                    default:
                        Console.WriteLine("Unvalid option. Loan denied.");
                        Thread.Sleep(1500);
                        break;
                }
            }

            void PresentLoan(int userTime, decimal intCal, decimal amount)
            {
                Console.Clear();
                ConsoleColor C;
                if (userTime == 6)
                { C = ConsoleColor.Green; }
                else if (userTime == 12)
                { C = ConsoleColor.DarkYellow; }
                else
                { C = ConsoleColor.Red; }
                LoanLogo();
                Console.WriteLine($"::[ {currentLoan} ]::..::[ Amount: {amount.ToString("C")} ]::..::[ Interest: {interest.ToString("P")} ]::.");
                Console.WriteLine(new string('-', 80));
                Console.ForegroundColor = C;
                Console.Write($"\n[{userTime}]");
                Console.Write(" Months");
                Console.ResetColor();
                Console.Write(". Last payment on ");
                Console.ForegroundColor = C;
                Console.Write($"[{LastPay.ToString("yyy-MM-dd")}]");
                Console.ResetColor();
                Console.Write($" Total interest payment");
                Console.ForegroundColor = C;
                Console.Write($" [{intCal.ToString("C")}]\n");
                Console.ResetColor();
                Console.WriteLine(new string('-', 80));
                Console.WriteLine("Would you like to accept theese terms?");
                Console.WriteLine("[1] Yes.");
                Console.WriteLine("[2] Change term.");
                Console.WriteLine("[3] Cancel loan negotiations.");
                int choice = Format.Choice(3);
                switch (choice)
                {
                    case 1:
                        SaveLoan(ActiveUser.GetInstance(), new LoanDataHandler(), new LoanFactory(), new AccountDataHandler(), intCal, amount);
                        break;
                    case 2:
                        Console.Clear();
                        Repayment(amount);
                        break;
                    case 3:
                        Console.Clear();
                        ShowMenu();
                        break;
                }
            }

            void SaveLoan(User user, LoanDataHandler loanData, LoanFactory loanFactory, AccountDataHandler activeUser, decimal intCal, decimal amount)
            {
                string displayLoan = currentLoan;
                if (currentLoan == "Car - Loan")
                {
                    currentLoan = "CarLoan";
                }
                else if (currentLoan == "House - Loan")
                {
                    currentLoan = "MortgageLoan";
                }
                else
                {
                    throw new Exception("Something went wrong, sorry.");
                }
                Console.Clear();
                LoanLogo();
                Console.WriteLine(new string('-', 80));
                Console.WriteLine($"Congratulations {user.UserName} on your new loan.\n\n" +
                    $"::Loan:[ {displayLoan} ]\n" +
                    $"::Amount: [ {amount.ToString("C")}]\n" +
                    $"::Interest rate: [ {interest.ToString("P")} ]" +
                    $"::Total interest: [ {intCal.ToString("C")}]\n" +
                    $"::Approved: [ {loanTimeStamp.ToString("yyy-MM-dd")} ]" +
                    $"::Expires: [ {LastPay.ToString("yyy-MM-dd")} ]");
                Console.WriteLine(new string('-', 80));
                ShowAccounts(ActiveUser.GetInstance(), new AccountDataHandler());

                Console.WriteLine("Wich account would you like the loan to be deposited into?");
                List<Account> accounts = activeUser.LoadAll(user.Id);
                int accountCount = 0;
                foreach (var acc in accounts)
                {
                    accountCount++;
                }
                int accChoice = Format.Choice(accountCount);
                if (accChoice >= 1 && accChoice <= accountCount)
                {
                    Account selectedAccount = accounts[accChoice - 1];
                    Console.WriteLine(new string('-', 80));
                    Console.WriteLine($"::Approved loan amount of {amount.ToString("C")} is being transfered to, \n" +
                        $"::Account: {selectedAccount.AccountType} ::Accountnumber: {selectedAccount.AccountNumber}.");
                    selectedAccount.Balance += amount;
                    activeUser.Save(selectedAccount);

                    string expire = LastPay.ToString("yyy-MM-dd");

                    loanData.Save(loanFactory.NewLoan(id, amount, expire, currentLoan));

                }
                else
                {
                    throw new NotImplementedException("Ooops.");
                }
                Console.Write("\nPress any key to go to main menu. . .");
                Console.ReadKey();
                ShowMenu();
            }

            double SetInterest(ref double interest)
            {
                var ldh = new LoanDataHandler();

                if (currentLoan == "Car - Loan")
                {
                    interest = ldh.GetInterest("CarLoan");
                }
                else if (currentLoan == "House - Loan")
                {
                    interest = ldh.GetInterest("MortgageLoan");
                }
                return interest;
            }

            decimal interestCalc(decimal amount, double interest, double time)
            {
                decimal timeDec = (decimal)time; // Convert double to decimal so i can calculate interest.
                decimal interestDec = (decimal)interest;

                return amount * interestDec * timeDec;
            }

            void Process(decimal amount)
            {
                string Proccess = "Checking credibility...";
                string rounds = "1";
                foreach (char c in rounds)
                {
                    Console.Clear();
                    LoanLogo();
                    foreach (char d in Proccess)
                    {
                        Console.Write(d);
                        Thread.Sleep(35);
                    }
                    Console.Clear();
                    LoanLogo();
                    if (amount <= user.LoanLimit)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Loan granted...");
                        Thread.Sleep(1500);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Loan denied...");
                        Thread.Sleep(1500);
                    }
                    Console.ResetColor();
                    Console.Clear();
                }
            }

            void LoanLogo()
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(" ________  __    __          __        ______    ______   __    __ \r\n|        \\|  \\  |  \\        |  \\      /      \\  /      \\ |  \\  |  \\\r\n \\$$$$$$$$| $$  | $$        | $$     |  $$$$$$\\|  $$$$$$\\| $$\\ | $$\r\n   | $$   | $$__| $$ ______ | $$     | $$  | $$| $$__| $$| $$$\\| $$\r\n   | $$   | $$    $$|      \\| $$     | $$  | $$| $$    $$| $$$$\\ $$\r\n   | $$   | $$$$$$$$ \\$$$$$$| $$     | $$  | $$| $$$$$$$$| $$\\$$ $$\r\n   | $$   | $$  | $$        | $$_____| $$__/ $$| $$  | $$| $$ \\$$$$\r\n   | $$   | $$  | $$        | $$     \\\\$$    $$| $$  | $$| $$  \\$$$\r\n    \\$$    \\$$   \\$$         \\$$$$$$$$ \\$$$$$$  \\$$   \\$$ \\$$   \\$$\r\n");
                Console.ResetColor();
                Console.WriteLine(new string('-', 80));
                Console.WriteLine(centeredText);
                Console.WriteLine(new string('-', 80));
            }
        }
    }
}

