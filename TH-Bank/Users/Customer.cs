

namespace TH_Bank
{
    public class Customer : User
    {
        public List<Account> accounts;
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public override string UserType { get; } = "Customer";

        public Customer(string id, string userName, string passWord, string firstName, string lastName) : base(id, userName, passWord)
        {
            accounts = new List<Account>();
            FirstName = firstName;
            LastName = lastName;
            IsBlocked = false;
        }

        public override string ToString()
        {

            return $"{Id}|{UserName}|{PassWord}|{FirstName}|{LastName}|{UserType}|Blocked:{IsBlocked}";
        }

    }
}
