using QLSV_BlockChain.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace QLSV_BlockChain.Areas.DangTai.Controllers
{
    public class SinhVienController : Controller
    {
        private BlockChain_QLSVDataContext db = new BlockChain_QLSVDataContext();
        // GET: DangTai/SinhVien
        public ActionResult Index()
        {
            if (Session["userAdmin"] == null)
            {
                return Redirect("~/Admin/HomeAdmin/DangNhap");
            }
            var listSV = db.SinhViens.ToList();
            return View(listSV);
        }

        public ActionResult Create()
        {
            ViewBag.Khoa = new SelectList(db.KhoaViens, "IDKhoa", "TenKhoa");
            ViewBag.TrangThai = new SelectList(db.StatusSinhViens, "IDTrangThai", "TenTrangThai");
            return View();
        }

        // POST: Admin/NguoiDungs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDSinhVien,TenSinhVien,Lop,KhoaVien,NgaySinh,TrangThai")] SinhVien sinhvien)
        {
            if (ModelState.IsValid)
            {
                db.SinhViens.InsertOnSubmit(sinhvien);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Khoa = new SelectList(db.KhoaViens, "IDKhoa", "TenKhoa");
            ViewBag.TrangThai = new SelectList(db.StatusSinhViens, "IDTrangThai", "TenTrangThai");
            return View(sinhvien);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SinhVien sinhvien = db.SinhViens.FirstOrDefault(p => p.IDSinhVien.Equals(id));
            if (sinhvien == null)
            {
                return HttpNotFound();
            }
            ViewBag.Khoa = new SelectList(db.KhoaViens, "IDKhoa", "TenKhoa");
            ViewBag.TrangThai = new SelectList(db.StatusSinhViens, "IDTrangThai", "TenTrangThai");
            return View(sinhvien);
        }

        // POST: Admin/NguoiDungs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDSinhVien, TenSinhVien, Lop, KhoaVien, NgaySinh, TrangThai")] SinhVien sinhvien)
        {
            if (ModelState.IsValid)
            {
                var edit = db.SinhViens.FirstOrDefault(p => p.IDSinhVien.Equals(sinhvien.IDSinhVien));
                edit.TenSinhVien = sinhvien.TenSinhVien;
                edit.Lop = sinhvien.Lop;
                edit.NgaySinh = sinhvien.NgaySinh;
                edit.KhoaVien = sinhvien.KhoaVien;
                edit.TrangThai = sinhvien.TrangThai;
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Khoa = new SelectList(db.KhoaViens, "IDKhoa", "TenKhoa");
            ViewBag.TrangThai = new SelectList(db.StatusSinhViens, "IDTrangThai", "TenTrangThai");
            return View(sinhvien);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SinhVien sinhvien = db.SinhViens.FirstOrDefault(p => p.IDSinhVien.Equals(id));
            if (sinhvien == null)
            {
                return HttpNotFound();
            }
            return View(sinhvien);
        }

        // POST: Admin/NguoiDungs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SinhVien sinhvien = db.SinhViens.FirstOrDefault(p => p.IDSinhVien.Equals(id));
            db.SinhViens.DeleteOnSubmit(sinhvien);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Dangxuat()
        {
            Session["userAdmin"] = null;
            return Redirect("~/Admin/HomeAdmin/DangNhap");
        }
    }
}