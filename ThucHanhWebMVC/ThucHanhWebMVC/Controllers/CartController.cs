using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ThucHanhWebMVC.Models;
using ThucHanhWebMVC.Models.ViewModels;
using ThucHanhWebMVC.Repository;

namespace ThucHanhWebMVC.Controllers
{
    public class CartController : Controller
    {
        private readonly QlbanVaLiContext _qlbanVaLiContext;
        public CartController(QlbanVaLiContext context)
        {
            _qlbanVaLiContext = context;
        }
        public IActionResult Index()
        {
            List<CartItemModel> cartItems = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
            CartItemViewModel cartVM = new()
            {
                CartItems = cartItems,
                GrandTotal = cartItems.Sum(x => x.Quantity * x.Price)
            };
            return View(cartVM);
        }

        public async Task<IActionResult> Addd(string id)
        {
            TDanhMucSp product = await _qlbanVaLiContext.TDanhMucSps.FindAsync(id);
            List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
            CartItemModel cartItems = cart.Where(c=>c.ProductID==id).FirstOrDefault();

            if(cartItems==null)
            {
                cart.Add(new CartItemModel(product));
            }
            else
            {
                cartItems.Quantity += 1;
            }
            HttpContext.Session.SetJson("Cart", cart);
            return Redirect(Request.Headers["Referer"].ToString());
        }

        public async Task<IActionResult> Decrease(string id)
        {
            List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ;
            CartItemModel cartItem = cart.Where(x=>x.ProductID == id).FirstOrDefault();
            if(cartItem.Quantity > 1)
            {
                --cartItem.Quantity;
            }
            else
            {
                cart.RemoveAll(p => p.ProductID == id);
            }
            if(cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cart);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Increase(string id)
        {
            List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart");
            CartItemModel cartItem = cart.Where(x => x.ProductID == id).FirstOrDefault();
            if (cartItem.Quantity >= 1)
            {
                ++cartItem.Quantity;
            }
            else
            {
                cart.RemoveAll(p => p.ProductID == id);
            }
            if (cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cart);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Remove(string id)
        {
            List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart");
            cart.RemoveAll(p => p.ProductID == id);
            if(cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cart);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Clear()
        {
            HttpContext.Session.Remove("Cart");
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> CheckOut()
        {
            
            var cus = _qlbanVaLiContext.TKhachHangs.FirstOrDefault(kh => kh.Username == HttpContext.Session.GetString("UserName")) ;
            if (cus == null || cus.MaKhanhHang == null)
            {
                return RedirectToAction("CheckOutKhachChuaLuu", "Cart");
            }
            else
            {
                List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart");
                var ordercode = Guid.NewGuid().ToString().Substring(0, 24);
                var orderItem = new THoaDonBan();
                orderItem.TongTienHd = cart.Sum(x => x.Quantity * x.Price);
                orderItem.MaHoaDon = ordercode;
                orderItem.MaSoThue = "0";
                orderItem.NgayHoaDon = DateTime.Now;
                orderItem.MaKhachHang = cus.MaKhanhHang;

                _qlbanVaLiContext.THoaDonBans.Add(orderItem);
                _qlbanVaLiContext.SaveChanges();
                foreach (var item in cart)
                {
                    var orderdetails = new TChiTietHdb();
                    var sanpham = _qlbanVaLiContext.TChiTietSanPhams.First(sp => sp.MaSp == item.ProductID);
                    orderdetails.MaChiTietSp = sanpham.MaChiTietSp;
                    orderdetails.DonGiaBan = item.Price;
                    orderdetails.MaHoaDon = ordercode;
                    orderdetails.SoLuongBan = item.Quantity;
                    _qlbanVaLiContext.TChiTietHdbs.Add(orderdetails);
                    _qlbanVaLiContext.SaveChanges();
                }
                HttpContext.Session.Remove("Cart");

                return View();
            }

        }
        [Route("CheckOutKhachChuaLuu")]
        [HttpGet]
        public IActionResult CheckOutKhachChuaLuu()
        {
            return View();

        }

        [Route("CheckOutKhachChuaLuu")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CheckOutKhachChuaLuu(TKhachHang khachHang)
        {
            if (ModelState.IsValid)
            {
                khachHang.MaKhanhHang = Guid.NewGuid().ToString().Substring(0, 6);
                khachHang.LoaiKhachHang = 0;
                khachHang.Username = HttpContext.Session.GetString("UserName");
                _qlbanVaLiContext.TKhachHangs.Add(khachHang);
                _qlbanVaLiContext.SaveChanges();
                return RedirectToAction("CheckOut");
            }
            return RedirectToAction("Index", "Cart");

        }


    }
}
