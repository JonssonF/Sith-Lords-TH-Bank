namespace TH_Bank
{
    public sealed class ActiveUserSingleton
    {
        private static ActiveUserSingleton _instance = null;

        public User LoggedInUser;
        private ActiveUserSingleton(User user)
        {
            LoggedInUser = user;
        }
        public static ActiveUserSingleton GetInstance(User loginUser)
        {
            if (_instance == null)
            {
                _instance = new ActiveUserSingleton(loginUser);
            }
            
                return _instance;
        }

        public static ActiveUserSingleton GetInstance()
        {
            return _instance;
        }

        
    }
}
