using System;
using System.Collections.Generic;
using Banks.Models;
using Banks.Tools;
using Banks.Various_Accounts;

namespace Banks.Services
{
    public class ConsoleAction : IAction
    {
        private readonly ICentralBank _centralBank;
        private readonly SpecificBank _bank;
        private Client _client = null;

        public ConsoleAction(ICentralBank centralBank, SpecificBank bank)
        {
            _centralBank = centralBank;
            _bank = bank;
            _bank.SendNotification += Notify;
        }

        public void StartListening()
        {
            Console.WriteLine("First - you need to register");
            Console.WriteLine("Enter QUIT to exit");
            RegisterClient();
            string command = Console.ReadLine();
            while (command != null && command != "QUIT")
            {
                switch (command)
                {
                   case "Update Personal Info":
                       UpdateClient();
                       break;
                   case "Draft Money":
                       DraftMoney();
                       break;
                   case "Adjust Money":
                       AdjustMoney();
                       break;
                   case "Add Account":
                       AddAccount();
                       break;
                   case "Transfer Money":
                       TransferMoney();
                       break;
                   default:
                       Console.WriteLine("Invalid Command. Try one more time");
                       break;
                }

                command = Console.ReadLine();
            }
        }

        public void Notify(string message)
        {
            Console.WriteLine($"[Notify] {message}");
        }

        private void RegisterClient()
        {
            var clientBuilder = new ClientBuilder();
            Console.WriteLine("Enter Your name");
            string name = Console.ReadLine();
            clientBuilder.SetName(name);
            Console.WriteLine("Enter your Passport (you can just press enter)");
            string passport = Console.ReadLine();
            clientBuilder.SetPassport(passport);
            Console.WriteLine("Enter your Address (you can just press enter)");
            string address = Console.ReadLine();
            clientBuilder.SetAddress(address);
            _client = clientBuilder.GetResult(_bank);
            _bank.AssignClient(_client);
            string verified = _client.Verified ? "Verified " : "UnVerified ";
            Console.WriteLine($"{name} you have {verified} account");
        }

        private void UpdateClient()
        {
            Console.WriteLine("Enter your Passport (you can just press enter)");
            string passport = Console.ReadLine();
            _client.UpdatePassport(passport);
            Console.WriteLine("Enter your Address (you can just press enter)");
            string address = Console.ReadLine();
            _client.UpdateAddress(address);
            string verified = _client.Verified ? "Verified " : "UnVerified ";
            Console.WriteLine($"{_client.Name} you have {verified} account");
        }

        private void DraftMoney()
        {
            Account chosenAccount = ChooseAccount("to draft");
            Console.WriteLine("Enter amount to Draft");
            int amount = int.Parse(Console.ReadLine() ?? string.Empty);
            try
            {
                chosenAccount.DraftMoney(amount);
            }
            catch (CentralBankException e)
            {
                Console.WriteLine($"[ERROR] {e.Message}");
            }

            PrintBalance(chosenAccount);
        }

        private void AdjustMoney()
        {
            Account chosenAccount = ChooseAccount("to adjust");
            Console.WriteLine("Enter amount to Adjust");
            int amount = int.Parse(Console.ReadLine() ?? string.Empty);
            chosenAccount.AdjustMoney(amount);
            PrintBalance(chosenAccount);
        }

        private void TransferMoney()
        {
            Account destination = ChooseAccount("destination");
            Account from = ChooseAccount("from");
            Console.WriteLine("Enter amount to Transfer");
            int amount = int.Parse(Console.ReadLine() ?? string.Empty);

            try
            {
                from.TransferMoney(destination, amount);
            }
            catch (CentralBankException e)
            {
                Console.WriteLine($"[ERROR] {e.Message}");
            }

            Console.WriteLine("Successfully!");
            PrintBalance(from);
        }

        private Account ChooseAccount(string message = "")
        {
            Console.WriteLine($"Choose account to {message}");
            _client.Accounts.ForEach(account =>
            {
                Console.WriteLine($"{account.Id}");
            });

            var chosenId = Guid.Parse(Console.ReadLine() ?? string.Empty);
            Account chosenAccount = _client.Accounts.Find(account => account.Id == chosenId);
            if (chosenAccount != null) return chosenAccount;
            Console.WriteLine("Invalid Account ID. Exiting");
            return null;
        }

        private void PrintBalance(Account account)
        {
            Console.WriteLine($"Your Balance is {account.Balance}");
        }

        private void AddAccount()
        {
            Console.WriteLine("Choose: ");
            Console.WriteLine("1. Debit Account");
            Console.WriteLine("2. Credit Account");
            Console.WriteLine("3. Saving Account");
            string answer = Console.ReadLine();
            Account newAccount = null;
            switch (answer)
            {
                case "1":
                    newAccount = _bank.CreateAccount("Debit", _client);
                    break;
                case "2":
                    newAccount = _bank.CreateAccount("Credit", _client);
                    break;
                case "3":
                    newAccount = _bank.CreateAccount("Saving", _client);
                    break;
                default:
                    Console.WriteLine("Invalid input. Exiting..");
                    break;
            }

            _bank.AssignAccount(_client, newAccount);
            Console.WriteLine("New Account has been created");
            Console.WriteLine($"You account number is {newAccount.Id}");
        }
    }
}