

namespace TH_Bank
{
    public class UserFactory
    {

        public User CreateUser(string id, string username, string password, string? fName, string? lName, string userInput)
        {

            User user = null;

            if(userInput == "Customer")
            {
                user = new Customer(id, username, password, fName, lName);
            }
            else if(userInput == "Admin")
            {
                user = new Admin(id, username, password);
            }
            else
            {
                throw new Exception($"Invalid user type, can't create {userInput}");
            }


            return user;
        }
    }
}
