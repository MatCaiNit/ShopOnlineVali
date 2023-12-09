using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ThucHanhWebMVC.Models;
using X.PagedList;

namespace ThucHanhWebMVC.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin")]
    [Route("admin/homeadmin")]
    public class HomeAdminController : Controller
    {
        QlbanVaLiContext db=new QlbanVaLiContext();
        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            return View();
        }
        [Route("danhmucsanpham")]
        public IActionResult DanhMucSanPham(int? page)
        {
            int pageSize = 12;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstsanpham = db.TDanhMucSps.AsNoTracking().OrderBy(x => x.TenSp);
            PagedList<TDanhMucSp> lst = new PagedList<TDanhMucSp>(lstsanpham, pageNumber, pageSize);

            return View(lst);
        }
        [Route("xuatxu")]
        public IActionResult XuatXu(int? page)
        {
            int pageSize = 12;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstquocgia = db.TQuocGia.AsNoTracking().OrderBy(x => x.TenNuoc);
            PagedList<TQuocGium> lst = new PagedList<TQuocGium>(lstquocgia, pageNumber, pageSize);

            return View(lst);
        }
        [Route("taikhoan")]
        public IActionResult TaiKhoan(int? page)
        {
            int pageSize = 12;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lsttaikhoan = db.TUsers.AsNoTracking().OrderBy(x => x.Username);
            PagedList<TUser> lst = new PagedList<TUser>(lsttaikhoan, pageNumber, pageSize);

            return View(lst);
        }

        [Route("chatlieu")]
        public IActionResult ChatLieu(int? page)
        {
            int pageSize = 12;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstsanpham = db.TChatLieus.AsNoTracking().OrderBy(x => x.ChatLieu);
            PagedList<TChatLieu> lst = new PagedList<TChatLieu>(lstsanpham, pageNumber, pageSize);

            return View(lst);
        }

        [Route("hang")]
        public IActionResult Hang(int? page)
        {
            int pageSize = 12;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstsanpham = db.THangSxes.AsNoTracking().OrderBy(x => x.HangSx);
            PagedList<THangSx> lst = new PagedList<THangSx>(lstsanpham, pageNumber, pageSize);

            return View(lst);
        }

        [Route("loai")]
        public IActionResult Loai(int? page)
        {
            int pageSize = 12;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstsanpham = db.TLoaiSps.AsNoTracking().OrderBy(x => x.Loai);
            PagedList<TLoaiSp> lst = new PagedList<TLoaiSp>(lstsanpham, pageNumber, pageSize);

            return View(lst);
        }

        [Route("dt")]
        public IActionResult Dt(int? page)
        {
            int pageSize = 12;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstsanpham = db.TLoaiDts.AsNoTracking().OrderBy(x => x.TenLoai);
            PagedList<TLoaiDt> lst = new PagedList<TLoaiDt>(lstsanpham, pageNumber, pageSize);

            return View(lst);
        }

        [Route("khachhang")]
        public IActionResult KhachHang(int? page)
        {
            int pageSize = 12;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstsanpham = db.TKhachHangs.AsNoTracking().OrderBy(x => x.MaKhanhHang);
            PagedList<TKhachHang> lst = new PagedList<TKhachHang>(lstsanpham, pageNumber, pageSize);

            return View(lst);
        }

        [Route("nhanvien")]
        public IActionResult NhanVien(int? page)
        {
            int pageSize = 12;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstsanpham = db.TNhanViens.AsNoTracking().OrderBy(x => x.MaNhanVien);
            PagedList<TNhanVien> lst = new PagedList<TNhanVien>(lstsanpham, pageNumber, pageSize);

            return View(lst);
        }


        [Route("ThemSanPhamMoi")]
        [HttpGet]
        public IActionResult ThemSanPhamMoi()
        {
            ViewBag.MaChatLieu = new SelectList(db.TChatLieus.ToList(), "MaChatLieu", "ChatLieu");
            ViewBag.MaHangSx = new SelectList(db.THangSxes.ToList(), "MaHangSx", "HangSx");
            ViewBag.MaNuocSx = new SelectList(db.TQuocGia.ToList(), "MaNuoc", "TenNuoc");
            ViewBag.MaLoai = new SelectList(db.TLoaiSps.ToList(), "MaLoai", "Loai");
            ViewBag.MaDt = new SelectList(db.TLoaiDts.ToList(), "MaDt", "TenLoai");
            return View();
        }
        [Route("ThemSanPhamMoi")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemSanPhamMoi(TDanhMucSp sanPham) 
        {
            if (ModelState.IsValid)
            {
                db.TDanhMucSps.Add(sanPham);
                db.SaveChanges();
                return RedirectToAction("DanhMucSanPham");
            }
            return View(sanPham);
        }

        [Route("ThemXuatXu")]
        [HttpGet]
        public IActionResult ThemXuatXu()
        {
            
            return View();
        }
        [Route("ThemXuatXu")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemXuatXu(TQuocGium quocgia)
        {
            if (ModelState.IsValid)
            {
                db.TQuocGia.Add(quocgia);
                db.SaveChanges();
                return RedirectToAction("XuatXu");
            }
            return View(quocgia);
        }
        [Route("ThemTaiKhoan")]
        [HttpGet]
        public IActionResult ThemTaiKhoan()
        {

            return View();
        }
        [Route("ThemTaiKhoan")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemTaiKhoan(TUser user)
        {
            if (ModelState.IsValid)
            {
                db.TUsers.Add(user);
                db.SaveChanges();
                return RedirectToAction("TaiKhoan");
            }
            return View(user);
        }

        [Route("ThemChatLieu")]
        [HttpGet]
        public IActionResult ThemChatLieu()
        {

            return View();
        }
        [Route("ThemChatLieu")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemChatLieu(TChatLieu chatLieu)
        {
            if (ModelState.IsValid)
            {
                db.TChatLieus.Add(chatLieu);
                db.SaveChanges();
                return RedirectToAction("ChatLieu");
            }
            return View(chatLieu);
        }

        [Route("ThemHang")]
        [HttpGet]
        public IActionResult ThemHang()
        {

            return View();
        }
        [Route("ThemHang")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemHang(THangSx hangsx)
        {
            if (ModelState.IsValid)
            {
                db.THangSxes.Add(hangsx);
                db.SaveChanges();
                return RedirectToAction("Hang");
            }
            return View(hangsx);
        }

        [Route("ThemLoai")]
        [HttpGet]
        public IActionResult ThemLoai()
        {

            return View();
        }
        [Route("ThemLoai")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemLoai(TLoaiSp loaisp)
        {
            if (ModelState.IsValid)
            {
                db.TLoaiSps.Add(loaisp);
                db.SaveChanges();
                return RedirectToAction("Loai");
            }
            return View(loaisp);
        }

        [Route("ThemDt")]
        [HttpGet]
        public IActionResult ThemDt()
        {

            return View();
        }
        [Route("ThemDt")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemDt(TLoaiDt loaidt)
        {
            if (ModelState.IsValid)
            {
                db.TLoaiDts.Add(loaidt);
                db.SaveChanges();
                return RedirectToAction("Dt");
            }
            return View(loaidt);
        }

        [Route("ThemKhachHang")]
        [HttpGet]
        public IActionResult ThemKhachHang()
        {
            ViewBag.Username = new SelectList(db.TUsers.ToList(), "Username", "Username");
            return View();
        }
        [Route("ThemKhachHang")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemKhachHang(TKhachHang khachhang)
        {
            if (ModelState.IsValid)
            {
                db.TKhachHangs.Add(khachhang);
                db.SaveChanges();
                return RedirectToAction("KhachHang");
            }
            return View(khachhang);
        }

        [Route("ThemNhanVien")]
        [HttpGet]
        public IActionResult ThemNhanVien()
        {
            ViewBag.Username = new SelectList(db.TUsers.ToList(), "Username", "Username");
            return View();
        }
        [Route("ThemNhanVien")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemNhanVien(TNhanVien nhanvien)
        {
            if (ModelState.IsValid)
            {
                db.TNhanViens.Add(nhanvien);
                db.SaveChanges();
                return RedirectToAction("NhanVien");
            }
            return View(nhanvien);
        }

        [Route("SuaSanPham")]
        [HttpGet]
        public IActionResult SuaSanPham(string maSanPham)
        {
            ViewBag.MaChatLieu = new SelectList(db.TChatLieus.ToList(), "MaChatLieu", "ChatLieu");
            ViewBag.MaHangSx = new SelectList(db.THangSxes.ToList(), "MaHangSx", "HangSx");
            ViewBag.MaNuocSx = new SelectList(db.TQuocGia.ToList(), "MaNuoc", "TenNuoc");
            ViewBag.MaLoai = new SelectList(db.TLoaiSps.ToList(), "MaLoai", "Loai");
            ViewBag.MaDt = new SelectList(db.TLoaiDts.ToList(), "MaDt", "TenLoai");
            var sanPham = db.TDanhMucSps.Find(maSanPham);
            return View(sanPham);
        }
        [Route("SuaSanPham")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaSanPham(TDanhMucSp sanPham)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DanhMucSanPham","HomeAdmin");
            }
            return View(sanPham);
        }

        [Route("SuaTaiKhoan")]
        [HttpGet]
        public IActionResult SuaTaiKhoan(string tentaikhoan)
        {
            var taikhoan = db.TUsers.Find(tentaikhoan);
            return View(taikhoan);
        }
        [Route("SuaTaiKhoan")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaTaiKhoan(TUser taikhoan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taikhoan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("TaiKhoan", "HomeAdmin");
            }
            return View(taikhoan);
        }

        [Route("SuaXuatXu")]
        [HttpGet]
        public IActionResult SuaXuatXu(string maNuoc)
        {
            var xuatXu = db.TQuocGia.Find(maNuoc);
            return View(xuatXu);
        }
        [Route("SuaXuatXu")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaXuatXu(TQuocGium quocgia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(quocgia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("XuatXu", "HomeAdmin");
            }
            return View(quocgia);
        }

        [Route("SuaChatLieu")]
        [HttpGet]
        public IActionResult SuaChatLieu(string machatlieu)
        {
            var chatlieu = db.TChatLieus.Find(machatlieu);
            return View(chatlieu);
        }
        [Route("SuaChatLieu")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaChatLieu(TChatLieu chatlieu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chatlieu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ChatLieu", "HomeAdmin");
            }
            return View(chatlieu);
        }

        [Route("SuaHang")]
        [HttpGet]
        public IActionResult SuaHang(string mahang)
        {
            var hang = db.THangSxes.Find(mahang);
            return View(hang);
        }
        [Route("SuaHang")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaHang(THangSx hangsx)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hangsx).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Hang", "HomeAdmin");
            }
            return View(hangsx);
        }

        [Route("SuaLoai")]
        [HttpGet]
        public IActionResult SuaLoai(string maloai)
        {
            var loai=db.TLoaiSps.Find(maloai);
            return View(loai);
        }
        [Route("SuaLoai")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaLoai(TLoaiSp loaisp)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loaisp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Loai","HomeAdmin");
            }
            return View(loaisp);
        }

        [Route("SuaDt")]
        [HttpGet]
        public IActionResult SuaDt(string madt)
        {
            var dt = db.TLoaiDts.Find(madt);
            return View(dt);
        }
        [Route("SuaDt")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaDt(TLoaiDt loaidt)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loaidt).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Dt", "HomeAdmin");
            }
            return View(loaidt);
        }

        [Route("SuaNhanVien")]
        [HttpGet]
        public IActionResult SuaNhanVien(string manhanvien)
        {
            ViewBag.Username = new SelectList(db.TUsers.ToList(), "Username", "Username");
            var nhanvien = db.TNhanViens.Find(manhanvien);
            return View(nhanvien);
        }
        [Route("SuaNhanVien")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaNhanVien(TNhanVien nhanVien)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nhanVien).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("NhanVien","HomeAdmin");
            }
            return View(nhanVien);
        }

        [Route("SuaKhachHang")]
        [HttpGet]
        public IActionResult SuaKhachHang(string makhachhang)
        {
            ViewBag.Username = new SelectList(db.TUsers.ToList(), "Username", "Username");
            var khachhang = db.TKhachHangs.Find(makhachhang);
            return View(khachhang);
        }
        [Route("SuaKhachHang")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaKhachHang(TKhachHang khachhang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(khachhang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("KhachHang", "HomeAdmin");
            }
            return View(khachhang);
        }

        [Route("XoaSanPham")]
        [HttpGet]
        public IActionResult XoaSanPham(string maSanPham)
        {
            TempData["Message"] = "";
            var chiTietSanPhams=db.TChiTietSanPhams.Where(x=>x.MaSp==maSanPham).ToList();
            if (chiTietSanPhams.Count > 0)
            {
                TempData["Message"] = "Không Xóa Được Sản Phẩm Này";
                return RedirectToAction("DanhMucSanPham", "HomeAdmin");
            }
            var anhSanPhams=db.TAnhSps.Where(x => x.MaSp == maSanPham);
            if (anhSanPhams.Any()) db.RemoveRange(anhSanPhams);
            db.Remove(db.TDanhMucSps.Find(maSanPham));
            db.SaveChanges();
            TempData["Message"] = "Sản Phẩm Đã Được Xóa";
            return RedirectToAction("DanhMucSanPham", "HomeAdmin");
        }
        [Route("XoaXuatXu")]
        [HttpGet]
        public IActionResult XoaXuatXu(string maNuoc)
        {
            TempData["Message"] = "";
            var quocgia = db.TDanhMucSps.Where(x => x.MaNuocSx == maNuoc).ToList();
            if (quocgia.Count > 0)
            {
                TempData["Message"] = "Không Xóa Được Xuất Xứ Này";
                return RedirectToAction("XuatXu", "HomeAdmin");
            }
            
            db.Remove(db.TQuocGia.Find(maNuoc));
            db.SaveChanges();
            TempData["Message"] = "Xuất Xứ Đã Được Xóa";
            return RedirectToAction("XuatXu", "HomeAdmin");
        }

        [Route("XoaTaiKhoan")]
        [HttpGet]
        public IActionResult XoaTaiKhoan(string tentaikhoan)
        {
            TempData["Message"] = "";
            var taikhoankhachhang = db.TKhachHangs.Where(x => x.Username == tentaikhoan).ToList();
            if (taikhoankhachhang.Count > 0)
            {
                TempData["Message"] = "Không Xóa Được Tài Khoản Này";
                return RedirectToAction("TaiKhoan", "HomeAdmin");
            }
            var taikhoannhanvien = db.TNhanViens.Where(x => x.Username == tentaikhoan).ToList();
            if (taikhoannhanvien.Count > 0)
            {
                TempData["Message"] = "Không Xóa Được Tài Khoản Này";
                return RedirectToAction("TaiKhoan", "HomeAdmin");
            }

            db.Remove(db.TUsers.Find(tentaikhoan));
            db.SaveChanges();
            TempData["Message"] = "Tài Khoản Đã Được Xóa";
            return RedirectToAction("TaiKhoan", "HomeAdmin");
        }

        [Route("XoaChatLieu")]
        [HttpGet]
        public IActionResult XoaChatLieu(string machatlieu)
        {
            TempData["Message"] = "";
            var chatlieu = db.TDanhMucSps.Where(x => x.MaChatLieu == machatlieu).ToList();
            if (chatlieu.Count > 0)
            {
                TempData["Message"] = "Không Xóa Được Chất Liệu Này";
                return RedirectToAction("ChatLieu", "HomeAdmin");
            }

            db.Remove(db.TChatLieus.Find(machatlieu));
            db.SaveChanges();
            TempData["Message"] = "Chất Liệu Này Đã Được Xóa";
            return RedirectToAction("ChatLieu", "HomeAdmin");
        }

        [Route("XoaHang")]
        [HttpGet]
        public IActionResult XoaHang(string mahang)
        {
            TempData["Message"] = "";
            var hang = db.TDanhMucSps.Where(x => x.MaHangSx == mahang).ToList();
            if (hang.Count > 0)
            {
                TempData["Message"] = "Không Xóa Được Hãng Này";
                return RedirectToAction("Hang", "HomeAdmin");
            }

            db.Remove(db.THangSxes.Find(mahang));
            db.SaveChanges();
            TempData["Message"] = "Hãng Này Đã Được Xóa";
            return RedirectToAction("Hang", "HomeAdmin");
        }

        [Route("XoaLoai")]
        [HttpGet]
        public IActionResult XoaLoai(string maloai)
        {
            TempData["Message"] = "";
            var loai = db.TDanhMucSps.Where(x => x.MaLoai == maloai).ToList();
            if (loai.Count > 0)
            {
                TempData["Message"] = "Không Xóa Được Loại Này";
                return RedirectToAction("Loai", "HomeAdmin");
            }

            db.Remove(db.TLoaiSps.Find(maloai));
            db.SaveChanges();
            TempData["Message"] = "Loại Này Đã Được Xóa";
            return RedirectToAction("Loai", "HomeAdmin");
        }

        [Route("XoaDt")]
        [HttpGet]
        public IActionResult XoaDt(string madt)
        {
            TempData["Message"] = "";
            var dt = db.TDanhMucSps.Where(x => x.MaDt == madt).ToList();
            if (dt.Count > 0)
            {
                TempData["Message"] = "Không Xóa Được Loại Này";
                return RedirectToAction("Dt", "HomeAdmin");
            }

            db.Remove(db.TLoaiDts.Find(madt));
            db.SaveChanges();
            TempData["Message"] = "Loại Này Đã Được Xóa";
            return RedirectToAction("Dt", "HomeAdmin");
        }

        [Route("XoaNhanVien")]
        [HttpGet]
        public IActionResult XoaNhanVien(string manhanvien)
        {
            TempData["Message"] = "";
            var nhanvien = db.THoaDonBans.Where(x => x.MaNhanVien == manhanvien).ToList();
            if (nhanvien.Count > 0)
            {
                TempData["Message"] = "Không Xóa Được Nhân Viên Này";
                return RedirectToAction("NhanVien", "HomeAdmin");
            }
            db.Remove(db.TNhanViens.Find(manhanvien));
            db.SaveChanges();
            TempData["Message"] = "Nhân Viên Đã Được Xóa";
            return RedirectToAction("NhanVien", "HomeAdmin");
        }

        [Route("XoaKhachHang")]
        [HttpGet]
        public IActionResult XoaKhachHang(string makhachhang)
        {
            TempData["Message"] = "";
            var khachhang = db.THoaDonBans.Where(x => x.MaKhachHang == makhachhang).ToList();
            if (khachhang.Count > 0)
            {
                TempData["Message"] = "Không Xóa Được Khách Hàng Này";
                return RedirectToAction("KhachHang", "HomeAdmin");
            }
            db.Remove(db.TKhachHangs.Find(makhachhang));
            db.SaveChanges();
            TempData["Message"] = "Khách Hàng Đã Được Xóa";
            return RedirectToAction("KhachHang", "HomeAdmin");
        }
    }
}
