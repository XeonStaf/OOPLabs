using System;
using Banks.Models;
using Banks.Tools;
using Banks.Various_Accounts;

namespace Banks.DraftMoney
{
    public class SavingHandler : AbstractHandler
    {
        public override Transaction Handle(Account account, int amount)
        {
            if (account is SavingAccount)
            {
                throw new CentralBankException("You can't draft money from Saving Account");
            }
            else
            {
                return base.Handle(account, amount);
            }
        }
    }
}