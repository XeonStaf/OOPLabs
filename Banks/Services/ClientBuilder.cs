using Banks.Models;

namespace Banks.Services
{
    public class ClientBuilder
    {
        private Client client = new Client();

        public ClientBuilder SetName(string name)
        {
            client.Name = name;
            return this;
        }

        public ClientBuilder SetAddress(string address)
        {
            client.Address = address;
            return this;
        }

        public ClientBuilder SetPassport(string passport)
        {
            client.Passport = passport;
            return this;
        }

        public void Reset()
        {
            client = new Client();
        }

        public Client GetResult(SpecificBank bank)
        {
            Client result = client;
            bank.Clients.Add(result);
            bank.CentralBank.RegisterClients.Add(result);
            Reset();
            return result;
        }
    }
}