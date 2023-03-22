using ReenBank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReenBank.Data.Repository.IRepository
{
    public interface ITransactionsRepository : IRepository<Transaction>
    {
        void Update(Transaction transaction);
    }
}
