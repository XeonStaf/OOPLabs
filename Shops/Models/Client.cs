using Shops.Tools;

namespace Shops.Models
{
    public class Client
    {
        private int _money;

        public Client(string name, int startMoney)
        {
            Name = name;
            _money = startMoney;
        }

        public string Name { get; }

        public void DraftMoney(int amount)
        {
            if (_money < amount)
                throw new CompanyManagerException("Client has not enough money");
            _money -= amount;
        }
    }
}