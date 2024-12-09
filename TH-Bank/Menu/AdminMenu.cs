

using System.Diagnostics;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;

namespace TH_Bank
{
    public class AdminMenu : Menu
    {
        UserFacade userFacade;
        public AdminMenu()
        {
            userFacade = new UserFacade();
            _menu = new string[] // Menu in array = easy to add options.
        {
            "1. Add a new user to system.",
            "2. Handle suspended customers.",
            "3. Change currency exchange rates.",
            "4. Logout.",
            "5. Exit program.",
        };
            menuWidth = CalculateWidth(extraWidth: 10);
        }



        public override void MenuChoices()
        {
            optionCount = _menu.Length; // Combined with Choice method from MenuClass wrongful inputs can't be made.
            access = true;

            // Moves directly to currency review if deadline of 24 hours since last update has passed)
            if (TimeToReview())
            {
                Console.Beep();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n -- WARNING! Currency exchange rates must be reviewed immediately! --\n");
                Console.ForegroundColor = ConsoleColor.Gray;
                UpdateExchangeRates();
            }

            int adminChoice = Format.Choice(optionCount); 
                switch (adminChoice)
                {

                    case 1:
                        // Add new customer. 
                        userFacade.CreateUser();
                        break;

                    case 2:
                        // Unblock user.
                        userFacade.UnblockUser();
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

        public void UpdateExchangeRates()
        {
            Console.WriteLine("Current exchange rates:");
            ExchangeCurrency.ViewAllRates();
            Console.WriteLine($"Currencies were last approved {ExchangeCurrency.GetLastReview().ToString()}.");
            Console.WriteLine("--------------");
            Console.WriteLine("1. Approve current rates");
            Console.WriteLine("2. Edit rates");
            Console.WriteLine("3. Back to menu");
            int appOrChange = Format.Choice(3);
            bool editing = false;

            if (appOrChange == 1)
            {
                ExchangeCurrency.Review(DateTime.Now);
                Console.WriteLine("Current exchange rates have been approved!");
            }
            else if(appOrChange == 2)
            {
                editing = true;
            }
            else
            {

            }

            while(editing)
            {
            Console.WriteLine("Chooce a currency to edit:");
            int loop = 1;

            // Shows all currencies available
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

            // looks through eschange rates for chosen currency, saves in temporary list for
            // easier access 
            foreach (var item in chosen.ExchangeRates)
            {
                Console.WriteLine($"{loop}. {item.Key}: {item.Value}");
                keys.Add(item.Key);
                loop++;
                
            }
            choice = Format.Choice(chosen.ExchangeRates.Count) - 1;

            Console.WriteLine($"Enter new rate for {chosen.NameShort} -> {keys[choice]} (current: {chosen.ExchangeRates[keys[choice]]})");
            
                // Uses "keys" list to set rate for correct currency
            chosen.ExchangeRates[keys[choice]] = (double)Format.DecimalInput();
            Console.WriteLine($"New rate for {chosen.NameShort} -> {keys[choice]}: {chosen.ExchangeRates[keys[choice]]} ");
            xdh.Save(chosen);
                Console.WriteLine("Do you wish to edit another rate?");
                Console.WriteLine("1. Yes");
                Console.WriteLine("2. No");
                int again = Format.Choice(2);
                if(again == 1)
                {
                    Console.Clear();
                }
                else
                {
                    // Sets last review time to current time when done editing rates.
                    ExchangeCurrency.Review(DateTime.Now);
                    Console.WriteLine("Currency review is done");
                    editing = false;
                }
            
            }
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
