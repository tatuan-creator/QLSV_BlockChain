using QLSV_BlockChain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace QLSV_BlockChain.Areas.Admin.Controllers
{
    public class NguoiDungsController : Controller
    {
        private BlockChain_QLSVDataContext db = new BlockChain_QLSVDataContext();
        // GET: Admin/NguoiDungs
        public ActionResult Index()
        {
            var nguoiDungs = db.NguoiDungs.ToList();
            return View(nguoiDungs);
        }

        public ActionResult Create()
        {
            ViewBag.MaVaiTro = new SelectList(db.VaiTros, "MaVaitro", "TenVaiTro");
            return View();
        }

        // POST: Admin/NguoiDungs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TaiKhoan,MatKhau,Mota,MaVaiTro,SoE,SoN")] NguoiDung nguoiDung)
        {
            if (ModelState.IsValid)
            {
                db.NguoiDungs.InsertOnSubmit(nguoiDung);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaVaiTro = new SelectList(db.VaiTros, "MaVaitro", "TenVaiTro", nguoiDung.MaVaiTro);
            return View(nguoiDung);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            NguoiDung nguoiDung = db.NguoiDungs.FirstOrDefault(n => n.MaNguoiDung.Equals(id));
            if (nguoiDung == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaVaiTro = new SelectList(db.VaiTros, "MaVaitro", "TenVaiTro", nguoiDung.MaVaiTro);
            return View(nguoiDung);
        }

        // POST: Admin/NguoiDungs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaNguoiDung,TaiKhoan,MatKhau,MoTa,MaVaiTro,SoE,SoN")] NguoiDung nguoiDung)
        {
            if (ModelState.IsValid)
            {
                var user = db.NguoiDungs.FirstOrDefault(n => n.MaNguoiDung.Equals(nguoiDung.MaNguoiDung));
                user.TaiKhoan = nguoiDung.TaiKhoan;
                user.MatKhau = nguoiDung.MatKhau;
                user.Mota = nguoiDung.Mota;
                user.MaVaiTro = nguoiDung.MaVaiTro;
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaVaiTro = new SelectList(db.VaiTros, "MaVaitro", "TenVaiTro", nguoiDung.MaVaiTro);
            return View(nguoiDung);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NguoiDung nguoiDung = db.NguoiDungs.FirstOrDefault(p => p.MaNguoiDung.Equals(id));
            if (nguoiDung == null)
            {
                return HttpNotFound();
            }
            return View(nguoiDung);
        }

        // POST: Admin/NguoiDungs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NguoiDung nguoiDung = db.NguoiDungs.FirstOrDefault(p => p.MaNguoiDung.Equals(id));
            db.NguoiDungs.DeleteOnSubmit(nguoiDung);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}