namespace TH_Bank
{
    public class UserFacade
    {
        public UserDataHandler userDataHandler;
        public UserFactory userFactory;

        public UserFacade()
        {
            userDataHandler = new UserDataHandler();
            userFactory  = new UserFactory();
        }

        public void CreateUser()
        {
            Console.WriteLine("What type of user do you want to create?");
            Console.WriteLine("1. Customer");
            Console.WriteLine("2. Admin");
            int choice = Format.Choice(2);

            string firstName = "";
            string lastName = "";
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

            Console.WriteLine($"You created a new User ({userDataHandler.Load(userName).UserType}): {userDataHandler.Load(userName).UserName}");
            Console.WriteLine($"Press any key to return to menu");
            Console.ReadKey(true);

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

                userDataHandler.Save(userFactory.CreateUser(userName, passWord, firstName, lastName, "Customer"));
            }

            void CreateAdmin()
            {
                Console.WriteLine("Enter a UserName for new Admin");
                userName = Console.ReadLine();
                Console.WriteLine("Enter a Password for new Admin");
                passWord = Console.ReadLine();

                userDataHandler.Save(userFactory.CreateUser(userName, passWord, null, null, "Admin"));
            }
        }

        public void UnblockUser()
        {
            List<User> allUsers = userDataHandler.LoadAll();
            List<User> blockedUsers = new List<User>();

            foreach (User user in allUsers)
            {
                if (user.IsBlocked == true)
                {
                    blockedUsers.Add(user);
                }
            }

            if (blockedUsers.Count > 0)
            {

                Console.WriteLine("Blocked Users:");
                foreach (User user in blockedUsers)
                {
                    Console.WriteLine($"{blockedUsers.IndexOf(user) + 1}: {user.Id}: {user.UserName} ({user.UserType})");
                }

                Console.WriteLine("Which user do you want to unblock? Select a number from the list.");
                int unblockThis = Format.Choice(allUsers.Count);

                var unblockMe = blockedUsers[unblockThis - 1];

                unblockMe.IsBlocked = false;

                userDataHandler.Save(unblockMe);

                Console.WriteLine($"You have unblocked user {unblockMe.UserName}!");
                Console.WriteLine("Press any key to return to menu.");
                Console.ReadKey(true);
            }
            else
            {
                Console.WriteLine("There are no blocked users currently! Press any key to return to menu.");
                Console.ReadKey(true);
            }
        }

    }
}
