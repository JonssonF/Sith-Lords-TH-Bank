
using System.Globalization;

namespace TH_Bank
{
    public class CustomerMenu : Menu
    {
        
        public CustomerMenu()
        {
            _menu = new string[] // Menu in array = easy to add options.
            {
                "1. Show accounts / balance.",
                "2. Show transactions.",
                "3. Internal transaction.",
                "4. External transaction.",
                "5. Apply for loan.",
                "6. Open new account.",
                "7. Logout.",
                "8. Exit program.",
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
                        ShowLoans(ActiveUserSingleton.GetInstance(), new LoanDataHandler());
                        Console.Write("Press any key to go back. . .");
                        Console.ReadLine();
                        Console.Clear();
                        ShowMenu();
                        break;
                    case 2:
                        //Show transactions.
                        break;
                    case 3:
                        // Transfer between accounts.
                        MakeTransaction(ActiveUserSingleton.GetInstance(), new AccountDataHandler());
                        break;
                    case 4:
                        //Transfer between customers.
                        MakeExternalTransaction(ActiveUserSingleton.GetInstance(), new AccountDataHandler());
                        break;
                    case 5:
                        ApplyForLoan(ActiveUserSingleton.GetInstance(), new LoanDataHandler(), new LoanFactory(), new AccountDataHandler());
                        Console.Clear();
                        ShowMenu();
                        break;
                    case 6:
                        // Spot for open new account.
                        CreateNewAccount(ActiveUserSingleton.GetInstance(), new AccountFactory(), new AccountDataHandler());
                        break;
                    case 7:
                        Return(); //Log out.
                        break;
                    case 8:
                        Close(); // Close application.
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

                if (acc.Balance < 500)
                {
                    textColor = ConsoleColor.Red;
                }
                else
                {
                    textColor = ConsoleColor.Green;
                }
                Console.ForegroundColor = textColor;

                string currentCurrency = ""; // Variable that holds balance and current Currency.

                if (acc.Currency == "SEK")
                {
                    currentCurrency = acc.Balance.ToString("C", new CultureInfo("sv-SE"));
                }
                else if (acc.Currency == "USD")
                {
                    currentCurrency = acc.Balance.ToString("C", new CultureInfo("en-US"));
                }
                else
                {
                    currentCurrency = acc.Balance.ToString("C", CultureInfo.CurrentCulture);
                }
                
                Console.WriteLine(
                    $"{" "}{nr}{"."}{CenterText(acc.AccountType, width)}" + // Type of Account.
                    $"{CenterText(acc.AccountNumber.ToString(), width)}" + //Accountnumber.
                    $"{CenterText(currentCurrency, width)}" + //Formatted Currency variable, from if statement. Shows balance and currency.
                    $"{CenterText(acc.GetInterest().ToString("P"), width)}");
                nr++;
                //$"{CenterText(acc.Balance.ToString("C"), width)}" + // Balance.             /*Lägger denna här under tiden ifall "balance / currency variabeln inte ska användas.*/
                Console.ResetColor();
            }
        }
        public string CenterText(string text, int width) // A method to align the text in columns when showing accounts.
        {
            
            int padding = (width - text.Length) / 2;
            string paddedText = text.PadLeft(padding + text.Length);
            paddedText = paddedText.PadRight(width);
            return paddedText;
        }

        public void ShowLoans(User user, LoanDataHandler loanUser)
        {
            int width = 20;
            int center = 80; // Used for dividing lines, and to align column headers.
            string text = $".:{user.UserName}'s Loans:."; //Headline.
            int padding = (center - text.Length) / 2;
            string centeredText = new string('.', padding) + text + new string('.', padding);
            Console.WriteLine(new string('-', center));
            List<Loan> allLoans = loanUser.LoadAll(user.Id);
            List<Loan> loanList = allLoans.Where(loan => loan.Id == user.Id).ToList(); // LINQ Method to filter away accounts that isn't current users.

            Console.WriteLine(centeredText);  // Headline for account show.
            Console.WriteLine(new string('-', center));
            Console.WriteLine(
                $"{"Nr:."}"+
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
                    Console.WriteLine(
                        $"{" "}{nr}{"."}{CenterText(loan.LoanType, width)}" +
                        $"{CenterText(loan.Amount.ToString("C"), width)}" +
                        $"{CenterText(loan.Interest.ToString("P"), width)}" +
                        $"{CenterText(loan.LoanStart.ToString("yyyy-MM-dd"), width)}");
                nr++;
                }
            }

            Console.WriteLine(new string('-', center));
        }

        public void MakeTransaction(User user, AccountDataHandler activeUser)
        {
            ShowAccounts(user, activeUser);

            List<Account> userAccounts = activeUser.LoadAll(user.UserName);
            //List<Account> allAccounts = new AccountDataHandler.LoadAll();

            Console.WriteLine("Enter account number to transfer from: ");
            int fromAccount = int.Parse(Console.ReadLine());

            decimal amount = 0;
            int toAccount = 0;
            bool validFromAccount = false;
            bool validToAccount = false;

            foreach (Account userAccount in userAccounts)
            {
                if (userAccount.AccountNumber == fromAccount)
                {
                    validFromAccount = true;
                    Console.WriteLine("Enter account number to transfer to: \n" +
                    "(Other customers account number available for external transaction)");
                    toAccount = int.Parse(Console.ReadLine());
                    //foreach (Account account in allAccounts)
                    //{
                    //    if (allAccounts.AccountNumber == toAccount)
                    //    {
                    //        validToAccount = true;
                    //        Console.WriteLine("Enter amount to transfer: ");
                    //        amount = decimal.Parse(Console.ReadLine());
                    //    }
                    //}
                }
            }
            if (validFromAccount == false || validToAccount == false)
            {
                Console.WriteLine("Invalid account number.");
            }
            else
            {
                //Transaction transaction = new TransactionFactory().CreateTransaction(amount, fromAccount, toAccount, Id);
                //transaction.TransferFunds();
            }
            Console.Write("Press any key to return to menu.");
            Console.ReadLine();
            Console.Clear();
            ShowMenu();

        }
        public void MakeExternalTransaction(User user, AccountDataHandler activeUser)
        {

        }
        public void CreateNewAccount(User user, AccountFactory accountFactory, AccountDataHandler activeUser)
        {
            List<Account> accountList = activeUser.LoadAll(user.UserName);

            AccountFactory acc1 = new AccountFactory();
            Console.WriteLine("Enter currency: ");
            decimal balance = 0;
            string currency = Console.ReadLine();
            Console.WriteLine("Enter account owner: ");
            string ownerid = Console.ReadLine();
            Console.WriteLine("Enter accounttype: ");
            string userchoice = Console.ReadLine();


            Account account = accountFactory.CreateAccount(ownerid, balance, currency, userchoice);

        }

        public void ApplyForLoan(User user, LoanDataHandler loanData, LoanFactory loanFactory, AccountDataHandler activeUser)
        {
            Console.Clear();
            bool loanBool = true;
            string id = user.Id;
            decimal amount = 0;
            double interest = 0;
            decimal maxLoan = 0;
            decimal maxValue = 0;
            string currentLoan = "";
            List<Account> accounts = activeUser.LoadAll(user.Id);
            
            foreach (var acc in accounts) // Counts total value of users accounts.
            {
                maxValue += acc.Balance;
            }
            maxLoan = maxValue * 5; // Counts max loan value.

            Console.WriteLine(new string('-', 80));
            Console.WriteLine($"Wich type of loan would you like to apply for {user.UserName}?");
            Console.WriteLine("1. Car-loan.");
            Console.WriteLine("2. Mortgage.\n");
            Console.WriteLine("\n3. Return to main menu.");
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
                currentLoan = "Car - Loan";
                interest = 0.06;
                while (loanBool)
                {
                    Console.Clear();
                    Console.WriteLine(new string('-', 80));
                    Console.WriteLine($"Should you wish to apply for a Car-loan.\n" +
                        $"You are eligible for a loan of this amount: {maxLoan.ToString("C")}\n" +
                        $"With a interest rate of {interest.ToString("P")}.\n\n");
                    Console.WriteLine("Would you like to continue?");
                    Console.WriteLine("[1] - Yes.");
                    Console.WriteLine("[2] - No.");
                    int userChoice = Format.Choice(2);

                    switch (userChoice)
                    {
                        case 1:
                            Console.Clear();
                            Console.WriteLine(new string('-', 80));
                            Console.WriteLine($"..::[ {currentLoan} ]::..::[ Max Amount: {maxLoan.ToString("C")} ]::..::[ Interest: {interest.ToString("P")} ]::..");
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
                                Process();
                                loanBool = false;
                                DateTime loanTimeStamp = DateTime.Now;
                                loanData.Save(loanFactory.NewLoan(id, amount, interest, "CarLoan"));
                                Console.WriteLine(new string('-', 80));
                                Console.WriteLine($"Congratulations {user.UserName} on your new loan.\n" +
                                    $"Loan type: [ {currentLoan} ]\n" +
                                    $"Loan amount: [ {amount.ToString("C")}]\n" +
                                    $"Interest rate: [ {interest.ToString("P")} ]\n" +
                                    $"Approved: [ {loanTimeStamp} ]");
                                Console.WriteLine(new string('-', 80));
                                Console.WriteLine("Press any key to get to the main menu.");
                                Console.ReadKey();
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

            void Mortgage()
            {

                currentLoan = "Mortgage - Loan";
                interest = 0.04;
                while (loanBool)
                {
                    Console.Clear();
                    Console.WriteLine(new string('-', 80));
                    Console.WriteLine($"Should you wish to apply for a mortgage.\n\n" +
                        $"You are eligible for a loan of this amount: {maxLoan.ToString("C")}\n" +
                        $"With a interest rate of {interest.ToString("P")}.\n\n");
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
                                loanData.Save(loanFactory.NewLoan(id, amount, interest, "Mortgage"));
                                Console.WriteLine(new string('-', 80));
                                Console.WriteLine($"Congratulations {user.UserName} on your new loan.\n" +
                                    $"Loan type: [ {currentLoan} ]\n" +
                                    $"Loan amount: [ {amount.ToString("C")}]\n" +
                                    $"Interest rate: [ {interest.ToString("P")} ]\n" +
                                    $"Approved: [ {loanTimeStamp} ]");
                                Console.WriteLine(new string('-', 80));
                                Console.WriteLine("Press any key to get to the main menu.");
                                Console.ReadKey();
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


        }
    }
}
