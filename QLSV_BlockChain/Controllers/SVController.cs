using QLSV_BlockChain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace QLSV_BlockChain.Controllers
{
    public class SVController : Controller
    {
        private BlockChain_QLSVDataContext db = new BlockChain_QLSVDataContext();
        // GET: SinhVien
        public ActionResult Index()
        {
            var sinhvien = db.SinhViens.ToList();
            return View(sinhvien);
        }
        public string mahoa(String str)
        {
            MD5 encrypt = MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(str);
            byte[] hash = encrypt.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i]);
            }
            return sb.ToString();
        }

        public ActionResult Dangxuat()
        {
            Session["userAdmin"] = null;
            return Redirect("~/Admin/HomeAdmin/DangNhap");
        }

        public ActionResult BangDiem()
        {
            var bangDiem = db.BangDiems.ToList();
            return View(bangDiem);
        }
        public ActionResult EditTaiKhoan(int id)
        {
            var edit = db.SinhViens.FirstOrDefault(p => p.IDSinhVien == id);
            edit.matkhau = null;
            return View(edit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTaiKhoan([Bind(Include = "IDSinhVien,TenSinhVien,Lop,KhoaVien,NgaySinh,TrangThai,taikhoan,matkhau")] SinhVien sinhVien)
        {
            if (ModelState.IsValid)
            {
                var user = db.SinhViens.FirstOrDefault(n => n.IDSinhVien.Equals(sinhVien.IDSinhVien));
                user.taikhoan = sinhVien.taikhoan;
                user.matkhau = mahoa(sinhVien.matkhau);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View(sinhVien);
        }
    }
}