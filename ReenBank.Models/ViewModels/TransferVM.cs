using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReenBank.Models.ViewModels
{
    public class TransferVM
    {
        public Person Person { get; set; }
        public Person ReceiverPerson { get; set; }
        public BankAccount ReceiverAccount { get; set; }
        public BankAccount SenderAccount { get; set; }
        public double AmountTransfer { get; set; }
        public string ReceiverAccNumber { get; set; }
        public string? Description { get; set; }
    }
}
