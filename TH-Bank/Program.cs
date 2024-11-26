

using System.Security.Cryptography.X509Certificates;

namespace TH_Bank
{
    internal class Program
    {
        static void Main(string[] args)
        {
            LogIn(new UserDataHandler());

            Console.ReadLine();

        }

        public static void LogIn(UserDataHandler userDataHandler)
        {
            bool userValidation = false;
            bool loginSuccess = false;
            int loginAttempts = 0;
            int maxAttempts = 3;
            string userName = "";
            string passWord = "";

            while (!userValidation)
            {
                Console.WriteLine("Welcome to TH-Bank.");
                Console.WriteLine("Please enter your username: ");
                userName = Console.ReadLine();

                // Does user exist?
                if(!userDataHandler.Exists(userName))
                {
                    Console.WriteLine("User does not exist!");
                    
                }

                // is user blocked?

                if (!userDataHandler.BlockCheck(userName))
                {
                    Console.WriteLine("You have been denied access. Contact you local office" +
                        "at office hours (9:30 AM - 10 AM on Wednesdays");
                }

                if(userDataHandler.BlockCheck(userName) && userDataHandler.Exists(userName))
                    {
                    userValidation = true;
                    }

            }


            while (loginAttempts < maxAttempts)
            {
                Console.WriteLine($"{userName} please type in your password.");
                passWord = Console.ReadLine();


                if (userDataHandler.PasswordCheck(userName,passWord))
                {

                    // Successful login! Loads user into active user spot
                    var activeUser = ActiveUserSingleton.GetInstance(userDataHandler.Load(userName));
                    
                    LoadMenu(activeUser.LoggedInUser);
                    break;

                }
                else
                {
                    loginAttempts++;
                    Console.WriteLine($"Login failed. {maxAttempts - loginAttempts} attempts left.");
                }

                if(loginAttempts == maxAttempts)
                {
                    User blockme = userDataHandler.Load(userName);
                    blockme.IsBlocked = true;
                    userDataHandler.Save(blockme);
                    
                }
                
            }


        }

        public static void LoadMenu(User user)
        {
            Menu menu;

            // Chooses the correct menu depending on user type.
            if(user.UserType == "Admin")
            {
                menu = new AdminMenu();
            }
            else if(user.UserType == "Customer")
            {
                menu = new CustomerMenu();
            }
            else
            {
                throw new Exception($"Ingen meny matchar användare av typen: {user.UserType}");
            }

            menu.ShowMenu();
        }
    }
}
