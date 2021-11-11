using System;
using System.Collections.Generic;
using Banks.Models;
using Banks.Services;
using Banks.Tools;
using Banks.Various_Accounts;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Banks.Tests
{
    public class Tests
    {
        private ICentralBank _centralBank;
        private SpecificBank _bank1;
        private SpecificBank _bank2;
        private Client _client;
        private Account _account;

        [SetUp]
        public void Setup()
        {
            _centralBank = new CentralBank();
            var depositPercent = new Dictionary<int, double>
            {
                [50000] = 3.65,
                [100000] = 7.3
            };
            _bank1 = _centralBank.CreateBank("SberBank", 4, 50, depositPercent);
            var depositPercent2 = new Dictionary<int, double>
            {
                [50000] = 4,
                [100000] = 8
            };
            _bank2 = _centralBank.CreateBank("Tinkoff", 5, 10, depositPercent2);
            _client = _centralBank.RegisterClient("Ivan", "31", "Puskina");
            _bank1.AssignClient(_client);
            _account = new DebitAccount(2000, _bank1, _client);
            _bank1.AssignAccount(_client, _account);

        }

        [Test]
        public void DraftMoney_CancelTransaction_BalanceTheSame()
        {
            Transaction transaction = _account.DraftMoney(1000);
            Assert.AreEqual(1000, _account.Balance);
            _centralBank.CancelTransaction(transaction);
            Assert.AreEqual(2000, _account.Balance);
        }
        
        [Test]
        public void AdjustMoney_CancelTransaction_BalanceTheSame()
        {
            Transaction transaction = _account.AdjustMoney(1000);
            Assert.AreEqual(3000, _account.Balance);
            _centralBank.CancelTransaction(transaction);
            Assert.AreEqual(2000, _account.Balance);
        }

        [Test]
        public void CreditAccountDraftBelow0_CheckCommission()
        {
            var creditAccount = new CreditAccount(500, _bank1, _client);
            creditAccount.DraftMoney(1000);
            creditAccount.EveryDayTask();
            creditAccount.MonthTask();
            Assert.AreEqual(-550, creditAccount.Balance);
        }
        
        [Test]
        public void DraftMoneyFromDebit_NotEnoughMoney()
        {
            Assert.Catch<CentralBankException>(() =>
            {
                _account.DraftMoney(10000);
            });
        }
        
        [Test]
        public void DraftMoneyFromSavingAccount_NotAllowed()
        {
            var savingAccount = new SavingAccount(10000, _bank1, _client);
            Assert.Catch<CentralBankException>(() =>
            {
                savingAccount.DraftMoney(6000);
            });
        }

        [Test]
        public void MonthsPassAdjustPercentToSavingAccount_CheckBalance()
        {
            var savingAccount = new SavingAccount(55000, _bank1, _client);
            _bank1.AssignAccount(_client, savingAccount);
            var savingAccount2 = new SavingAccount(150000, _bank1, _client);
            _bank1.AssignAccount(_client, savingAccount2);
            for (int i = 0; i < 30; i++)
            {
                savingAccount.EveryDayTask();
                savingAccount2.EveryDayTask();
            }
                
            savingAccount.MonthTask();
            savingAccount2.MonthTask();
            Assert.AreEqual(71500 ,savingAccount.Balance);
            Assert.AreEqual(240000 ,savingAccount2.Balance);
        }

        [Test]
        public void MonthsPass_CheckBalanceChanges()
        {
            var savingAccount = new SavingAccount(55000, _bank1, _client);
            _bank1.AssignAccount(_client, savingAccount);
            var savingAccount2 = new SavingAccount(150000, _bank1, _client);
            _bank1.AssignAccount(_client, savingAccount2);
            _centralBank.AddDays(30);
            Assert.AreEqual(71500 ,savingAccount.Balance);
            Assert.AreEqual(240000 ,savingAccount2.Balance);
        }
        
    }
}