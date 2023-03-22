using Microsoft.AspNetCore.Mvc;
using ReenBank.Data.Repository.IRepository;
using ReenBank.Models.ViewModels;
using ReenBank.Utility;

namespace AlxReenBank.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly IUnitOfWork _db;
        public DashboardController(IUnitOfWork db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            AdminDashboardVM adminDashboard = new();
            adminDashboard.PeopleList = _db.PersonRepo.GetAll();
            return View(adminDashboard);
        }

        public IActionResult ClientList()
        {
            var clientList = _db.PersonRepo.GetAll("BankAccount");
            return View(clientList);
        }
    }
}
