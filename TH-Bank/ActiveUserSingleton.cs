namespace TH_Bank
{
    public sealed class ActiveUserSingleton : User
    {
        public override string UserType => throw new NotImplementedException();

        private static User? _instance;

        public override string ToString()
        {
            return null;
        }
        private ActiveUserSingleton(string id, string passWord, string userName) : base(id, passWord, userName)
        {

        }
        public static User SetInstance(User user)
        {
            if (_instance == null)
            {
                _instance = user;
            }
            return _instance;
        }

        public static void Reset()
        {
            _instance = null;
        }

        public static User GetInstance()
        {
            return _instance;
        } 
    }
}
