using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReenBank.Models.ViewModels
{
    public class PersonAccountTransactionVM
    {
        public Person Person { get; set; }
        public IEnumerable<Transaction> TransactionList { get; set; }
        public BankAccount BankAccount { get; set; }

        public double AmountDeposit { get; set; }


    }
}
