using Microsoft.AspNetCore.Mvc;
using ThucHanhWebMVC.Models;
using Microsoft.AspNetCore.Http;

namespace ThucHanhWebMVC.Controllers
{
    public class AccessController : Controller
    {
        QlbanVaLiContext db = new QlbanVaLiContext();
        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }
        [HttpPost]
        public IActionResult Login(TUser user)
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                var u = db.TUsers.Where(x => x.Username.Equals(user.Username) &&
                x.Password.Equals(user.Password)).FirstOrDefault();
                
                if (u != null)
                {
                    HttpContext.Session.SetString("UserName", u.Username.ToString());
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("UserName");
            return RedirectToAction("Login", "Access");
        }

        [Route("Register")]
        [HttpGet]
        public IActionResult Register()
        {
            return View();

        }

        [Route("Register")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(TUser user)
        {
            if (ModelState.IsValid)
            {
                user.LoaiUser = 0;
                db.TUsers.Add(user);
                db.SaveChanges();
                return RedirectToAction("Login");
            }
            return RedirectToAction("Login", "Access");
        }

    }


    }

