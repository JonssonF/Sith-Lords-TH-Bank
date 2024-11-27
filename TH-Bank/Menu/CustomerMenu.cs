
namespace TH_Bank
{
    public class CustomerMenu : Menu
    {

        public CustomerMenu()
        {
            _menu = new string[] // Menu in array = easy to add options.
            {
                "1. Show balance / accounts.",
                "2. Show transactions.",
                "3. Internal transfer.",
                "4. External transfer.",
                "5. Apply for loan.",
                "6. Logout.",
                "7. Exit program.",
            };
            menuWidth = CalculateWidth(extraWidth: 10);
        }


        public override void ShowMenu()
        {
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
                        //ShowAccounts(); // Shows accounts & balance of current user.
                        ShowAccounts(ActiveUserSingleton.GetInstance(),new AccountDataHandler());
                        break;

                    case 2:
                        //Show transactions.
                        // Logger.
                        break;

                    case 3:
                        // Transfer between accounts.
                        break;

                    case 4:
                        //Transfer between customers.
                        break;

                    case 5:
                        //Set up a new loan.
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

            Console.WriteLine($"..::{user.UserName}'s Accounts::..{user.UserType}{user.Id}");

            Console.WriteLine($"\n\n UserName:{user.UserName} User Type: {user.UserType} User ID: {user.Id} Password: {user.PassWord} \n\n");

            activeUser.LoadAll(user.Id);
            List<Account> accountList = new List<Account>();


            foreach (var acc in accountList)
            {
                Console.WriteLine($"Account: {acc.AccountType} Account number: {acc.AccountNumber} Balance: {acc.Balance:C}");
                Console.WriteLine(acc.ToString());
            }



            Console.ReadKey();
            ShowMenu();
        }

    }
}
