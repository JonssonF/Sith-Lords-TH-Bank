using System.Reflection.Metadata.Ecma335;

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
                Console.WriteLine("Enter first name of customer (minimum 2 characters)");
                firstName = Format.StringMinimumInput(2);
                Console.WriteLine("Enter last name of customer (minimum 2 characters)");
                lastName = Format.StringMinimumInput(2);
                Console.WriteLine("Enter a UserName for Customer (at least 5 characters)");
                userName = Format.StringMinimumInput(5);
                Console.WriteLine("Enter a Password for Customer (at least 5 characters");
                passWord = Format.StringMinimumInput(5);

                userDataHandler.Save(userFactory.CreateUser(userName, passWord, firstName, lastName, "Customer"));
            }

            void CreateAdmin()
            {
                bool creating = true;
                while(creating)
                {
                    Console.WriteLine("Enter a UserName for new Admin (must contain at least 5 characters)");
                    userName = Format.StringMinimumInput(5);
                    
                    Console.WriteLine("Enter a Password for new Admin (must contain at least 5 characters)");
                    passWord = Format.StringMinimumInput(5);

                    userDataHandler.Save(userFactory.CreateUser(userName, passWord, null, null, "Admin"));

                }
                
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
