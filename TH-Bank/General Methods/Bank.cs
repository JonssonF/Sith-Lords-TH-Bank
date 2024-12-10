using System.Media;

namespace TH_Bank
{
    public class Bank
    {

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
                "CUS00000000|112004|3000|SEK|SalaryAccount"

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
                "LastReview|2024|12|10|10|12|10"
            };

            string[] defaultLoans =
                {
                    "///INTERESTRATES///",
                    "CarLoan|0,08",
                    "MortgageLoan|0,05",
                    "///ENDINTERESTRATES///",
                };

            string[] defaultTransactions =
            {

                //"TRA00000001",
                //"2024-12-09 14:56:07",
                //"100",
                //"112004",
                //"112003",
                //"SEK",
                //"CUS00000000",
                //"CUS00000000"
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
                File.AppendAllLines(FilePaths.TransactionPath, defaultTransactions);
            }
            if (!File.Exists(FilePaths.SystemPath))
            {
                File.AppendAllLines(FilePaths.SystemPath, defaultSystem);
            }
            if (!File.Exists(FilePaths.LoanPath))
            {
                File.AppendAllLines(FilePaths.LoanPath, defaultLoans);
            }
            if (!File.Exists(FilePaths.CurrencyPath))
            {
                File.AppendAllLines(FilePaths.CurrencyPath, defaultCurrencies);
            }
        }

        public static void Intro()
        {
            Console.ReadKey();
            SoundPlayer sound = new SoundPlayer(@"C:\Users\Fredr\Desktop\S K O L A\P R O J E K T\TH-Bank\intro.wav");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("" +
                "\t _____                                _____ \r\n" +
                "\t( ___ )------------------------------( ___ )\r\n" +
                "\t |   |                                |   | \r\n" +
                "\t |   |      ____ ___ _____ _   _      |   | \r\n" +
                "\t |   |     / ___|_ _|_   _| | | |     |   | \r\n" +
                "\t |   |     \\___ \\| |  | | | |_| |     |   | \r\n" +
                "\t |   |      ___) | |  | | |  _  |     |   | \r\n" +
                "\t |   |  _  |____/___|_|_|_|_| |_|__   |   | \r\n" +
                "\t |   | | |   / _ \\|  _ \\|  _ \\/ ___|  |   | \r\n" +
                "\t |   | | |  | | | | |_) | | | \\___ \\  |   | \r\n" +
                "\t |   | | |__| |_| |  _ <| |_| |___) | |   | \r\n" +
                "\t |   | |_____\\___/|_| \\_\\____/|____/  |   | \r\n" +
                "\t |___|                                |___| \r\n" +
                "\t(_____)------------------------------(_____)");
            Console.ReadKey();
            sound.Play();
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            string intro = "\nA class was tasked with developing a banking application for their project.\n" +
                "Using their creativity and teamwork, they built a secure platform capable of\n" +
                "handling complex transactions while overcoming coding challenges along the way.\n\n" +
                "It was an exciting journey of learning and innovation. . . .";
            foreach (char c in intro)
            {
                Console.Write(c);
                Thread.Sleep(25);
            }
            Console.ReadKey();
            sound.Stop();
            Console.Clear();
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
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("" +
            " _____ _   _  ______  ___  _   _ _   __\r\n" +
            "|_   _| | | | | ___ \\/ _ \\| \\ | | | / /\r\n" +
            "  | | | |_| | | |_/ / /_\\ \\  \\| | |/ / \r\n" +
            "  | | |  _  | | ___ \\  _  | . ` |    \\ \r\n" +
            "  | | | | | | | |_/ / | | | |\\  | |\\  \\\r\n" +
            "  \\_/ \\_| |_/ \\____/\\_| |_|_| \\_|_| \\_/\n");
                Console.ResetColor();
                Console.WriteLine("\nPlease enter your username: ");

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

                if (userDataHandler.BlockCheck(userName) && userDataHandler.Exists(userName))
                {
                    userValidation = true;
                }

            }


            while (loginAttempts < maxAttempts)
            {
                Console.WriteLine($"{userName} please type in your password: ");
                passWord = HidePassword();


                if (userDataHandler.PasswordCheck(userName, passWord))
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

                if (loginAttempts == maxAttempts)
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
            if (user.UserType == "Admin")
            {
                menu = new AdminMenu();
            }
            else if (user.UserType == "Customer")
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
