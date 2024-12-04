
using System.Globalization;

namespace TH_Bank
{
    public class CustomerMenu : Menu
    {
        public CustomerMenu()
        {
            _menu = new string[] // Menu in array = easy to add options.
            {
                "1. Accounts.",
                "2. Show transactions.",
                "3. Perform transaction.",
                "4. Loan.",
                "5. Open new account.",
                "6. Logout.",
                "7. Exit program.",
            };
            menuWidth = CalculateWidth(extraWidth: 10);
        }


        public override void ShowMenu()
        {
            Console.Clear();
            LogoText();
            DrawBorder();
            foreach (string item in _menu)
            {
                DrawMenuItem(item);
            }
            DrawBorder();
            MenuCustomer();
        }


        public void MenuCustomer()
        {
            optionCount = _menu.Length; // Combined with Choice method from MenuClass wrongful inputs can't be made.
            access = true;
            while (access)
            {
                int customerChoice = Format.Choice(optionCount);
                switch (customerChoice)
                {

                    case 1:
                        ShowAccounts(ActiveUserSingleton.GetInstance(), new AccountDataHandler());
                        Console.Write("Press any key to go back. . .");
                        Console.ReadLine();
                        Console.Clear();
                        ShowMenu();
                        break;
                    case 2:
                        //Show transactions.
                        break;
                    case 3:
                        MakeTransaction(ActiveUserSingleton.GetInstance(), new AccountDataHandler());
                        break;

                    case 4:
                        LoanSection(ActiveUserSingleton.GetInstance(), new LoanDataHandler());
                        
                        Console.Clear();
                        ShowMenu();
                        break;
                    case 5:
                        CreateNewAccount(ActiveUserSingleton.GetInstance(), new AccountFactory(), new AccountDataHandler());
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
        public override void ShowAccounts(User user, AccountDataHandler activeUser)
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

        public void MakeTransaction(User user, AccountDataHandler adh)
        {
            ShowAccounts(user, adh);
            Console.WriteLine("\n[1] Transaction between own accounts");
            Console.WriteLine("[2] Transaction to external account");
            int transChoice = Format.Choice(2);
            Console.Clear();
            ShowAccounts(user, adh);

            List<Account> userAccounts = adh.LoadAll(user.Id);
            List<Account> allAccounts = adh.LoadAll();
            Account[] accountArray = userAccounts.ToArray();
            optionCount = userAccounts.Count;
            Account fromAccount = null;
            Account toAccount = null;
            bool validToAccount = false;

            switch (transChoice)
            {
                case 1:
                    Console.WriteLine("\n[1] Transaction between own accounts");
                    int accChoiceFrom = ValidOwnAccount("from");
                    int accChoiceTo = ValidOwnAccount("to");
                    if (accChoiceTo == accChoiceFrom)
                    {
                        Console.WriteLine("To and from account are the same. Transaction aborted." +
                            "\nPress any key to return to menu.");
                        Console.ReadKey();
                        ShowMenu();
                    }
                    else
                    {
                        fromAccount = accountArray[accChoiceFrom - 1];
                        toAccount = accountArray[accChoiceTo - 1];
                    }
                    break;
                case 2:
                    Console.WriteLine("\n[2] Transaction to external account");
                    int accChoice = ValidOwnAccount("from");
                    fromAccount = accountArray[accChoice - 1];
                    Console.WriteLine("Enter recieving account number: ");
                    int toAccountInt = Format.IntegerInput(6);

                    foreach (Account account in allAccounts)
                    {
                        if (account.AccountNumber == toAccountInt)
                        {
                            toAccount = account;   //Kolla att denna kodrad är ok när transaktionen väl funkar
                            validToAccount = true;
                            string ownerID = toAccount.OwnerID;
                            UserDataHandler udh = new UserDataHandler();
                            Customer owner = (Customer)udh.Load(ownerID);
                            Console.WriteLine($"Reciever: {owner.FirstName} {owner.LastName}");
                        }
                    }
                    if (validToAccount == false)
                    {
                        Console.WriteLine("Invalid account number. Transaction aborted." +
                            "\nPress any key to return to menu.");
                        Console.ReadKey();
                        ShowMenu();
                    }
                    break;
            }
            Console.WriteLine("Enter amount to transfer: ");
            decimal amount = Format.DecimalInput();
            if (amount > fromAccount.Balance)
            {
                Console.WriteLine("Transaction not possible. Check your balance.\nPress any key to return to menu.");
                Console.ReadLine();
                Console.Clear();
                ShowMenu();
            }
            else
            {
                Console.WriteLine($"{amount} {fromAccount.Currency} will be tranferred from account {fromAccount.AccountNumber} to account {toAccount.AccountNumber}." +
                $"\nDo you wish to continue? \n1. Yes\n2. No");
                int proceed = Format.Choice(2);
                if (proceed == 1)
                {
                    Transaction transaction = new TransactionFactory().CreateTransaction(amount, fromAccount, toAccount);
                    TransactionSender transactionSender = TransactionSender.GetInstance();
                    transactionSender.AddPendingTransaction(transaction);
                    var tdh = new TransactionDataHandler();
                    tdh.Save(transaction);
                    Console.Clear();
                    ShowAccounts(user, adh);
                    Console.WriteLine("\nTransaction complete.");
                }
                else if (proceed == 2)
                {
                    Console.WriteLine("Transaction aborted.");
                }
                Console.Write("Press any key to return to menu.");
                Console.ReadLine();
                Console.Clear();
                ShowMenu();
            }


        }

        public int ValidOwnAccount(string toOrFrom)
        {
            int ownAccount = 0;
            do
            {
                Console.WriteLine($"Enter account to transfer {toOrFrom} (1 - {optionCount}): ");

                if (optionCount <= 9)
                {
                    ownAccount = Format.Choice(optionCount);
                    Console.WriteLine(ownAccount);
                }
                else if (optionCount >= 10)
                {
                    ownAccount = Format.IntegerInput(2);
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
            Console.WriteLine("[1] Salaryaccount ");
            Console.WriteLine("[2] Savingsaccount ");
            Console.Write("\nEnter account type: ");
            int accountChoice = Format.Choice(2);
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
            Console.WriteLine("\nPress any key to continue to main menu. . .");
            Console.ReadLine();
            ShowMenu();
        }
        public void LoanSection(User user, LoanDataHandler loanUser)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" ________  __    __          __        ______    ______   __    __ \r\n|        \\|  \\  |  \\        |  \\      /      \\  /      \\ |  \\  |  \\\r\n \\$$$$$$$$| $$  | $$        | $$     |  $$$$$$\\|  $$$$$$\\| $$\\ | $$\r\n   | $$   | $$__| $$ ______ | $$     | $$  | $$| $$__| $$| $$$\\| $$\r\n   | $$   | $$    $$|      \\| $$     | $$  | $$| $$    $$| $$$$\\ $$\r\n   | $$   | $$$$$$$$ \\$$$$$$| $$     | $$  | $$| $$$$$$$$| $$\\$$ $$\r\n   | $$   | $$  | $$        | $$_____| $$__/ $$| $$  | $$| $$ \\$$$$\r\n   | $$   | $$  | $$        | $$     \\\\$$    $$| $$  | $$| $$  \\$$$\r\n    \\$$    \\$$   \\$$         \\$$$$$$$$ \\$$$$$$  \\$$   \\$$ \\$$   \\$$\r\n");
            Console.ResetColor();
            Console.WriteLine(new string('-', 70));
            Console.WriteLine(new string('-', 70));
            Console.WriteLine($"Welcome to TH-Bank's loan section {user.UserName}.");
            Console.WriteLine($"\nChoose a number to proceed.");
            Console.WriteLine($"[1] Apply for new loans.");
            Console.WriteLine($"[2] Display previously taken loans.");
            Console.WriteLine($"[3] Review current loan interest rates.");
            Console.WriteLine($"[4] Return to main menu.");
            int userChoice = Format.Choice(4);
            switch (userChoice)
            {

                case 1:
                    ApplyForLoan(ActiveUserSingleton.GetInstance(), new LoanDataHandler(), new LoanFactory(), new AccountDataHandler());
                    break;
                case 2:
                    ShowLoans(ActiveUserSingleton.GetInstance(), new LoanDataHandler());
                    break;
                case 3:
                    break;
                case 4:
                    return;
                default:
                    Console.WriteLine("Please choose a valid option.");
                    break;
            }

            Console.WriteLine();



            Console.ReadLine();
            ShowMenu();
        }
        public void ShowLoans(User user, LoanDataHandler loanUser)
        {
            Console.Clear();
            ConsoleColor textColor;
            int width = 20;
            int center = 86; // Used for dividing lines, and to align column headers.
            string text = $".:{user.UserName}'s Loans:."; //Headline.
            int padding = (center - text.Length) / 2;
            string centeredText = new string('.', padding) + text + new string('.', padding);
            Console.WriteLine(new string('-', center));
            List<Loan> allLoans = loanUser.LoadAll(user.Id);
            Console.WriteLine(new string('-', center));
            Console.WriteLine(centeredText);  // Headline for account show.
            Console.WriteLine(new string('-', center));
            Console.WriteLine(
                $"{"Nr:."}" +
                $"{CenterText(".:Loan Type:.", width)}" +
                $"{CenterText(".:Amount:.", width)}" +
                $"{CenterText(".:Interest:.", width)}" +
                $"{CenterText(".:Loan Start:.", width)}");
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
                    $"{CenterText(loan.LoanStart.ToString("yyyy-MM-dd"), width)}");
                    nr++;
                    Console.ResetColor();
                    Console.WriteLine(new string('-', center));
                }
            }
            Console.Write("Press any key to go back. . .");
            Console.ReadKey();
            LoanSection(ActiveUserSingleton.GetInstance(), new LoanDataHandler());
        }

        public void ApplyForLoan(User user, LoanDataHandler loanData, LoanFactory loanFactory, AccountDataHandler activeUser)
        {
            Console.Clear();
            bool loanBool = true;
            string id = user.Id;
            decimal amount = 0;
            decimal maxLoan = 0;
            decimal maxValue = 0;
            string currentLoan = "";
            List<Account> accounts = activeUser.LoadAll(user.Id);
            
            SetMaxLoan(ref maxLoan);

            Console.WriteLine(new string('-', 80));
            Console.WriteLine(new string('-', 80));
            Console.WriteLine($"Wich type of loan would you like to apply for {user.UserName}?\n");
            Console.WriteLine("[1] Car-loan.");
            Console.WriteLine("[2] Housing-mortgage-loan.");
            Console.WriteLine("[3] Return to main menu.");
            Console.Write("Choose option:");
            int userChoice = Format.Choice(3);

            switch (userChoice)
            {
                case 1:
                    CarLoan();
                    return;
                case 2:
                    Mortgage();
                    return;
                case 3:
                    return;
                default:
                    throw new Exception("Invalid menu choice");
            }

            void CarLoan()
            {
                var ldh = new LoanDataHandler();
                double interest = ldh.GetInterest("CarLoan");
                currentLoan = "Car - Loan";
                while (loanBool)
                {
                    Console.Clear();
                    Console.WriteLine(new string('-', 80));
                    Console.WriteLine(new string('-', 80));
                    Console.WriteLine($"Should you wish to apply for a Car-loan.\n" +
                        $"You are eligible for a loan of this amount: {maxLoan.ToString("C")}\n");
                    Console.WriteLine(new string('-', 80));
                    Console.WriteLine("Would you like to continue?");
                    Console.WriteLine("[1] - Yes.");
                    Console.WriteLine("[2] - No.");
                    int userChoice = Format.Choice(2);

                    switch (userChoice)
                    {
                        case 1:
                            Console.Clear();
                            Console.WriteLine(new string('-', 80));
                            Console.WriteLine($":[ {currentLoan} ]::..::[ Max Amount: {maxLoan.ToString("C")} ]::..::[ Interest: **** ]::");
                            Console.WriteLine(new string('-', 80));
                            Console.Write("How much would you like to loan: ");
                            if (!decimal.TryParse(Console.ReadLine(), out amount))
                            {
                                Console.WriteLine("Invalid input. Make sure you write an integer.");
                                Console.WriteLine("Press any key to try again.");
                                Console.ReadKey();
                            }
                            else if (amount > maxLoan)
                            {
                                Process();
                                Console.WriteLine(new string('-', 80));
                                Console.WriteLine("Invalid amount. You are not eligible for that.");
                                Console.WriteLine("Press any key to try again.");
                                Console.ReadKey();
                                amount = 0;
                            }
                            else
                            {
                                Console.WriteLine(new string('-', 80));
                                Console.WriteLine($":[ {currentLoan} ]::..::[ Amount: {amount.ToString("C")} ]::..::[ Interest: {interest.ToString("P")} ]:");


                                Process();
                                loanBool = false;
                                DateTime loanTimeStamp = DateTime.Now;
                                loanData.Save(loanFactory.NewLoan(id, amount, "CarLoan"));
                                Console.WriteLine(new string('-', 80));
                                Console.WriteLine($"Congratulations {user.UserName} on your new loan.\n" +
                                    $"::Loan:[ {currentLoan} ]\n" +
                                    $"::Amount: [ {amount.ToString("C")}]\n" +
                                    $"::Interest rate: [ {interest.ToString("P")} ]\n" +
                                    $"::Approved: [ {loanTimeStamp} ]");
                                Console.WriteLine(new string('-', 80));
                                Console.ReadKey();
                                break;
                            }
                            break;
                        case 2:
                            return;
                        default:
                            throw new Exception("Invalid menu choice");
                    }
                }
                Console.WriteLine("Wich account would you like the loan to be deposited into?");
                ShowAccounts(ActiveUserSingleton.GetInstance(), new AccountDataHandler());
            }

            void Mortgage()
            {
                var ldh = new LoanDataHandler();
                double interest = ldh.GetInterest("MortgageLoan");
                currentLoan = "Housing - Mortgage - Loan";

                while (loanBool)
                {
                    Console.Clear();
                    Console.WriteLine(new string('-', 80));
                    Console.WriteLine(new string('-', 80));
                    Console.WriteLine($"Should you wish to apply for a mortgage.\n\n" +
                        $"You are eligible for a loan of this amount: {maxLoan.ToString("C")}\n" +
                        $"With a interest rate of {interest.ToString("P")}.\n\n");
                    Console.WriteLine(new string('-', 80));
                    Console.WriteLine("Would you like to continue?");
                    Console.WriteLine("[1] - Yes.");
                    Console.WriteLine("[2] - No.");
                    int userChoice = Format.Choice(2);

                    switch (userChoice)
                    {
                        case 1:
                            Console.Clear();
                            Console.WriteLine($"..::[ {currentLoan} ]::..::[ Max Amount: {maxLoan.ToString("C")} ]::..::[ Interest: {interest.ToString("P")} ]::..");
                            Console.WriteLine(new string('-', 80));
                            Console.Write("How much would you like to loan: ");
                            if (!decimal.TryParse(Console.ReadLine(), out amount))
                            {
                                Console.WriteLine(new string('-', 80));
                                Console.WriteLine("Invalid input. Makre sure you write an integer.");
                                Console.WriteLine("Press any key to try again.");
                                Console.ReadKey();
                            }
                            else if (amount > maxLoan)
                            {
                                Process();
                                Console.WriteLine(new string('-', 80));
                                Console.WriteLine("Invalid amount. You are not eligible for that.");
                                Console.WriteLine("Press any key to try again.");
                                amount = 0;
                                Console.ReadKey();
                            }
                            else
                            {
                                Process();
                                loanBool = false;
                                DateTime loanTimeStamp = DateTime.Now;
                                loanData.Save(loanFactory.NewLoan(id, amount, "MortgageLoan"));
                                Console.WriteLine(new string('-', 80));
                                Console.WriteLine($"Congratulations {user.UserName} on your new loan.\n" +
                                    $"Loan type: [ {currentLoan} ]\n" +
                                    $"Loan amount: [ {amount.ToString("C")}]\n" +
                                    $"Interest rate: [ {interest.ToString("P")} ]\n" +
                                    $"Approved: [ {loanTimeStamp} ]");
                                Console.WriteLine(new string('-', 80));
                                Console.WriteLine("Press any key to get to the main menu.");
                                return;
                            }
                            break;
                        case 2:
                            return;
                        default:
                            throw new Exception("Invalid menu choice");
                    }

                }
            }
            void Process()
            {
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
                    Console.Clear();
                    if (amount < maxLoan)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Loan granted.");
                        Thread.Sleep(1500);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Loan denied.");
                        Thread.Sleep(1500);
                    }
                    Console.ResetColor();
                    Console.Clear();
                }
            }

            decimal SetMaxLoan(ref decimal maxLoan)
            {
                User current = ActiveUserSingleton.GetInstance();

                var activeUser = new AccountDataHandler();

                List<Account> accounts = activeUser.LoadAll(current.Id);
                foreach (var acc in accounts)
                {
                    maxLoan += acc.Balance;
                }
                decimal maxLoanAmount = maxLoan * 5;

                current.LoanLimit = maxLoanAmount;

                return maxLoanAmount;
            }

        }
    }
}
