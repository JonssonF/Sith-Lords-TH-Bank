

using System.Runtime.Serialization;

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
            Console.Clear();
            LogoText();
            if (TimeToReview())
            {
                Console.Beep();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n -- WARNING! Currency exchange rates must be reviewed immediately! --\n");
                Console.ForegroundColor = ConsoleColor.Gray;
                UpdateExchangeRates();
            }
            DrawBorder();

            

            foreach (string item in _menu)
            {
                DrawMenuItem(item);
            }
            
            DrawBorder();
            MenuAdmin();
        }

        public override void ShowAccounts(User user, AccountDataHandler activeUser)
        {
            throw new NotImplementedException();
        }


        public void MenuAdmin()
        {
            optionCount = _menu.Length; // Combined with Choice method from MenuClass wrongful inputs can't be made.
            access = true;
            while (access)
            {
                
                int adminChoice = Format.Choice(optionCount); 
                switch (adminChoice)
                {

                    case 1:
                        // Add new customer. 
                        CreateUser(new UserDataHandler(), new UserFactory());
                        break;

                    case 2:
                        // Unblock user.
                        UnblockUser(new UserDataHandler());
                        break;

                    case 3:
                        // Change currency exchange rate.
                        UpdateExchangeRates();
                        break;

                    case 4:
                        Return();
                        break;

                    case 5:
                        Close();
                        break;

                    default:
                        break;
                }
            }
        }

        public void CreateUser(UserDataHandler userData, UserFactory userFactory)
        {
            Console.WriteLine("What type of user do you want to create?");
            Console.WriteLine("1. Customer");
            Console.WriteLine("2. Admin");
            int choice = Format.Choice(2);

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

                userData.Save(userFactory.CreateUser(userName, passWord,firstName, lastName, "Customer"));
            }

            void CreateAdmin()
            {
                Console.WriteLine("Enter a UserName for new Admin");
                userName = Console.ReadLine();
                Console.WriteLine("Enter a Password for new Admin");
                passWord = Console.ReadLine();

                userData.Save(userFactory.CreateUser(userName, passWord, null, null, "Admin"));
            }
        }

        public void UnblockUser(UserDataHandler userDataHandler)
        {
            List<User> allUsers = userDataHandler.LoadAll();
            Console.WriteLine("Blocked Users:");
            foreach(User user in allUsers)
            {
                if(user.ToString().Contains("Blocked:True"))
                {
                    Console.WriteLine($"{user.Id}: {user.UserName} ({user.UserType})");
                }
            }

            Console.WriteLine("Which user do you want to unblock? Enter ID:");
            string unblockId = Console.ReadLine();

            var unblockMe = allUsers.Find(x => x.Id == unblockId);

            unblockMe.IsBlocked = false;

            userDataHandler.Save(unblockMe);

            Console.WriteLine($"You have unblocked user {unblockMe.UserName}!");
        }

        public void UpdateExchangeRates()
        {
            Console.WriteLine("Chooce a currency to edit:");
            int loop = 1;
            foreach (Currency c in ExchangeCurrency.currencies)
            {
                Console.WriteLine($"{loop}: {c.Name} ({c.NameShort})");
                loop++;
            }
            loop = 1;
            int choice = Format.Choice(ExchangeCurrency.currencies.Length) - 1;
            var xdh = new ExchangeDataHandler();
            Currency chosen = ExchangeCurrency.currencies[choice];
            Console.WriteLine($"Choose rate to edit for {chosen.NameShort}:");
            List<string> keys = new List<string>();
            List<double> values = new List<double>();
            foreach (var item in chosen.ExchangeRates)
            {
                Console.WriteLine($"{loop}. {item.Key}: {item.Value}");
                keys.Add(item.Key);
                loop++;
                
            }
            choice = Format.Choice(chosen.ExchangeRates.Count) - 1;

            Console.WriteLine($"Enter new rate for {chosen.NameShort} -> {keys[choice]} (current: {chosen.ExchangeRates[keys[choice]]})");

            chosen.ExchangeRates[keys[choice]] = Double.Parse(Console.ReadLine());
            Console.WriteLine($"New rate for {chosen.NameShort} -> {keys[choice]}: {chosen.ExchangeRates[keys[choice]]} ");
            xdh.Save(chosen);
            ExchangeCurrency.Review(DateTime.Now);
        }

        public bool TimeToReview()
        {
            DateTime now = DateTime.Now;

            DateTime lastReview = ExchangeCurrency.GetLastReview();

            TimeSpan interval = now - lastReview;

            if(interval.TotalHours > 24)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
