using ReenBank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReenBank.Data.Repository.IRepository
{
    public interface IPersonRepository : IRepository<Person>
    {
        void Update (Person person);
    }
}
