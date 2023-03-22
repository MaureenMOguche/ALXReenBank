using AlxReenBank.Data;
using ReenBank.Data.Repository.IRepository;
using ReenBank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReenBank.Data.Repository
{
    public class PersonRepository : Repository<Person>, IPersonRepository
    {
        private readonly AlxReenBankContext _db;
        public PersonRepository(AlxReenBankContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Person person)
        {
            _db.People.Update(person);
        }
    }
}
