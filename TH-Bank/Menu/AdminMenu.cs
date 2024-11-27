

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
            MenuAdmin();
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
                
                int adminChoice = Choice(optionCount); 
                switch (adminChoice)
                {

                    case 1:
                        // Add new customer. 
                        CreateUser(new UserDataHandler(), new UserFactory());
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

        public void CreateUser(UserDataHandler userData, UserFactory userFactory)
        {
            Console.WriteLine("What type of user do you want to create?");
            Console.WriteLine("1. Customer");
            Console.WriteLine("2. Admin");
            int choice = Choice(2);

            string firstName = "";
            string lastName = "";
            //string userChoice = "";
            string passWord = "";
            string userName = "";

            switch(choice)
            {
                case 1:
                    CreateCustomer();
                    break;
                case 2:
                    CreateAdmin();
                    break;
                default:
                    throw new Exception("Invalid menu choice");
            }

            Console.WriteLine($"You created a new User ({userData.Load(userName).UserType}): {userData.Load(userName).UserName}");

            void CreateCustomer()
            {
                Console.WriteLine("Enter Customer First Name");
                firstName = Console.ReadLine(); 
                Console.WriteLine("Enter Customer Last Name");
                lastName = Console.ReadLine();
                Console.WriteLine("Enter a UserName for Customer");
                userName = Console.ReadLine();
                Console.WriteLine("Enter a Password for Customer");
                passWord = Console.ReadLine();

                userData.Save(userFactory.CreateUser("USR", passWord, userName, firstName, lastName, "Customer"));
            }

            void CreateAdmin()
            {
                Console.WriteLine("Enter a UserName for new Admin");
                userName = Console.ReadLine();
                Console.WriteLine("Enter a Password for new Admin");
                passWord = Console.ReadLine();

                userData.Save(userFactory.CreateUser("USR", passWord, userName, null, null, "Admin"));
            }
        }
    }
}
