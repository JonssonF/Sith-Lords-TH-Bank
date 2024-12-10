

using System.Media;
using System.Security;
using System.Security.Cryptography.X509Certificates;

namespace TH_Bank
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Bank.Intro(); // Launches intro of program.
            Bank.CreateFiles(); // Creating files for first time launch.
            Bank.LogIn(new UserDataHandler());
        }
    }
}
