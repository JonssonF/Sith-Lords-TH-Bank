﻿using System.Security.Cryptography.X509Certificates;


namespace TH_Bank
{
    public class UserDataHandler : IAggregateDataHandler<User>
    {
        public string FilePath { get; set; }
        public UserDataHandler()
        {
            FilePath = FilePaths.UserPath;
        }
        public void Delete(User deleteThis)
        {
            string[] openFile = File.ReadAllLines(FilePath);

            // Finds the row that contains the account to be deleted.
            int deleterow = Array.IndexOf(openFile, deleteThis.ToString());
            // Loops trough users starting at deleted row, and shifts them one to the left,
            // writing over the deleted account info.
            for (int i = deleterow + 1; i < openFile.Length; i++)
            {
                openFile[i - 1] = openFile[i];
            }

            File.WriteAllLines(FilePath, openFile);

        }

        public User Load(string userid)
        {
            string[] openFile = File.ReadAllLines(FilePath);

            foreach (string line in openFile)
            {
                if (line.Contains(userid) && line.Contains("Customer"))
                {
                    string[] variables = line.Split('|');
                    string id = variables[0];
                    string username = variables[1];
                    string password = variables[2];
                    string firstname = variables[3];
                    string lastname = variables[4];
                    var customer = new Customer(id, username, password, firstname, lastname);
                    return customer;
                }
                else if (line.Contains(userid) && line.Contains("Admin"))
                {
                    string[] variables = line.Split('|');
                    string id = variables[0];
                    string username = variables[1];
                    string password = variables[2];
                    
                    
                    var admin = new Admin(id, username, password);

                    return admin;
                }
            }

            throw new Exception("Invalid User Type");
        }

        public List<User> LoadAll()
        {
            string[] openFile = File.ReadAllLines(FilePath);

            var allusers = new List<User>();

            foreach (string line in openFile)
            {
                if (line.Contains("Customer"))
                {
                    string[] variables = line.Split('|');
                    string id = variables[0];
                    string username = variables[1];
                    string password = variables[2];
                    string firstname = variables[3];
                    string lastname = variables[4];

                    var customer = new Customer(id, username, password, firstname, lastname);

                    if(line.Contains("Blocked:True"))
                    { 
                        customer.IsBlocked = true;  
                    }

                    allusers.Add(customer);
                }
            }
            return allusers;
        }

        public void Save(User saveThis)
        {
            string[] openFile = File.ReadAllLines(FilePath);

            bool userExists = false;

            foreach(string line in openFile)
            {
                if(line.Contains(saveThis.UserName))
                {
                    userExists = true;
                }
            }

            if (!userExists)
            {
                openFile = openFile.Append(saveThis.ToString()).ToArray();
            }
            else
            {
                int overwrite = -1;
                foreach (string line in openFile)
                {
                    if (line.Contains(saveThis.UserName))
                    {
                        overwrite = Array.IndexOf(openFile, line);
                    }
                }
                
                openFile[overwrite] = saveThis.ToString();
            }

            File.WriteAllLines(FilePath, openFile);
        }

        public void SaveAll(List<User> saveList)
        {
            throw new NotImplementedException();
        }

        // Checks if password matches user
        public bool PasswordCheck(string username, string password)
        {
            string[] openFile = File.ReadAllLines(FilePath);
            int deleterow = 0;
            for (int i = deleterow + 1; i < openFile.Length; i++)
            {
                openFile[i - 1] = openFile[i];
            }

            foreach (string s in openFile)
            {
                if (s.Contains(password) && s.Contains(username))
                {
                    return true;
                }
            }
            return false;
        }

        // Checks if user is blocked
        public bool BlockCheck(string username)
        {
            string[] openFile = File.ReadAllLines(FilePath);

            foreach (string s in openFile)
            {
                if (s.Contains(username) && s.Contains("Blocked:True"))
                {
                    return false;
                }
            }
            return true;
        }

        // Checks if user exists
        public bool Exists(string username)
        {
            string[] openFile = File.ReadAllLines(FilePath);
            int deleterow = 0;
            for (int i = deleterow + 1; i < openFile.Length; i++)
            {
                openFile[i - 1] = openFile[i];
            }

            foreach (string s in openFile)
            {
                    string[] usrcheck = s.Split('|');

                    if (usrcheck[1] == username)
                    {
                        return true;
                    }
            }
            return false;
        }
    }
}
