using Banks.Models;
using Banks.Various_Accounts;

namespace Banks.DraftMoney
{
    public class CreditHandler : AbstractHandler
    {
        public override Transaction Handle(Account account, int amount)
        {
            if (account is CreditAccount)
            {
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