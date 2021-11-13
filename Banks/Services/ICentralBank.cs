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
        void CancelTransaction(Transaction transaction);
        void AddDays(int day);
    }
}