using System;
using System.Collections.Generic;
using Banks.Tools;

namespace Banks.Models
{
    public abstract class Account
    {
        protected Account(int startBalance, SpecificBank bank, Client client)
        {
            Bank = bank;
            Id = Guid.NewGuid();
            Balance = startBalance;
            Client = client;
            Transactions = new List<Transaction>();
            MonthsChanges = 0;
            bank.CentralBank.DayPass += EveryDayTask;
            bank.CentralBank.MonthPass += MonthTask;
        }

        public double Balance { get; internal set; }
        public List<Transaction> Transactions { get; }
        internal Guid Id { get; }
        protected double MonthsChanges { get; set; }
        protected Client Client { get; }
        protected SpecificBank Bank { get; }

        public virtual Transaction AdjustMoney(int amount)
        {
            var result = new Transaction(null, this, amount);
            Balance += amount;
            return result;
        }

        public virtual Transaction DraftMoney(int amount)
        {
            if (Balance < amount)
                throw new CentralBankException("Client doesn't have enough money");
            var result = new Transaction(this, null, amount);
            Balance -= amount;
            return result;
        }

        public virtual Transaction TransferMoney(Account destination, int amount)
        {
            if (Client.Verified && Bank.TransferLimit > amount)
                throw new CentralBankException("Clients should be verified");
            if (Balance < amount)
                throw new CentralBankException("Client doesn't have enough money");
            var result = new Transaction(this, destination, amount);
            Balance -= amount;
            destination.Balance += amount;
            return result;
        }

        public virtual void EveryDayTask()
        {
            return;
        }

        public void MonthTask()
        {
            Balance += MonthsChanges;
            MonthsChanges = 0;
        }
    }
}