using System.Collections.Generic;

namespace Banks.Models
{
    public class Client
    {
        private string _passport;
        private string _address;

        public Client(string name, string passport = "", string address = "")
        {
            Name = name;
            _passport = passport;
            _address = address;
            Accounts = new List<Account>();
            CheckVerify();
        }

        public string Name { get; }
        public List<Account> Accounts { get; }
        public bool Verified { get; set; }

        public bool CheckVerify()
        {
            if (string.IsNullOrEmpty(_passport) || string.IsNullOrEmpty(_address))
            {
                Verified = false;
                return Verified;
            }

            Verified = true;
            return Verified;
        }

        public void UpdatePassport(string passport)
        {
            _passport = passport;
            CheckVerify();
        }

        public void UpdateAddress(string address)
        {
            _address = address;
            CheckVerify();
        }
    }
}