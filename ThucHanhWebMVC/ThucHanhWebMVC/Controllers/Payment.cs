using Microsoft.AspNetCore.Mvc;
using ThucHanhWebMVC.Models;
using ThucHanhWebMVC.Models.ViewModels;
using ThucHanhWebMVC.Repository;

namespace ThucHanhWebMVC.Controllers
{
    public class Payment : Controller
    {
        QlbanVaLiContext db = new QlbanVaLiContext();
        public IActionResult Index()
        {
            return View();
                
        }
    }
}
