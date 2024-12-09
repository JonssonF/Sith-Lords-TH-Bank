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

                "TRA00000001",
                "2024-12-09 14:56:07",
                "100",
                "112004",
                "112003",
                "SEK",
                "CUS00000000",
                "CUS00000000"
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
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("" +
                "\t\t     _______. __  .___________. __    __  \r\n" +
                "\t\t    /       ||  | |           ||  |  |  | \r\n" +
                "\t\t   |   (----`|  | `---|  |----`|  |__|  | \r\n" +
                "\t\t    \\   \\    |  |     |  |     |   __   | \r\n" +
                "\t\t.----)   |   |  |     |  |     |  |  |  | \r\n" +
                "\t\t|_______/    |__|     |__|     |__|  |__| \r\n" +
                "                                         ");
            Console.WriteLine("" +
                "\t __        ______   .______       _______       _______.\r\n" +
                "\t|  |      /  __  \\  |   _  \\     |       \\     /       |\r\n" +
                "\t|  |     |  |  |  | |  |_)  |    |  .--.  |   |   (----`\r\n" +
                "\t|  |     |  |  |  | |      /     |  |  |  |    \\   \\    \r\n" +
                "\t|  `----.|  `--'  | |  |\\  \\----.|  '--'  |.----)   |   \r\n" +
                "\t|_______| \\______/  | _| `._____||_______/ |_______/    \r\n" +
                "                                                       ");
            //Console.WriteLine("                                                                                                  \r\n                         ..:                                                                        \r\n                        .:...                                                                       \r\n                       .+x+ ..                                                                      \r\n                  .. ..xxxx;..  .                                                                   \r\n             .:xXx: .;Xxxxxx;  ;.. .                                                                \r\n          .:+XxX+..:xXxxxxxxx:..xx+. .             .      ..                                        \r\n       ..:xxx+:...;xxxxxxxxxxx...xxx+..         .x&&&X$&X...                                        \r\n     . .+xXx:....+xxxxxxx+xxxxx. .;xxx:       .x$$$X$$$&;. .                                        \r\n      .+X++... .:xxxxxxxxxxxxxxx.. .x+X;  .  +&$&X....&$:..:;:...                                   \r\n     .+xx:.. ....;xXxXxxxxXxxxxx..   xxx.   ;&&$$x.  .x+.. .$&&&..;&&&&&&&X+;;.........    ...      \r\n    .;xX;...:;....+Xxxxxxx+xxx;...   .xxx.  :$&&X&;  ...  .:$&&$...xx$&&$&$$$X..;&&&&+....:XXx+..   \r\n   ..Xx+...;XX;.. .+XXxXxxxxx.....:.. :Xx;   +$&&&$:      .:&&$+ .....X&&&.......x&&$:....:&$&&...  \r\n   .:Xx:..xXxxx;  ..xxxxxxx;... :xx:...+xX   .+&&&&&..   . :&$$+ .....$$&&.......X&&&.....:&&&&:.   \r\n   .;xx;..:xxxxx;. ..xxxxx..  .+xxxx: .;xx ... +$&&&&.     :&$&.......$&$$.......X&&&.....x&&&&..   \r\n   .:Xx;...;xxXXx:..;XxXx:...:xxxxxx;..;XX .....&$&$$$    .X&$&.......&$$&......:$&$&.....;&&&x..   \r\n   ..Xx;....;XxxXXxxxXXXXX..;xXxXxx...:XXx .... .$$&&&X.. .&&$$ ......$$&$ .....:&&&&:....X&&&X..   \r\n   ..+Xx:....+XX+xxXXxXXXXXx+.      ..;xx.........x&$$&;...&&&x......:&$&:......;&$&&$&$&&&&&&;...  \r\n   ...xxx.....xxXxxxXxXxxxX+..:;;;;;. .++.........;&X&X;...&&&:......+&&&.......;&&&X+xxx$&&&&...   \r\n    ...xXx:....xxxxxXxxXxxx+..X&&&&&$ .:.........:$&&&X...x&$&:......$&&&.......x&&&......$&&$...   \r\n    .:..+xX+...:XxxXxxxxxXX+..x&&$$&$...........x&$$$;....x&&&:......X&&$.......+&&&......&&&x..    \r\n     ::..+xxX;..:XXxxXXXxxxx...&&$&$:..........$&$+.......$&&&:......&&&+.......$&&x......&&&:..    \r\n      .::..+Xx+..;XxxXxXXX+....&&$&&.........x&&X........ +&$X.......&&&;.......&&&;.....:&&&:..    \r\n        .;...:x+..;Xxxxxx:.....&&&&$.......:$$..............:;........X&;.......+&&;.....:&&&:..    \r\n          .::......+Xxx+...;..+$&$&+.....;&x:.....................................+.......:$$:..    \r\n             .;:.:..xX;.......+&&$$:..........x&&&&&&&&$$x+...;:............................:..     \r\n                 ....:.:;;;X..X&&$$:.........x&&&&x+xX$&&&&:..$&&&&&$&$XX+....++;::............     \r\n                  ....:.   ...X&&&&:.........x&$&x:...;&&&&...X&&&&x&$&&&&$..:$&$&&&&&&&&$$x:.      \r\n                   .X;.    ..:$&$$&:.........x&&$x....:&&&&:..$&$&;...x&&&&...$&&&&&X&&&&&&&;..     \r\n                          ...x&$&&X:.........x&&&x....:&&&&...X&$&;..:X&&&$...X&&&$:...+&&&$;..     \r\n                          ...x&$&&+.........+&$&&;....X&$&&...$&&X...:$$$&x...X&&$X....+&&&$;..     \r\n                          ...x&&$$+.........+&&&X+....$&&&X..:&&&x...:&$$$X...X&&&+....x&&&$:..     \r\n                          ..:X&&&$;.........+&&$&+...:&&&&X..x&&&x..x$&&&&...:&&&&+....x&&&$:..     \r\n                          ...X&&$X..........+&&&$:....&&&&;..X&&&&$$&&&&X....:&&&&+....x&&&X..      \r\n                          ..;$&&&X..........x&&&X....:&&&&:..x$&$$&$$$$x.....+&&&&+...;$&&&X..      \r\n                          ..+&&&&x.........:&&&&$....:&&&&...X$&&x..&&&X:....;&&&&;...;&&&&X..      \r\n                         ..:X&&&&x.........:$&&&$....+&&&&..:$&&$;..X&&&;....x&&&$;...;$&&&x.:      \r\n                         ...;$&&&&$$$$&$x..:X&&&$;;:;&&&&;..;$&&x....&&&+....X&&&X:...;&&&&;::      \r\n                          ...;$&&&&Xx+...:..x&&&&&&&&$&$&:..;$&&;....X&&&:..:$&&&X:...+&&&&::.      \r\n                           .:.;&x;...;+;::::..:;::;+xXXx;...:X&$:....:&&&+..:$&&&$;::X&&&X:::       \r\n                            ::...:;::.     :;;+;;:...:.::;&;..:&::xx;::X&&..;$&&&&&&&&&X::;:        \r\n                             .+:..               ..;;:.    ::;::::. :;;:.;.:::;;:;;+x+::;:.         \r\n                                                             :+++;    :;+;;++;;;;;;;;;;:            \r\n                                                                        .:.   ....:::               ");
            Console.ReadKey();
            sound.Play();
            Console.ReadKey();
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
            CreateFiles();
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
