using Banks.Models;
using Banks.Tools;

namespace Banks.Various_Accounts
{
    public class DebitAccount : Account
    {
        public DebitAccount(int startBalance, SpecificBank bank, Client client)
            : base(startBalance, bank, client)
        {
        }

        public override void EveryDayTask()
        {
            double percentEveryDay = Bank.Percent / 365;
            MonthsChanges += Balance * percentEveryDay;
            base.EveryDayTask();
        }
    }
}