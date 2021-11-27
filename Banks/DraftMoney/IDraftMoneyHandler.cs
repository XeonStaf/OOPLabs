using Banks.Models;

namespace Banks.DraftMoney
{
    public interface IDraftMoneyHandler
    {
        IDraftMoneyHandler SetNext(IDraftMoneyHandler handler);
        Transaction Handle(Account account, int amount);
    }
}