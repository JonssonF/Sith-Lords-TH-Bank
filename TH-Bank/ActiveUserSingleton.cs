namespace TH_Bank
{
    public sealed class ActiveUserSingleton : User
    {
        public override string UserType { get; }

        private static User? _instance;

        public override string ToString()
        {
            return _instance.ToString();
        }

        // private constructor makes sure we can't create multiple
        // active user objects
        private ActiveUserSingleton(string id, string passWord, string userName) : base(id, passWord, userName)
        {

        }

        // set instance creates a new instance of active user if it's null,
        // otherwise returns the instance instead.
        public static User SetInstance(User user)
        {
            if (_instance == null)
            {
                _instance = user;
            }
            return _instance;
        }

        // resets instance, to be used in logging out
        public static void Reset()
        {
            _instance = null;
        }

        // Returns the active user instance
        public static User GetInstance()
        {
            return _instance;
        } 
    }
}
