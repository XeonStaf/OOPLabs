using System;

namespace Banks.Models
{
    public class Transaction
    {
        public Transaction(Account sourceAccount, Account destinationAccount, int amount)
        {
            Id = Guid.NewGuid();
            SourceAccount = sourceAccount;
            DestinationAccount = destinationAccount;
            Amount = amount;
            Canceled = false;
        }

        public Guid Id { get; }
        public Account SourceAccount { get; }
        public Account DestinationAccount { get; }
        public int Amount { get; }
        public bool Canceled { get; set; }
    }
}