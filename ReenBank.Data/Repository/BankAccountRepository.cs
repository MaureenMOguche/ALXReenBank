using AlxReenBank.Data;
using ReenBank.Data.Repository.IRepository;
using ReenBank.Models;
using ReenBank.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReenBank.Data.Repository
{
    public class BankAccountRepository : Repository<BankAccount>, IBankAccountRepository
    {
        private readonly AlxReenBankContext _db;
        public BankAccountRepository(AlxReenBankContext db) : base(db)
        {
            _db = db;
        }

        public string Deposit(double amount, BankAccount account)
        {
            if (amount < 1)
            {
                return "Cannot Deposit less than $1";
            }
            else
            {
                account.Balance += amount;

                //Create the Deposit transaction
                var person = _db.People.FirstOrDefault(x => x.BankAccountId == account.Id);
                Transaction transaction = new()
                {
                    Amount = amount,
                    TransactionDate = DateTime.Now,
                    TransactionType = TransactionType.Credit,
                    BankAccount = account,
                    BankAccountId = account.Id,
                    From = "Self",
                    To = $"{person.FirstName} {person.LastName}",
                    CreditType = CreditDebitType.Deposit,
                };
                

                _db.Transactions.Add(transaction);
                _db.SaveChanges();

                return $"Successfully deposited {amount}";
            }
        }

        public string Transfer(double amount, BankAccount sender, String receiverAccNumber)
        {
            //Track sender Account from Db
            var senderAccount = _db.BankAccounts.FirstOrDefault(x => x.Id == sender.Id);


            //Check if receiver account exists
            var receiverAccount = _db.BankAccounts.FirstOrDefault(x => x.AccountNumber == receiverAccNumber);
            
            if (receiverAccount == null)
            {
                return "Not Found";
            }

            var receiverPerson = _db.People.FirstOrDefault(x => x.BankAccountId == receiverAccount.Id);
            
            if (senderAccount.Balance < amount)
            {
                return "Insufficient";
            }
            else
            {
                //Send Money
                senderAccount.Balance -= amount;
                //Create the Sender transaction
                var senderPerson = _db.People.FirstOrDefault(x => x.BankAccountId == senderAccount.Id);
                Transaction transactionSend = new()
                {
                    Amount = amount,
                    TransactionDate = DateTime.Now,
                    TransactionType = TransactionType.Debit,
                    BankAccount = senderAccount,
                    BankAccountId = senderAccount.Id,
                    From = $"{senderPerson.FirstName} {senderPerson.LastName}",
                    To = $"{receiverPerson.FirstName} {receiverPerson.LastName}"
                };

                _db.Transactions.Add(transactionSend);


                //Receive Money
                receiverAccount.Balance += amount;
                //Create the receiver transaction
                Transaction transactionReceive = new()
                {
                    Amount = amount,
                    TransactionDate = DateTime.Now,
                    TransactionType = TransactionType.Credit,
                    BankAccount = receiverAccount,
                    BankAccountId = receiverAccount.Id,
                    From = $"{senderPerson.FirstName} {senderPerson.LastName}",
                    To = $"{receiverPerson.FirstName} {receiverPerson.LastName}"
                };

                _db.Transactions.Add(transactionReceive);
                _db.SaveChanges();

                return "Successfull";
            }
            
        }

        public BankAccount SearchAccount(string searchAccount)
        {
            var account = _db.BankAccounts.Where(x => x.AccountNumber == searchAccount).FirstOrDefault();

            if (account == null)
            {
                return null;
            }

            return account;
        }


		public void Update(BankAccount bankAccount)
        {
            _db.BankAccounts.Update(bankAccount);
        }
    }
}
