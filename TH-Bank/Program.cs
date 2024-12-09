﻿

using System.Media;
using System.Security;
using System.Security.Cryptography.X509Certificates;

namespace TH_Bank
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CreateFiles();
            Format.Intro();
            LogIn(new UserDataHandler());

        }

        public static void CreateFiles()
        {
            string[] defaultUsers =
            {
                "USERID/USERNAME/PASSWORD/FIRSTNAME*/LASTNAME*/USERTYPE",
                "CUS00000000|UserName|Password|FirstName|LastName|Customer",
                "ADM00000000|Admin|Password|Admin"
            };

            string[] defaultSystem =
            {
                "CustomerIDCount|1",
                "AdminIDCount|1",
                "TransactionIDCount|1"
            };

            string[] defaultAccounts =
            {
                "///INTERESTRATES///",
                "SalaryAccount|0,0",
                "SavingsAccount|0,1",
                "///ENDINTERESTRATES///",
                "OwnerID/Accountnumber/Balance/Currency/AccountType",
                "CUS00000000|112003|1000|SEK|SalaryAccount",
                
            };

            string[] defaultCurrencies =
            {
                "SEK",
                "Swedish Krona",
                "USD|0,1",
                "EUR|0,09",
                "//ENDSwedish Krona//",
                "USD",
                "US Dollar",
                "SEK|10,0",
                "EUR|0,95",
                "//ENDUS Dollar//",
                "EUR",
                "European Euro",
                "SEK|10,1",
                "USD|1,05",
                "//ENDEuropean Euro//",
                "LastReview"
            };

            string[] defaultLoans = 
                {
                    "///INTERESTRATES///",
                    "CarLoan|0,08",
                    "MortgageLoan|0,05",
                    "///ENDINTERESTRATES///",
                };



            if (!File.Exists(FilePaths.AccountPath))
            {
                File.AppendAllLines(FilePaths.AccountPath, defaultAccounts);
            }
            if (!File.Exists(FilePaths.UserPath))
            {
                File.AppendAllLines(FilePaths.UserPath, defaultUsers);
            }
            if (!File.Exists(FilePaths.TransactionPath))
            {
                File.Create(FilePaths.TransactionPath);
            }
            if (!File.Exists(FilePaths.SystemPath))
            {
                File.AppendAllLines(FilePaths.SystemPath, defaultSystem);
            }
            if (!File.Exists(FilePaths.LoanPath))
            {
                File.AppendAllLines(FilePaths.LoanPath, defaultLoans);
            }
            if(!File.Exists(FilePaths.CurrencyPath))
            {
                File.AppendAllLines(FilePaths.CurrencyPath, defaultCurrencies);
            }
        }

        public static void LogIn(UserDataHandler userDataHandler)
        {
            bool userValidation = false;
            int loginAttempts = 0;
            int maxAttempts = 3;
            string userName = "";
            string passWord = "";

            while (!userValidation)
            {
                
                Console.ResetColor();
                Console.WriteLine("Welcome!");
                Console.WriteLine("Please enter your username: ");
                userName = Console.ReadLine();

                if (string.IsNullOrEmpty(userName))
                {
                    Console.WriteLine("Username cannot be empty, try again.");
                    continue;
                }

                // Does user exist?
                if (!userDataHandler.Exists(userName))
                {
                    Console.WriteLine("User does not exist!");
                }

                // is user blocked?
                if (!userDataHandler.BlockCheck(userName))
                {
                    Console.Clear();
                    Console.WriteLine("You have been denied access.\nContact us" +
                        " between office hours (9:30 AM - 10 AM on Wednesdays.");
                    Console.Write("Press any key to return...");
                    Console.ReadKey();
                    Console.Clear();
                    LogIn(new UserDataHandler());
                }

                if(userDataHandler.BlockCheck(userName) && userDataHandler.Exists(userName))
                    {
                    userValidation = true;
                    }

            }


            while (loginAttempts < maxAttempts)
            {
                Console.WriteLine($"{userName} please type in your password: ");
                passWord = HidePassword();


                if (userDataHandler.PasswordCheck(userName,passWord))
                {

                    // Successful login! Loads user into active user spot
                    var activeUser = ActiveUser.SetInstance(userDataHandler.Load(userName));
                    Console.Clear();
                    LoadMenu(activeUser);
                    break;
                }
                else
                {
                    loginAttempts++;
                    Console.WriteLine($"Login failed. {maxAttempts - loginAttempts} attempts left.");
                }

                if(loginAttempts == maxAttempts)
                {
                    Console.Clear();
                    Console.WriteLine("You have been denied access.\nContact us" +
                        " between office hours (9:30 AM - 10 AM on Wednesdays.)");
                    User blockme = userDataHandler.Load(userName);
                    blockme.IsBlocked = true;
                    userDataHandler.Save(blockme);
                    Console.Write("Press any key to return...");
                    Console.ReadKey();
                    Console.Clear();
                    LogIn(new UserDataHandler());

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
                throw new Exception($"Can't find menu that matches user type: {user.UserType}");
            }

            menu.ShowMenu();
        }
        static string HidePassword()
        {
            string pass = string.Empty;
            ConsoleKey key;

            do
            {
                var keyInfo = Console.ReadKey(intercept: true);
                key = keyInfo.Key;

                if (key == ConsoleKey.Backspace && pass.Length > 0)
                {
                    Console.Write("\b \b");
                    pass = pass[0..^1];
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    Console.Write("*");
                    pass += keyInfo.KeyChar;
                }
            } while (key != ConsoleKey.Enter); // Return when 'Enter' is pressed.

            return pass;
        }

    }
}
