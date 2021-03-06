using Banks.Models;

namespace Banks.Various_Accounts
{
    public class CreditAccount : Account
    {
        public CreditAccount(int startBalance, SpecificBank bank, Client client)
            : base(startBalance, bank, client)
        {
        }

        public override void EveryDayTask()
        {
            if (Balance < 0)
                MonthsChanges -= Bank.Commission;
            base.EveryDayTask();
        }
    }
}