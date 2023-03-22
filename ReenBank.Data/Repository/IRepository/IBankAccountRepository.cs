using ReenBank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReenBank.Data.Repository.IRepository
{
    public interface IBankAccountRepository : IRepository<BankAccount>
    {
        void Update(BankAccount bankAccount);

        string Deposit(double amount, BankAccount account);
        string Transfer(double amount, BankAccount sender, String receiverAccNumber);

        BankAccount SearchAccount(string accountNum);
    }

}
