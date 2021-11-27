using System;
using System.Collections.Generic;
using Banks.DraftMoney;
using Banks.Services;
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
            Transactions = new List<Transaction>();
            MonthsChanges = 0;
            Client = client;
            bank.CentralBank.DayPass += EveryDayTask;
            bank.CentralBank.MonthPass += MonthTask;
        }

        public double Balance { get; internal set; }
        public List<Transaction> Transactions { get; }
        public Client Client { get; }
        internal Guid Id { get; }
        protected double MonthsChanges { get; set; }
        protected SpecificBank Bank { get; }

        public virtual Transaction AdjustMoney(int amount)
        {
            var result = new Transaction(null, this, amount);
            Balance += amount;
            return result;
        }

        public virtual Transaction DraftMoney(int amount)
        {
            var debitHandler = new DebitHandler();
            var creditHandler = new CreditHandler();
            var savingHandler = new SavingHandler();
            savingHandler.SetNext(creditHandler).SetNext(debitHandler);
            return savingHandler.Handle(this, amount);
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