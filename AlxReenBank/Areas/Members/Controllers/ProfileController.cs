using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using ReenBank.Data.Repository.IRepository;
using ReenBank.Models;
using ReenBank.Models.ViewModels;
using System.Security.Claims;

namespace AlxReenBank.Areas.Members.Controllers
{
    [Area("Members")]
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IUnitOfWork _db;

        [BindProperty]
        public PersonAccountTransactionVM patVM { get; set; } = new();
        public ProfileController(IUnitOfWork db)
        {
            _db = db;
        }
        
        public IActionResult Index()
        {
            var claimsidentity = (ClaimsIdentity)User.Identity;
            var claims = claimsidentity.FindFirst(ClaimTypes.NameIdentifier);

            patVM.Person = _db.PersonRepo.GetOne(x => x.Id == claims.Value);
            patVM.BankAccount = _db.BankAccountRepo.GetOne(x => x.Id == patVM.Person.BankAccountId);
            patVM.TransactionList = _db.TransactionsRepo.GetAll("BankAccount")
                                        .Where(x => x.BankAccountId == patVM.BankAccount.Id)
                                        .OrderByDescending(x => x.TransactionDate).Take(2);
      
            return View(patVM);
        }

        public IActionResult Deposit()
        {

            var claimsidentity = (ClaimsIdentity)User.Identity;
            var claims = claimsidentity.FindFirst(ClaimTypes.NameIdentifier);

            patVM.Person = _db.PersonRepo.GetOne(x => x.Id == claims.Value);
            patVM.BankAccount = _db.BankAccountRepo.GetOne(x => x.Id == patVM.Person.BankAccountId);
            patVM.TransactionList = _db.TransactionsRepo.GetAll("BankAccount").Where(x => x.BankAccountId == patVM.BankAccount.Id);



            return View(patVM);
        }

              


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Deposit(PersonAccountTransactionVM newPatVM)
        {

            var claimsidentity = (ClaimsIdentity)User.Identity;
            var claims = claimsidentity.FindFirst(ClaimTypes.NameIdentifier);

            newPatVM.Person = _db.PersonRepo.GetOne(x => x.Id == claims.Value);
            newPatVM.BankAccount = _db.BankAccountRepo.GetOne(x => x.Id == newPatVM.Person.BankAccountId);
            newPatVM.TransactionList = _db.TransactionsRepo.GetAll("BankAccount").Where(x => x.BankAccountId == newPatVM.BankAccount.Id);

            _db.BankAccountRepo.Deposit(newPatVM.AmountDeposit, newPatVM.BankAccount);
            _db.Save();
            TempData["Success"] = $"Successfully deposited {newPatVM.AmountDeposit}";
            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Transfer(string searchAccount)
        {
            TransferVM transferVM = new()
            {
                Person = new(),
            };

            //Search 
            var receiver = _db.BankAccountRepo.SearchAccount(searchAccount);
            if (receiver != null)
            {
                transferVM.ReceiverAccNumber = receiver.AccountNumber;
            }

            var claimsidentity = (ClaimsIdentity)User.Identity;
            var claims = claimsidentity.FindFirst(ClaimTypes.NameIdentifier);

            transferVM.Person = _db.PersonRepo.GetOne(x => x.Id == claims.Value);
            transferVM.SenderAccount = _db.BankAccountRepo.GetOne(x => x.Id == transferVM.Person.BankAccountId);


			return View(transferVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Transfer(TransferVM transferVM)
        {

            _db.BankAccountRepo.Transfer(transferVM.AmountTransfer, 
                                        transferVM.SenderAccount, transferVM.ReceiverAccNumber);
            
      

            return RedirectToAction("Index");
        }


        public IActionResult Settings()
        {
            var claimsidentity = (ClaimsIdentity)User.Identity;
            var claims = claimsidentity.FindFirst(ClaimTypes.NameIdentifier);

            patVM.Person = _db.PersonRepo.GetOne(x => x.Id == claims.Value);
            patVM.BankAccount = _db.BankAccountRepo.GetOne(x => x.Id == patVM.Person.BankAccountId);
            patVM.TransactionList = _db.TransactionsRepo.GetAll("BankAccount").Where(x => x.BankAccountId == patVM.BankAccount.Id);


            return View(patVM.Person);
        }


        public IActionResult Transactions()
        {
            var claimsidentity = (ClaimsIdentity)User.Identity;
            var claims = claimsidentity.FindFirst(ClaimTypes.NameIdentifier);

            patVM.Person = _db.PersonRepo.GetOne(x => x.Id == claims.Value);
            patVM.BankAccount = _db.BankAccountRepo.GetOne(x => x.Id == patVM.Person.BankAccountId);
            patVM.TransactionList = _db.TransactionsRepo.GetAll("BankAccount").Where(x => x.BankAccountId == patVM.BankAccount.Id);

            return View(patVM);
        }

        public IActionResult AccountStatement()
        {
            var claimsidentity = (ClaimsIdentity)User.Identity;
            var claims = claimsidentity.FindFirst(ClaimTypes.NameIdentifier);

            patVM.Person = _db.PersonRepo.GetOne(x => x.Id == claims.Value);
            patVM.BankAccount = _db.BankAccountRepo.GetOne(x => x.Id == patVM.Person.BankAccountId);
            patVM.TransactionList = _db.TransactionsRepo.GetAll("BankAccount").Where(x => x.BankAccountId == patVM.BankAccount.Id);

            return View(patVM);
        }


        public IActionResult DownloadStatement()
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            PdfDocument document = new();
            PdfPage page = document.AddPage();

            XGraphics gfx = XGraphics.FromPdfPage(page);
            XFont font = new XFont("Verdana", 20, XFontStyle.Bold);

            gfx.DrawString("Hello World", font, XBrushes.Black,
                    new XRect(0,0, page.Width, page.Height),
                    XStringFormat.Center);

            string filename = "mypdf.pdf";
            document.Save(filename);
            
            return View("Transactions");
        }
    }
}
