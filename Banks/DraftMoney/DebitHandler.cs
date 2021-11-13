using System;
using Banks.Models;
using Banks.Tools;
using Banks.Various_Accounts;

namespace Banks.DraftMoney
{
    public class DebitHandler : AbstractHandler
    {
        public override Transaction Handle(Account account, int amount)
        {
            if (account is DebitAccount)
            {
                if (account.Balance < amount)
                    throw new CentralBankException("Client doesn't have enough money");
                var result = new Transaction(account, null, amount);
                account.Balance -= amount;
                return result;
            }
            else
            {
                return base.Handle(account, amount);
            }
        }
    }
}