using System.Collections.Generic;
using Banks.Models;
using Banks.Services;

namespace Banks
{
    internal static class Program
    {
        private static void Main()
        {
            CentralBank centralBank = new CentralBank();
            var depositPercent = new Dictionary<int, double>
            {
                [50000] = 3.65,
                [100000] = 7.3,
            };
            SpecificBank bank = centralBank.CreateBank("SberBank", 4, 50, depositPercent);
            IAction consoleInterface = new ConsoleAction(centralBank, bank);
            consoleInterface.StartListening();
        }
    }
}
