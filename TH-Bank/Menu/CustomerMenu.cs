
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
                "3. Perform transaction.",
                "4. Apply for loan.",
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
                        Console.Write("Press any key to go back.");
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
                        break;
                    case 4:
                        // Lån alternativet.
                        Loan.ApplyForLoan(ActiveUserSingleton.GetInstance());
                        break;
                    case 5:
                        // Spot for open new account.
                        CreateNewAccount(ActiveUserSingleton.GetInstance(), new AccountFactory(), new AccountDataHandler());
                        break;
                    case 6:
                        Return(); //Log out.
                        break;
                    case 7:
                        Close(); // Close application.
                        break;
                }
            }
        }
        public override void ShowAccounts(User user, AccountDataHandler activeUser)
        {
            Console.Clear();
            int width = 20;
            int center = 80; // Used for dividing lines, and to align column headers.
            string text = $".:{user.UserName}'s Accounts:."; //Headline.
            int padding = (center - text.Length) / 2;
            string centeredText = new string('.', padding) + text + new string('.', padding);
            ConsoleColor textColor;
            CultureInfo currencyFormat;
            List<Account> allAccounts = activeUser.LoadAll(user.Id);
            List<Account> accountList = allAccounts.Where(acc => acc.OwnerID == user.Id).ToList(); // LINQ Method to filter away accounts that isn't current users.

            if (accountList == null || accountList.Count == 0)
            {

                textColor = ConsoleColor.Red;
                Console.WriteLine($"Username: {user.UserName} have no registered accounts at the moment.");
                Thread.Sleep(2000);
                ShowMenu();
                return;
            }

            Console.WriteLine(centeredText);  // Headline for account show.
            Console.WriteLine(new string('-', center));
            Console.WriteLine(
                $"{CenterText(".:Account Type:.", width)}" +
                $"{CenterText(".:Account Number:.", width)}" +
                $"{CenterText(".:Balance:.", width)}" +
                $"{CenterText(".:Interest:.", width)}");
            Console.WriteLine(new string('-', center));


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
                    $"{CenterText(acc.AccountType, width)}" + // Type of Account.
                    $"{CenterText(acc.AccountNumber.ToString(), width)}" + //Accountnumber.
                    $"{CenterText(currentCurrency, width)}" + //Formatted Currency variable, from if statement. Shows balance and currency.
                    $"{CenterText(acc.Interest.ToString("%"), width)}");

                //$"{CenterText(acc.Balance.ToString("C"), width)}" + // Balance.             /*Lägger denna här under tiden ifall "balance / currency variabeln inte ska användas.*/
                Console.ResetColor();
            }
            Console.WriteLine(new string('-', center));
            
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
            Console.WriteLine("1. Transaction between own accounts");
            Console.WriteLine("2. Transaction to external account");
            int transChoice = Format.Choice(2);
            Console.Clear();
            ShowAccounts(user, adh);

            List<Account> userAccounts = adh.LoadAll(user.Id);
            List<Account> allAccounts = adh.LoadAll();
            Account[] accountArray = userAccounts.ToArray();
            optionCount = userAccounts.Count;
            int fromAccount = 0;
            int toAccount = 0;
            bool validToAccount = false;

            switch (transChoice)
            {
                case 1:
                    Console.WriteLine("1. Transaction between own accounts");
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
                        fromAccount = accountArray[accChoiceFrom -1].AccountNumber;
                        toAccount = accountArray[accChoiceTo -1].AccountNumber;
                    }
                    break;
                case 2:
                    Console.WriteLine("2. Transaction to external account");
                    int accChoice = ValidOwnAccount("from");
                    fromAccount = accountArray[accChoice -1].AccountNumber;
                    Console.WriteLine("Enter recieving account number: ");
                    toAccount = Format.IntegerInput(6);

                    foreach (Account account in allAccounts)
                    {
                        if (toAccount == account.AccountNumber)
                        {
                            validToAccount = true;
                            Console.WriteLine($"Reciever: ");  //Fundera på åtkomst till reciever
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
            //Lägg till "if amount > Balance"... Balance verkar inte finnas för tillfället.
            Transaction transaction = new TransactionFactory().CreateTransaction(amount, fromAccount, toAccount);
            //transaction.TransferFunds();
            Console.Clear();
            Console.WriteLine("Transaction complete.");
            Console.Write("Press any key to return to menu.");
            Console.ReadLine();
            Console.Clear();
            ShowMenu();
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
            List<Account> accountList = activeUser.LoadAll(user.UserName);

            AccountFactory acc1 = new AccountFactory();
            Console.WriteLine("Enter currency: ");
            decimal balance = 0;
            string currency = Console.ReadLine();
            Console.WriteLine("Enter account owner: ");
            string ownerid = Console.ReadLine();
            Console.WriteLine("Enter accounttype: ");
            string userchoice = Console.ReadLine();

            Account account = accountFactory.CreateAccount(ownerid,balance, currency, userchoice);

        }
    }
}
