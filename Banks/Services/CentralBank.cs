using System;
using System.Collections.Generic;
using Banks.Models;
using Banks.Tools;

namespace Banks.Services
{
    public class CentralBank : ICentralBank
    {
        private readonly List<SpecificBank> _banks = new List<SpecificBank>();
        private DateTime _date = DateTime.Now;

        public delegate void DayPassHandler();
        public delegate void MonthPassHandler();
        public event DayPassHandler DayPass;
        public event MonthPassHandler MonthPass;
        public List<Client> RegisterClients { get; } = new List<Client>();
        public SpecificBank CreateBank(
            string name,
            int percent,
            int commission,
            Dictionary<int, double> depositPercent,
            int transferLimit = 1000)
        {
            var bank = new SpecificBank(name, percent, commission, this, depositPercent, transferLimit);
            _banks.Add(bank);
            return bank;
        }

        public void CancelTransaction(Transaction transaction)
        {
            if (transaction.Canceled)
                throw new CentralBankException("This transaction is already canceled");
            if (transaction.SourceAccount != null)
                transaction.SourceAccount.Balance += transaction.Amount;
            if (transaction.DestinationAccount != null)
                transaction.DestinationAccount.Balance -= transaction.Amount;
            transaction.Canceled = true;
        }

        public void AddDays(int day)
        {
            for (int i = 0; i < day; i++)
                DayPass.Invoke();
            if (_date.Month != _date.AddDays(day).Month)
                MonthPass.Invoke();
            _date.AddDays(day);
        }
    }
}