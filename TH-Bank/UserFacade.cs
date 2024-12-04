namespace TH_Bank
{
    public class UserFacade
    {
        public UserDataHandler userData;
        public UserFactory userFactory;

        public UserFacade()
        {
            userData = new UserDataHandler();
            userFactory = new UserFactory();
        }
        public void CreateUser()
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

            switch (choice)
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

                userData.Save(userFactory.CreateUser(userName, passWord, firstName, lastName, "Customer"));
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

        public void UnblockUser()
        {
            List<User> allUsers = userData.LoadAll();
            Console.WriteLine("Blocked Users:");
            foreach (User user in allUsers)
            {
                if (user.ToString().Contains("Blocked:True"))
                {
                    Console.WriteLine($"{user.Id}: {user.UserName} ({user.UserType})");
                }
            }

            Console.WriteLine("Which user do you want to unblock? Enter ID:");
            string unblockId = Console.ReadLine();

            var unblockMe = allUsers.Find(x => x.Id == unblockId);

            unblockMe.IsBlocked = false;

            userData.Save(unblockMe);

            Console.WriteLine($"You have unblocked user {unblockMe.UserName}!");
        }
    }
}
