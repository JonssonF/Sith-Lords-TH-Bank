

namespace TH_Bank
{
    public abstract class UserFactory
    {
        public abstract User MakeUser(UserDataHandler userDataHandler);

        public User CreateUser()
        {
            return MakeUser(new UserDataHandler());
        }
    }
}
