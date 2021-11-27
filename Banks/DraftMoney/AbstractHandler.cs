using Banks.Models;

namespace Banks.DraftMoney
{
    public class AbstractHandler : IDraftMoneyHandler
    {
        private IDraftMoneyHandler _nextHandler;
        public IDraftMoneyHandler SetNext(IDraftMoneyHandler handler)
        {
            _nextHandler = handler;
            return handler;
        }

        public virtual Transaction Handle(Account account, int amount)
        {
            return _nextHandler?.Handle(account, amount);
        }
    }
}