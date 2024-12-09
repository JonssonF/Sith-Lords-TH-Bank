using System.Globalization;
using System.Media;

namespace TH_Bank
{
    public class Format
    {
        public static int Choice(int max) // Method to only accept valid keypress.
        {
            bool isNumber;
            int num = 0;
            do
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                // Checks if the choice is a number
                // If it is, converts keypress
                // into an int. 
                isNumber = Char.IsAsciiDigit(key.KeyChar);
                if (isNumber)
                {
                    num = Convert.ToInt32(key.KeyChar.ToString());
                }

            } while (!isNumber || num > max || num == 0); // exits while loop only if number is not bigger
                                              // than the max index of the active users accounts.
            return num;
        }

        public static decimal DecimalInput()   //Method to only accept valid keypress
        {
            bool isNumber = false;
            string numberString = "";
            decimal number;
            ConsoleKeyInfo cki;

            do
            {   // Takes input and checks if it is a number
                cki = Console.ReadKey(true);
                isNumber = decimal.TryParse(cki.KeyChar.ToString(), out decimal check);

                if (cki.Key != ConsoleKey.Backspace)
                {
                    if (isNumber) //if it is a number, adds to string of numbers 
                    {
                        // Makes sure user can only type two digits after entering a comma
                        if (numberString.Contains(",") && numberString.IndexOf(",") < numberString.Length - 2)
                        {
                            // do nothing
                        }
                        else
                        {
                            numberString += cki.KeyChar;
                            Console.Write(cki.KeyChar);
                        }
                    }
                    else if (!numberString.Contains(",") && cki.Key == ConsoleKey.OemComma)
                    { // if key pressed is comma and there is no comma in string, adds a comma.
                        numberString += cki.KeyChar;
                        Console.Write(cki.KeyChar);
                    }
                } // Backspace key delete characters from the string
                else if (cki.Key == ConsoleKey.Backspace && numberString.Length > 0)
                {
                    numberString = numberString.Substring(0, (numberString.Length - 1));
                    Console.Write("\b \b");
                }
                // Exits while loop when user presses enter after entering at least 1 number
            } while (cki.Key != ConsoleKey.Enter || numberString.Length == 0);

            Console.WriteLine();

            // The string is converted to double and returned.
            // CurrentCulture used to make sure Ören are represented correctly.
            decimal.TryParse(numberString, CultureInfo.CurrentCulture, out number);

            return number;
        }


        public static int IntegerInput(int numberOfDigits) // Method to only accept valid keypress
        {
            bool isNumber = false;
            string numberString = "";
            int number;
            ConsoleKeyInfo cki;

            do
            {   // Takes input and checks if it is a number
                cki = Console.ReadKey(true);
                isNumber = int.TryParse(cki.KeyChar.ToString(), out int check);

                if (cki.Key != ConsoleKey.Backspace)
                {
                    if (isNumber) //if it is a number, adds to string of numbers 
                    {
                        if (numberString.Length < numberOfDigits)
                        {
                            numberString += cki.KeyChar;
                            Console.Write(cki.KeyChar);
                        }
                    }
                } // Backspace key delete characters from the string
                else if (cki.Key == ConsoleKey.Backspace && numberString.Length > 0)
                {
                    numberString = numberString.Substring(0, (numberString.Length - 1));
                    Console.Write("\b \b");
                }
                // Exits while loop when user presses enter after entering at least 1 number
            } while (cki.Key != ConsoleKey.Enter || numberString.Length == 0);

            Console.WriteLine();

            // The string is converted to int and returned.
            int.TryParse(numberString, out number);
            return number;
        }

       

        private static Random randaccount = new Random();
        public static int UniqueAccountNo(string account)
        {
            string choice = account == "SalaryAccount" ? "11" : "22";
            int rndAcc = randaccount.Next(1000, 10000);
            string accountNumber = choice + rndAcc.ToString();
            return int.Parse(accountNumber);
        }

    
    public static string StringMinimumInput(int minimumInput) // Method to only accept valid keypress
    {
        bool isNumber = false;
        string toReturn = "";
        ConsoleKeyInfo cki;
        char character;

        do
        {   // Takes input 
            cki = Console.ReadKey(true);
            character = cki.KeyChar;

            if (cki.Key != ConsoleKey.Backspace && cki.Key != ConsoleKey.Enter)
            {
                        toReturn += cki.KeyChar;
                        Console.Write(cki.KeyChar); 
            } // Backspace key delete characters from the string
            else if (cki.Key == ConsoleKey.Backspace && toReturn.Length > 0)
            {
                toReturn = toReturn.Substring(0, (toReturn.Length - 1));
                Console.Write("\b \b");
            }
 
                
            // Exits while loop when user presses enter after entering at least x characters
        } while (cki.Key != ConsoleKey.Enter || toReturn.Length < minimumInput);

        Console.WriteLine();


        return toReturn;
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

    }
}
