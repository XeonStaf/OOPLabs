using System.Collections.Generic;
using Banks.Models;

namespace Banks.Services
{
    public interface ICentralBank
    {
        public SpecificBank CreateBank(
            string name,
            int percent,
            int commission,
            Dictionary<int, double> depositPercent,
            int transferLimit = 1000);
        Client RegisterClient(string name, string passport = "", string address = "");
        void CancelTransaction(Transaction transaction);
        void AddDays(int day);
    }
}