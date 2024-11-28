﻿
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
                "3. Transfer funds.",
                "4. Apply for loan.",
                "5. Logout.",
                "6. Exit program.",
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
                        break;

                    case 2:
                        //Show transactions.
                        break;

                    case 3:
                        // Transfer funds.
                        break;

                    case 4:
                        //Set up a new loan.
                        break;

                    case 5:
                        Return(); //Log out.
                        break;

                    case 6:
                        Close(); // Close application.
                        break;

                    case 7:

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
            int center = 80; // Used for dividing lines, and to align column headers.
            string text = $".:{user.UserName}'s Accounts:."; //Headline.
            int padding = (center - text.Length) / 2;
            string centeredText = new string('.', padding) + text + new string('.', padding);
            ConsoleColor textColor;
            List<Account> accountList = activeUser.LoadAll(user.Id);
            CultureInfo currencyFormat;


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
            Console.Write("Press any key to go back.");
            Console.ReadLine();
            Console.Clear();
            ShowMenu();
        }

        public string CenterText(string text, int width) // A method to align the text in columns when showing accounts.
        {
            int padding = (width - text.Length) / 2;
            string paddedText = text.PadLeft(padding + text.Length);
            paddedText = paddedText.PadRight(width);
            return paddedText;
        }
    }
}
