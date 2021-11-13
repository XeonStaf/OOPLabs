using System;
using System.Collections.Generic;
using Banks.Services;
using Banks.Tools;
using Banks.Various_Accounts;

namespace Banks.Models
{
    public class SpecificBank
    {
        public SpecificBank(
            string name,
            int percent,
            int commission,
            CentralBank centralBank,
            Dictionary<int, double> depositPercent = null,
            int transferLimit = 1000)
        {
            Percent = percent;
            Commission = commission;
            DepositPercent = new Dictionary<int, double>();
            if (depositPercent != null)
                DepositPercent = depositPercent;
            TransferLimit = transferLimit;
            Name = name;
            Clients = new List<Client>();
            CentralBank = centralBank;
        }

        public delegate void SendNotificationHandler(string message);
        public event SendNotificationHandler SendNotification;

        public string Name { get; }
        public int TransferLimit { get; set; }
        public Dictionary<int, double> DepositPercent { get; }
        public int Percent { get; private set; }
        public int Commission { get; private set; }
        internal CentralBank CentralBank { get; }
        internal List<Client> Clients { get; }

        public void ChangePercent(int newPercent)
        {
            Percent = newPercent;
            SendNotification?.Invoke($"New Percent in bank {Name} is {Percent}");
        }

        public void ChangeCommission(int newCommission)
        {
            Commission = newCommission;
            SendNotification?.Invoke($"New Commision in bank {Name} is {Commission}");
        }

        public void AssignClient(Client client)
        {
            Clients.Add(client);
        }

        public void AssignAccount(Client client, Account account)
        {
            if (!Clients.Contains(client))
                throw new CentralBankException("Client is not assigned with this bank");
            client.Accounts.Add(account);
        }

        public Account CreateAccount(string type, Client client, int startBalance = 0)
        {
            Account newAccount = type switch
            {
                "Debit" => new DebitAccount(startBalance, this, client),
                "Saving" => new SavingAccount(startBalance, this, client),
                "Credit" => new CreditAccount(startBalance, this, client),
                _ => throw new CentralBankException("Incorrect type")
            };

            return newAccount;
        }
    }
}