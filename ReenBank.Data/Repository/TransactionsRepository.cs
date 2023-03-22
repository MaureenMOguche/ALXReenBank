using AlxReenBank.Data;
using Microsoft.EntityFrameworkCore;
using ReenBank.Data.Repository.IRepository;
using ReenBank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReenBank.Data.Repository
{
    public class TransactionsRepository : Repository<Transaction>, ITransactionsRepository
    {
        private readonly AlxReenBankContext _db;
        public TransactionsRepository(AlxReenBankContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Transaction transaction)
        {
            _db.Transactions.Update(transaction);
        }
    }
}
