
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
                int customerChoice = Choice(optionCount);
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
                        MakeInternalTransaction(ActiveUserSingleton.GetInstance(), new AccountDataHandler());
                        break;
                    case 4:
                        //Transfer between customers.
                        MakeExternalTransaction(ActiveUserSingleton.GetInstance(), new AccountDataHandler());
                        break;
                    case 5:
                        // Lån alternativet.
                        Loan.ApplyForLoan(ActiveUserSingleton.GetInstance());
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
            int center = 80; // Used for dividing lines, and to align column headers.
            string text = $".:{user.UserName}'s Accounts:."; //Headline.
            int padding = (center - text.Length) / 2;
            string centeredText = new string('.', padding) + text + new string('.', padding);
            ConsoleColor textColor;
            List<Account> accountList = activeUser.LoadAll(user.Id);
            CultureInfo currencyFormat;

                if (accountList == null || accountList.Count == 0)
                {
                    
                    textColor = ConsoleColor.Red;
                    Console.WriteLine("\t\n\nYou dont have any accounts at the moment.");
                    Thread.Sleep(1500);
                    MenuCustomer();
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

        //Lät interna/externa transaktioner vara kvar i två separata metoder. Blev så brötigt med val av transaktionstyp
        //inne i en enda metod. Misstycker ni fullständigt så gör jag om.
        public void MakeInternalTransaction(User user, AccountDataHandler activeUser)
        {
            //ShowAccounts();   Ska det verkligen vara en ActiveUserSingleton som inparameter till ShowAccounts?

            List<Account> accountList = activeUser.LoadAll(user.Id);   //Behöver komma åt listan från ShowAccounts.
                                                                             //Går det att göra på annat sätt?
            Console.WriteLine("Enter account to transfer from: ");
            optionCount = accountList.Count();
            int indexFromAccount = Choice(optionCount);
            int fromAccount = accountList[indexFromAccount - 1].AccountNumber;
            Console.WriteLine("Enter account to transfer to: ");
            int indexToAccount = Choice(optionCount);
            int toAccount = accountList[indexToAccount - 1].AccountNumber;
            Console.WriteLine("Enter amount to transfer: ");
            decimal.TryParse(Console.ReadLine(), out decimal amount);    //Finns annan felhanteringslösning här?

            //Transaction transaction = new TransactionFactory().CreateTransaction(amount, fromAccount, toAccount, Id);
            //Id? Är det verkligen en inparameter? Eller är det ett transaktionsid unikt för detta transaktionsobjektet? 
            //Isf borde det väl genereras automatiskt i konstruktorn för transaktionsobjektet?
            //transaction.TransferFunds();
        }
        public void MakeExternalTransaction(User user, AccountDataHandler activeUser)
        {

        }
        public void CreateNewAccount(User user, AccountFactory accountFactory, AccountDataHandler activeUser)
        {
            List<Account> accountList = activeUser.LoadAll(user.UserName);

            AccountFactory acc1 = new AccountFactory();
            Console.WriteLine("Enter account balance: ");
            decimal ab = Decimal.Parse(Console.ReadLine());
            Console.WriteLine("Enter currency: ");
            string currency = Console.ReadLine();
            Console.WriteLine("Enter accountnumber: ");
            int an = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Enter account owner: ");
            string acOwner = Console.ReadLine();
            Console.WriteLine("Enter accounttype: ");
            string acType = Console.ReadLine();

            //Account account = accountFactory.CreateAccount(ab, currency, an, acOwner, acType);
        }



    }
}
