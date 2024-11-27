

namespace TH_Bank
{
    public class UserFactory
    {

        public User CreateUser(string id, string password, string username, string? fName, string? lName, string userInput)
        {

            User user = null;

            if(userInput == "Customer")
            {
                user = new Customer(id, password, fName, lName, username);
            }
            else if(userInput == "Admin")
            {
                user = new Admin(id, password, username);
            }
            else
            {
                throw new Exception($"Invalid user type, can't create {userInput}");
            }


            return user;
        }
    }
}
