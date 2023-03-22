using AlxReenBank.Data;
using ReenBank.Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReenBank.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AlxReenBankContext _db;
        public UnitOfWork(AlxReenBankContext db)
        {
            _db = db;
            PersonRepo = new PersonRepository(db);
            BankAccountRepo = new BankAccountRepository(db);
            TransactionsRepo = new TransactionsRepository(db);
        }
        public IPersonRepository PersonRepo { get; private set; }

        public IBankAccountRepository BankAccountRepo { get; private set; }

        public ITransactionsRepository TransactionsRepo { get; private set; }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
