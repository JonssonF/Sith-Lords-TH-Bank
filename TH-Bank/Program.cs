

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

            LogIn(new UserDataHandler());

            Console.ReadLine();
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
                "CUS00000000|112003|5000|SEK|SalaryAccount",
                "CUS00000000|223030|60000|SEK|SavingsAccount"
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
                "//ENDEuropean Euro//"
            };

            string[] defaultLoans = 
                {
                    "///INTERESTRATES///",
                    "CarLoan|0,08",
                    "MortgageLoan|0,05",
                    "///ENDINTERESTRATES///",
                    "CUS00000000|CarLoan|125000|0,1|2024-12-03 11:56:14",
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
            bool loginSuccess = false;
            int loginAttempts = 0;
            int maxAttempts = 3;
            string userName = "";
            string passWord = "";

            while (!userValidation)
            {
                SoundPlayer sound = new SoundPlayer(@"MzU4NDE2MzM1ODQ1MQ_tNMVHRnt0Rw.wav");
                sound.Play();
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine("                                                                                                  \r\n                         ..:                                                                        \r\n                        .:...                                                                       \r\n                       .+x+ ..                                                                      \r\n                  .. ..xxxx;..  .                                                                   \r\n             .:xXx: .;Xxxxxx;  ;.. .                                                                \r\n          .:+XxX+..:xXxxxxxxx:..xx+. .             .      ..                                        \r\n       ..:xxx+:...;xxxxxxxxxxx...xxx+..         .x&&&X$&X...                                        \r\n     . .+xXx:....+xxxxxxx+xxxxx. .;xxx:       .x$$$X$$$&;. .                                        \r\n      .+X++... .:xxxxxxxxxxxxxxx.. .x+X;  .  +&$&X....&$:..:;:...                                   \r\n     .+xx:.. ....;xXxXxxxxXxxxxx..   xxx.   ;&&$$x.  .x+.. .$&&&..;&&&&&&&X+;;.........    ...      \r\n    .;xX;...:;....+Xxxxxxx+xxx;...   .xxx.  :$&&X&;  ...  .:$&&$...xx$&&$&$$$X..;&&&&+....:XXx+..   \r\n   ..Xx+...;XX;.. .+XXxXxxxxx.....:.. :Xx;   +$&&&$:      .:&&$+ .....X&&&.......x&&$:....:&$&&...  \r\n   .:Xx:..xXxxx;  ..xxxxxxx;... :xx:...+xX   .+&&&&&..   . :&$$+ .....$$&&.......X&&&.....:&&&&:.   \r\n   .;xx;..:xxxxx;. ..xxxxx..  .+xxxx: .;xx ... +$&&&&.     :&$&.......$&$$.......X&&&.....x&&&&..   \r\n   .:Xx;...;xxXXx:..;XxXx:...:xxxxxx;..;XX .....&$&$$$    .X&$&.......&$$&......:$&$&.....;&&&x..   \r\n   ..Xx;....;XxxXXxxxXXXXX..;xXxXxx...:XXx .... .$$&&&X.. .&&$$ ......$$&$ .....:&&&&:....X&&&X..   \r\n   ..+Xx:....+XX+xxXXxXXXXXx+.      ..;xx.........x&$$&;...&&&x......:&$&:......;&$&&$&$&&&&&&;...  \r\n   ...xxx.....xxXxxxXxXxxxX+..:;;;;;. .++.........;&X&X;...&&&:......+&&&.......;&&&X+xxx$&&&&...   \r\n    ...xXx:....xxxxxXxxXxxx+..X&&&&&$ .:.........:$&&&X...x&$&:......$&&&.......x&&&......$&&$...   \r\n    .:..+xX+...:XxxXxxxxxXX+..x&&$$&$...........x&$$$;....x&&&:......X&&$.......+&&&......&&&x..    \r\n     ::..+xxX;..:XXxxXXXxxxx...&&$&$:..........$&$+.......$&&&:......&&&+.......$&&x......&&&:..    \r\n      .::..+Xx+..;XxxXxXXX+....&&$&&.........x&&X........ +&$X.......&&&;.......&&&;.....:&&&:..    \r\n        .;...:x+..;Xxxxxx:.....&&&&$.......:$$..............:;........X&;.......+&&;.....:&&&:..    \r\n          .::......+Xxx+...;..+$&$&+.....;&x:.....................................+.......:$$:..    \r\n             .;:.:..xX;.......+&&$$:..........x&&&&&&&&$$x+...;:............................:..     \r\n                 ....:.:;;;X..X&&$$:.........x&&&&x+xX$&&&&:..$&&&&&$&$XX+....++;::............     \r\n                  ....:.   ...X&&&&:.........x&$&x:...;&&&&...X&&&&x&$&&&&$..:$&$&&&&&&&&$$x:.      \r\n                   .X;.    ..:$&$$&:.........x&&$x....:&&&&:..$&$&;...x&&&&...$&&&&&X&&&&&&&;..     \r\n                          ...x&$&&X:.........x&&&x....:&&&&...X&$&;..:X&&&$...X&&&$:...+&&&$;..     \r\n                          ...x&$&&+.........+&$&&;....X&$&&...$&&X...:$$$&x...X&&$X....+&&&$;..     \r\n                          ...x&&$$+.........+&&&X+....$&&&X..:&&&x...:&$$$X...X&&&+....x&&&$:..     \r\n                          ..:X&&&$;.........+&&$&+...:&&&&X..x&&&x..x$&&&&...:&&&&+....x&&&$:..     \r\n                          ...X&&$X..........+&&&$:....&&&&;..X&&&&$$&&&&X....:&&&&+....x&&&X..      \r\n                          ..;$&&&X..........x&&&X....:&&&&:..x$&$$&$$$$x.....+&&&&+...;$&&&X..      \r\n                          ..+&&&&x.........:&&&&$....:&&&&...X$&&x..&&&X:....;&&&&;...;&&&&X..      \r\n                         ..:X&&&&x.........:$&&&$....+&&&&..:$&&$;..X&&&;....x&&&$;...;$&&&x.:      \r\n                         ...;$&&&&$$$$&$x..:X&&&$;;:;&&&&;..;$&&x....&&&+....X&&&X:...;&&&&;::      \r\n                          ...;$&&&&Xx+...:..x&&&&&&&&$&$&:..;$&&;....X&&&:..:$&&&X:...+&&&&::.      \r\n                           .:.;&x;...;+;::::..:;::;+xXXx;...:X&$:....:&&&+..:$&&&$;::X&&&X:::       \r\n                            ::...:;::.     :;;+;;:...:.::;&;..:&::xx;::X&&..;$&&&&&&&&&X::;:        \r\n                             .+:..               ..;;:.    ::;::::. :;;:.;.:::;;:;;+x+::;:.         \r\n                                                             :+++;    :;+;;++;;;;;;;;;;:            \r\n                                                                        .:.   ....:::               ");
                Console.ReadKey();
                sound.Stop();
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
                Console.WriteLine($"{userName} please type in your password.");
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
