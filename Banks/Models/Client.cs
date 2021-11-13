using System.Collections.Generic;
using Banks.Services;

namespace Banks.Models
{
    public class Client
    {
        public string Passport { get; internal set; }
        public string Address { get; internal set; }
        public string Name { get; internal set; }
        public List<Account> Accounts { get; } = new List<Account>();
        public bool Verified { get; private set; }

        public bool CheckVerify()
        {
            if (string.IsNullOrEmpty(Passport) || string.IsNullOrEmpty(Address))
            {
                Verified = false;
                return Verified;
            }

            Verified = true;
            return Verified;
        }

        public void UpdatePassport(string passport)
        {
            Passport = passport;
            CheckVerify();
        }

        public void UpdateAddress(string address)
        {
            Address = address;
            CheckVerify();
        }
    }
}