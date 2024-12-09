

using System.Media;
using System.Security;
using System.Security.Cryptography.X509Certificates;

namespace TH_Bank
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            Bank.Intro();
            Bank.LogIn(new UserDataHandler());

        }
    }
}
