

namespace TH_Bank
{
    public class AdminMenu : Menu
    {
        public AdminMenu()
        {
            _menu = new string[] // Menu in array = easy to add options.
        {
            "1. Add new customer.",
            "2. Handle suspended customers.",
            "3. Change currency exchange rate.",
            "4. Logout.",
            "5. Exit program.",
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
        }

        public override void ShowAccounts(ActiveUserSingleton activeUser, AccountDataHandler accountDataHandler)
        {
            throw new NotImplementedException();
        }


        public void MenuAdmin()
        {
            optionCount = _menu.Length; // Combined with Choice method from MenuClass wrongful inputs can't be made.
            access = true;
            while (access)
            {
                ShowMenu();
                int adminChoice = Choice(optionCount); 
                switch (adminChoice)
                {

                    case 1:
                        // Add new customer. 

                        break;

                    case 2:
                        // Unblock user.

                        break;

                    case 3:
                        // Change currency exchange rate.

                        break;

                    case 4:
                        Return();
                        break;

                    case 5:
                        Close();
                        break;
                }
            }
        }
    }
}
