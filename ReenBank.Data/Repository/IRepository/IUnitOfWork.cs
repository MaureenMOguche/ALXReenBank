using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReenBank.Data.Repository.IRepository
{
    public interface IUnitOfWork
    {
        public IPersonRepository PersonRepo { get; }
        public IBankAccountRepository BankAccountRepo { get; }
        public ITransactionsRepository TransactionsRepo { get; }



        void Save();

    }
}
