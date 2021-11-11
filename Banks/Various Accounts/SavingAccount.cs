using System;
using System.Collections.Generic;
using System.Linq;
using Banks.Models;
using Banks.Tools;

namespace Banks.Various_Accounts
{
    public class SavingAccount : Account
    {
        public SavingAccount(int startBalance, SpecificBank bank, Client client)
            : base(startBalance, bank, client)
        {
        }

        public override Transaction DraftMoney(int amount)
        {
            throw new CentralBankException("You can't draft money from Saving Account");
        }

        public override void EveryDayTask()
        {
            KeyValuePair<int, double> percent =
                Bank.DepositPercent.OrderBy(e => Math.Abs(e.Key - Balance)).FirstOrDefault();
            double everyDayPercent = percent.Value / 365;
            MonthsChanges += Balance * everyDayPercent;
            base.EveryDayTask();
        }
    }
}