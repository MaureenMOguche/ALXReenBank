using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReenBank.Models
{
    public class BankAccount
    {
        private static int _initalNumber = 1234567890;
        public BankAccount()
        {
            Balance = 0;
            AccountNumber = _initalNumber.ToString();
            _initalNumber++;
        }

        [Key]
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public double Balance { get; set; }

    }
}
