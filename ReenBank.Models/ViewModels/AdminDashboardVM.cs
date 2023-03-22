using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReenBank.Models.ViewModels
{
    public class AdminDashboardVM
    {
        public IEnumerable<Person> PeopleList { get; set; }
        public double TotalDeposit { get; set; }
        public double TotalWithdrawal { get; set; }
    }

}
