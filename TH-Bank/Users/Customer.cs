

namespace TH_Bank
{
    public class Customer : User
    {
        public List<Account> accounts;
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public override string UserType { get; } = "Customer";

        public AccountDataHandler dataHandler { get; set; }

        public Customer(string id, string passWord, string userName, string firstName, string lastName) : base(id, passWord, userName)
        {
            accounts = new List<Account>();
            FirstName = firstName;
            LastName = lastName;
            IsBlocked = false;
            dataHandler = new AccountDataHandler();
            userMenu = new CustomerMenu();
        }

        public override string ToString()
        {

            return $"{Id}|{UserName}|{PassWord}|{FirstName}|{LastName}|{UserType}|Blocked:{IsBlocked}";
        }

    }
}
